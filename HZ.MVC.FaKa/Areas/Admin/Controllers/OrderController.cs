using HZ.MVC.FaKa.Areas.Admin.Models;
using HZ.MVC.FaKa.Areas.Admin.Models.Enum;
using HZ.MVC.FaKa.BLL;
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

        public JsonResult GetOrders(int limit, int offset, string departmentname)
        {
            List<OrderViewModel> lstRes = null;
            if (!string.IsNullOrEmpty(departmentname))
                lstRes = BOrder.SearchByContact(departmentname);
            else
                lstRes = BOrder.SearchAll();

            var total = lstRes.Count;
            var rows = lstRes.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete()
        {
            string name = Request.Form["pId"];

            List<DeleteId> models = LitJson.JsonMapper.ToObject<List<DeleteId>>(name);
            if (models == null)
            {
                return Content(ReturnMsg.fail.ToString());
            }

            bool isSucc = BOrder.Delete(models.Select(_ => _.Id).ToList());
            if (isSucc)
            {
                return Content(ReturnMsg.success.ToString());
            }
            else
            {
                return Content(ReturnMsg.fail.ToString());
            }
        }

        [Serializable]
        public class DeleteId
        {
            public int Id { get; set; }
        }
    }
}
