using HZ.MVC.FaKa.Areas.Admin.Models;
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
            ProductTypeViewModel model = new ProductTypeViewModel();
            model.ProductName = name;
            model.UpdateTime = DateTime.Now;
            bool isSucc = BProductType.Insert(model);
            if (isSucc)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        public ActionResult Delete()
        {
            string name = Request.Form["pId"];

            List<DeleteId> models = LitJson.JsonMapper.ToObject<List<DeleteId>>(name);
            if (models == null)
            {
                return Content("fail");
            }

            bool isSucc = BProductType.Delete(models.Select(_ => _.Id).ToList());
            if (isSucc)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        public ActionResult Update()
        {
            string newName = Request.Form["proM"];

            ProductTypeViewModel model = LitJson.JsonMapper.ToObject<ProductTypeViewModel>(newName);
            model.UpdateTime = DateTime.Now;
            if (model == null)
            {
                return Content("fail");
            }
            if (BProductType.Update(model))
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        [Serializable]
        public class DeleteId
        {
            public int Id { get; set; }
        }
    }
}
