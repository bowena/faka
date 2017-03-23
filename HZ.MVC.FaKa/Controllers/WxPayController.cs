using HZ.MVC.FaKa.Common;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZ.MVC.FaKa.Controllers
{
    public class WxPayController : Controller
    {
        //
        // GET: /WxPay/

        public ActionResult Index()
        {
            PostData data = new PostData();

            data.Add("appid", "wx69928c2eedef13bd");
            data.Add("mch_id", "1433411202");
            data.Add("device_info", "WEB");
            data.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));
            data.Add("body", "会员付款");
            data.Add("out_trade_no", DTHelper.GetCurrentTimeString());
            data.Add("total_fee", "2");
            data.Add("spbill_create_ip", "115.60.63.101");
            data.Add("notify_url", "http://www.69zshi.com");
            data.Add("trade_type", "NATIVE");
           
            string sign = data.GetSignValue();
            data.Add("sign", sign);

            data.Post();
            string url = data.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接
            return View();
        }


        /// <summary>
        /// 订单生成
        /// </summary>
        /// <returns></returns>
        public ActionResult Order()
        {



            ViewBag.QR_url = "";
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_QrCodePartial", OAuthWebSecurity.RegisteredClientData);
        }

    }
}
