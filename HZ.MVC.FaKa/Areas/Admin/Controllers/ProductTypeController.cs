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
    public class ProductTypeController : Controller
    {
        //
        // GET: /Admin/ProductType/

        public ActionResult Index()
        {
            if (Session["admin_User"] == null)
            {
                return RedirectToRoute(new { controller = "Account", action = "Login", area = "" });
            }
            ViewBag.title = "类型管理";

            return View();
        }

        public JsonResult GetProductTypes(int limit, int offset, string departmentname)
        {
            List<ProductTypeViewModel> lstRes = null;
            if (!string.IsNullOrEmpty(departmentname))
                lstRes = BProductType.SearchBysql(departmentname);
            else
                lstRes = BProductType.SearchAll();

            var total = lstRes.Count;
            var rows = lstRes.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            string name = Request.Form["proT"];
            if(string.IsNullOrEmpty(name))
            {
                return Content(ReturnMsg.empty.ToString());
            }
            ProductTypeViewModel model = new ProductTypeViewModel();
            model.ProductName = name;
            model.UpdateTime = DateTime.Now;
            bool isSucc = BProductType.Insert(model);
            if (isSucc)
            {
                return Content(ReturnMsg.success.ToString());
            }
            else
            {
                return Content(ReturnMsg.fail.ToString());
            }
        }

        public ActionResult Delete()
        {
            string name = Request.Form["pId"];

            List<DeleteId> models = LitJson.JsonMapper.ToObject<List<DeleteId>>(name);
            if (models == null)
            {
                return Content(ReturnMsg.fail.ToString());
            }

            bool isSucc = BProductType.Delete(models.Select(_ => _.Id).ToList());
            if (isSucc)
            {
                return Content(ReturnMsg.success.ToString());
            }
            else
            {
                return Content(ReturnMsg.fail.ToString());
            }
        }

        public ActionResult Update()
        {
            string newName = Request.Form["proM"];

            ProductTypeViewModel model = LitJson.JsonMapper.ToObject<ProductTypeViewModel>(newName);
            model.UpdateTime = DateTime.Now;
            if (model == null)
            {
                return Content(ReturnMsg.fail.ToString());
            }
            if (string.IsNullOrEmpty(model.ProductName))
            {
                return Content(ReturnMsg.empty.ToString());
            }
            if (BProductType.Update(model))
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
