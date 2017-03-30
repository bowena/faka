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

        public JsonResult GetProductTypes(int limit, int offset, string departmentname, string statu)
        {
            var lstRes = BProductType.SearchAll();

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
            if(isSucc)
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
            //BProductType.Insert()
            string name = Request.Form["pId"];

            List<DeleteId> models = LitJson.JsonMapper.ToObject<List<DeleteId>>(name);

            return View();
        }

        public ActionResult Update()
        {
            //BProductType.Insert()

            return View();
        }

        [Serializable]
        public class DeleteId
        {
            public int Id { get; set; }
        }
    }
}
