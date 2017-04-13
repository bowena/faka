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
    public class ProductController : Controller
    {
        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            if (Session["admin_User"] == null)
            {
                return RedirectToRoute(new { controller = "Account", action = "Login", area = "" });
            }
            List<ProductTypeViewModel> lstRes = BProductType.SearchAll();
            ViewBag.option = lstRes;
            ViewBag.title = "产品管理";

            return View();
        }

        public JsonResult GetProducts(int limit, int offset, string departmentname)
        {
            List<ProductViewModel> lstRes = null;
            if (!string.IsNullOrEmpty(departmentname))
                lstRes = BProduct.SearchBysql(departmentname);
            else
                lstRes = BProduct.SearchAll();

            var total = lstRes.Count;
            var rows = lstRes.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            string name = Request.Form["proT"];
            if (string.IsNullOrEmpty(name))
            {
                return Content(ReturnMsg.empty.ToString());
            }
            ProductViewModel model = LitJson.JsonMapper.ToObject<ProductViewModel>(name);
            if (model == null)
            {
                return Content(ReturnMsg.fail.ToString());
            }
            model.AddedTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            bool isSucc = BProduct.Insert(model);
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

            ProductViewModel model = LitJson.JsonMapper.ToObject<ProductViewModel>(newName);
            model.UpdateTime = DateTime.Now;
            if (model == null)
            {
                return Content(ReturnMsg.fail.ToString());
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                return Content(ReturnMsg.empty.ToString());
            }
            if (BProduct.Update(model))
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

            bool isSucc = BProduct.Delete(models.Select(_ => _.Id).ToList());
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
