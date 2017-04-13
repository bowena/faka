using HZ.MVC.FaKa.Areas.Admin.Models;
using HZ.MVC.FaKa.BLL;
using HZ.MVC.FaKa.Common;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WxPayAPI;

namespace HZ.MVC.FaKa.Controllers
{
    public class WxPayController : Controller
    {
        //
        // GET: /WxPay/

        public ActionResult Index()
        {

            return View();
        }


        /// <summary>
        /// 订单生成
        /// </summary>
        /// <returns></returns>
        public ActionResult Order()
        {
            string proId = Request.Form["proId"];
            string name = Request.Form["proName"];
            string price = Request.Form["price"];
            string num = Request.Form["num"];
            string email = Request.Form["email"];

            ProductViewModel model = BProduct.SearchById(proId);
            if (model == null)
            {
                return Content("订单生成异常");
            }

            double _price = Convert.ToDouble(price);
            int _num = Convert.ToInt32(num);

            if (model.Price != _price)
            {
                return Content("订单生成异常(金额不符)");
            }
            if (model.Name != name)
            {
                return Content("订单生成异常(名称不符)");
            }

            double totalPrice = _price * _num;
            string orderNo = DTHelper.GetCurrentTimeOrderNo();
            //订单生成
            OrderViewModel orderModel = new OrderViewModel();
            orderModel.Count = _num;
            orderModel.NO = orderNo;
            orderModel.Price = _price;
            orderModel.Product_Id = Convert.ToInt32(proId);
            orderModel.Remark = email;
            orderModel.Type = Request.Form["paytype"];
            orderModel.LocalStatus = "待支付";
            orderModel.UpdateTime = DateTime.Now;
            BOrder.Insert(orderModel);

            string url = "";

            if (orderModel.Type == "1")
            {
                NativePay nativePay = new NativePay();

                //转化为以分为单位的金额
                int money = Convert.ToInt32(totalPrice * 100);
                //生成扫码支付模式二url
                url = "https://www.baifubao.com/o2o/0/qrcode?size=6&text=" + nativePay.GetPayUrl(proId, money, name, orderNo); 
            }
            else if(orderModel.Type == "2")
            {
                url = "/Zhifu/precreate.aspx?tid=" + orderNo;
            }
            else { }

            ViewBag.QR_url = url;
            ViewBag.Name = name;
            ViewBag.Num = _num;
            ViewBag.Total = totalPrice;
            ViewBag.tNo = orderNo;
            ViewBag.payType = Request.Form["paytype"];
            return View();
        }

        public ActionResult KaMiList()
        {
            //查询卡密，并显示
            string tNo = Request.QueryString["tNo"];
            OrderViewModel order = BOrder.SearchByTradeNo(tNo);
            if (order == null)
                return View();

            Dictionary<int, string> kamis = BKaMi.SearchKamiByTrade(order);

            #region 更新卡密状态为已使用
            List<KaMiViewModel> models = new List<KaMiViewModel>();
            foreach (var item in kamis)
            {
                KaMiViewModel ka = new KaMiViewModel();
                ka.Id = item.Key;
                ka.Remark = order.Remark;
                ka.Trade_No = tNo;
                models.Add(ka);
            }
            BKaMi.UpdateBySql(models);
            #endregion

            return View(kamis);
        }

        public ActionResult SearchByContact()
        {
            string contact = Request.Form["contact"];
            Regex regex = new Regex(@"^\b[A-Z]+\d+\b$");
            Dictionary<int, string> kamis = new Dictionary<int, string>();
            if (!regex.IsMatch(contact))
            {
                 kamis = BKaMi.SearchKamiByContact(contact, "");
                 if (kamis.Count == 0)
                     return Content("未查到对应订单信息");
            }
            else
            {
                 kamis = BKaMi.SearchKamiByContact("", contact);
                 if (kamis.Count == 0)
                     return Content("未查到对应订单信息");
            }

            return View("KaMiList", kamis);
        }

        public JsonResult CheckPayResult()
        {
            string tradeNo = Request["trade_no"];
            string payType = Request["payway"];

            OrderViewModel model = BOrder.SearchByTradeNo(tradeNo);
            if (model == null)
            {
                var result = new { code = "-2", msg = "not exists" };
                return Json(LitJson.JsonMapper.ToJson(result));
            }

            if (model.LocalStatus != "已付款")
            {
                var result = new { code = "-1", msg = "wait for pay" };
                return Json(LitJson.JsonMapper.ToJson(result));
            }
            else
            {
                var result = new { code = "1", msg = "success" };
                return Json(LitJson.JsonMapper.ToJson(result));
            }
        }

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPayResult()
        {
            string resultFromWx = getPostStr();
            if (string.IsNullOrEmpty(resultFromWx))
                return View();
            //string path = Path.Combine(Server.MapPath("~/data"), "data" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
            //FileInfo f = new FileInfo(path);
            //if (!Directory.Exists(f.DirectoryName))
            //    Directory.CreateDirectory(f.DirectoryName);
            //using (StreamWriter writer = new StreamWriter(path))
            //{
            //    writer.Write(resultFromWx);
            //}

            var res = XDocument.Parse(resultFromWx);

            //通信成功
            try
            {
                if (res.Element("xml").Element("return_code").Value == "SUCCESS")
                {
                    if (res.Element("xml").Element("result_code").Value == "SUCCESS")
                    {
                        //交易成功
                        string transaction_id = res.Element("xml").Element("transaction_id").Value;//微信订单号
                        string trade_no = res.Element("xml").Element("out_trade_no").Value;//商户订单号

                        //查询订单是否存在
                        OrderViewModel model = BOrder.SearchByTradeNo(trade_no);
                        if (model != null)
                        {
                            //存在
                            BOrder.Update("update Orders set LocalStatus='已付款' where Id=" + model.Id);
                        }
                        return Content(CallWxSuccess());
                    }
                    else
                    {
                        return Content("支付出现问题，请返回重试");
                    }
                }
            }
            catch (Exception e)
            {

            }
            return Content("failed");
        }

        public string CallWxSuccess()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.Append("<return_code><![CDATA[SUCCESS]]></return_code>");
            builder.Append("<return_msg><![CDATA[OK]]></return_msg>");
            builder.Append("</xml>");
            return builder.ToString();
        }

        public string CallWxFail()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<xml>");
            builder.Append("<return_code><![CDATA[FAIL]]></return_code>");
            builder.Append("<return_msg><![CDATA[OK]]></return_msg>");
            builder.Append("</xml>");
            return builder.ToString();
        }

        /// <summary>
        /// 获取 Post 提交的参数
        /// </summary>
        /// <returns></returns>
        public string getPostStr()
        {
            Int32 intLen = Convert.ToInt32(Request.InputStream.Length);
            byte[] b = new byte[intLen];
            Request.InputStream.Read(b, 0, intLen);
            return System.Text.Encoding.UTF8.GetString(b);
        }

    }
}
