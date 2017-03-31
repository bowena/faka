using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZ.MVC.FaKa.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Admin/Order/

        public ActionResult Index()
        {
            ViewBag.title = "订单管理";
            return View();
        }

    }
}
