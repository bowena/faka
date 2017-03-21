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
            string appid = "wx69928c2eedef13bd";
            string mch_id = "1433411202";
            string device_info = "WEB";
            string nonce_str = DTHelper.GetCurrentTimeString();


            return View();
        }

    }
}
