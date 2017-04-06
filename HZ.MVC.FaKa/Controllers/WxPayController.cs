using HZ.MVC.FaKa.Common;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            string orderNo = Request.Form["orderNo"];
            string price = Request.Form["price"];
            string num = Request.Form["num"];
            string email = Request.Form["email"];


            NativePay nativePay = new NativePay();

            //生成扫码支付模式二url
            string url = nativePay.GetPayUrl("1234567890");

            ViewBag.QR_url = url;
            return View();
        }

        public ActionResult KaMiList()
        {
            //此处接受微信返回通知，进行相应处理
            System.IO.Stream response = Request.InputStream;
            using (StreamReader read = new StreamReader(response,System.Text.Encoding.UTF8))
            {
                string resStr = read.ReadToEnd();
            }



            return View();
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
            string path = Path.Combine(Server.MapPath("~/data"), "data" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
            FileInfo f = new FileInfo(path);
            if (!Directory.Exists(f.DirectoryName))
                Directory.CreateDirectory(f.DirectoryName);
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(resultFromWx);
            }

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
                        string transaction_id_own = res.Element("xml").Element("out_trade_no").Value;//商户订单号

                        //查询订单是否存在
                        //XDocument query = WXHelper.Orderquery(transaction_id_own);
                        //if (query.Element("xml").Element("trade_state").Value == "SUCCESS")
                        //{
                        //    UpdateOrder(res, transaction_id, transaction_id_own);
                        //}

                        //通知微信不必返回
                        //Response.Write(CallWxSuccess());
                    }
                }
            }
            catch (Exception e)
            {

            }

            return Content(CallWxSuccess());
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
