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
    public class KaMiController : Controller
    {
        //
        // GET: /Admin/KaMi/

        public ActionResult Index()
        {
            List<ProductTypeViewModel> lstRes = BProductType.SearchAll();
            ViewBag.option = lstRes;
            ViewBag.title = "卡密管理";
            return View();
        }

        public ActionResult InitProduct()
        {
            string typeId = Request.Form["proT"];
            List<ProductViewModel> lstRes = BProduct.SearchByTypeId(typeId);
            return Content(LitJson.JsonMapper.ToJson(lstRes));
        }

        public JsonResult GetKaMis(int limit, int offset, string departmentname)
        {
            List<KaMiViewModel> lstRes = null;
            if (!string.IsNullOrEmpty(departmentname))
                lstRes = BKaMi.SearchBysql(departmentname);
            else
                lstRes = BKaMi.SearchAll();

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
            KaMiViewModel model = LitJson.JsonMapper.ToObject<KaMiViewModel>(name);
            if (model == null)
            {
                return Content(ReturnMsg.fail.ToString());
            }
            model.AddedTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            bool isSucc = BKaMi.Insert(model);
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

            KaMiViewModel model = LitJson.JsonMapper.ToObject<KaMiViewModel>(newName);
            model.UpdateTime = DateTime.Now;
            if (model == null)
            {
                return Content(ReturnMsg.fail.ToString());
            }
            if (string.IsNullOrEmpty(model.Content))
            {
                return Content(ReturnMsg.empty.ToString());
            }
            if (BKaMi.Update(model))
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

            bool isSucc = BKaMi.Delete(models.Select(_ => _.Id).ToList());
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
