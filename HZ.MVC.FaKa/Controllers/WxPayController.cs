using HZ.MVC.FaKa.Common;
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
            //string appid = "wx69928c2eedef13bd";
            //string mch_id = "1433411202";
            //string device_info = "WEB";
            //string nonce_str = Guid.NewGuid().ToString().Replace("-", "");
            //string body = "【腾讯视频VIP】1个月账号（包售后）";
            //string out_trade_no = DTHelper.GetCurrentTimeString();
            //int total_fee = 2;
            //string spbill_create_ip = "115.60.63.101";
            //string notify_url = "http://www.69zshi.com";
            //string trade_type = "APP";

            string sign = data.GetSignValue();
            data.Add("sign", sign);

            string res = data.Post();
            return View();
        }

    }
}
