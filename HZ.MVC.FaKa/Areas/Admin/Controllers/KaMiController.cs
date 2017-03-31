using HZ.MVC.FaKa.Areas.Admin.Models;
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
            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Content(LitJson.JsonMapper.ToJson(lstRes));
        }
    }
}
