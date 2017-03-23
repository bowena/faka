using HZ.MVC.FaKa.Common;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            NativePay nativePay = new NativePay();

            //生成扫码支付模式二url
            string url = nativePay.GetPayUrl("123456789");

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

    }
}
