using HZ.MVC.FaKa.Areas.Admin.Models;
using HZ.MVC.FaKa.BLL;
using HZ.MVC.FaKa.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZ.MVC.FaKa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var allPro = GetAllProducts();
            ViewBag.pros = allPro;

            ViewBag.Message = "一、支付宝付款后不要动，20秒左右自动跳转到卡密页面" + "\r\n" +
                              "二、微信付款后等待10秒左右，点击已完成付款 查询卡密" + "\r\n" +
                              "如果没法识别扫描，请截图保存去扫一扫里添加相片扫描";
            ViewBag.ZhuYi = "温馨提示：" + "\n" +
                            "1.可以不用注册，可直接付款购买，自动发货！无需等待即买即看" + "\n" +
                            "2.万一你付款成功网页被你关了，找到订单，拿订单号到网页下方去提取" + "\n" +
                            "3.为了保护您账号安全，会定期删档，买号请收藏，不可分享不然封停后果自负" + "\n" +
                            "4.如有问题联系客服QQ：43854337 没有订单号不售后，没有库存联系客服上货" + "\n" +
                            "诚邀各路大牛共造双赢，一手货源，实力批发，诚信经营" + "\n" +
                            "本店专业批发各大影视Vip、各种业务卡密、QQ号等" + "\n" +
                            "有实力可以跑量的联系我，量大价格肯定美丽！" + "\n" +
                            "购买5件起，可享受批发价！提交订单查看价格";
            return View();
        }

        private List<ProductViewModel> GetAllProducts()
        {
            List<ProductViewModel> products = BProduct.SearchAll();
            return products;
        }

        public ActionResult About()
        {
            ViewBag.Message = "<p>一、支付宝付款后不要动，20秒左右自动跳转到卡密页面</p>" +
                              "<p>二、微信付款后等待10秒左右，点击已完成付款 查询卡密</p>" +
                              "<p>如果没法识别扫描，请截图保存去扫一扫里添加相片扫描</p>";
            ViewBag.ZhuYi = "<p>温馨提示：</p>" +
                            "<p>1.可以不用注册，可直接付款购买，自动发货！无需等待即买即看</p>" +
                            "<p>2.万一你付款成功网页被你关了，找到订单，拿订单号到网页下方去提取</p>" +
                            "<p>3.为了保护您账号安全，会定期删档，买号请收藏，不可分享不然封停后果自负</p>" +
                            "<p>4.如有问题联系客服QQ：43854337 没有订单号不售后，没有库存联系客服上货</p>" +
                            "<p>诚邀各路大牛共造双赢，一手货源，实力批发，诚信经营</p>" +
                            "<p>本店专业批发各大影视Vip、各种业务卡密、QQ号等</p>" +
                            "<p>有实力可以跑量的联系我，量大价格肯定美丽！</p>" +
                            "<p>购买5件起，可享受批发价！提交订单查看价格</p>";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Order()
        {
            


            return View();
        }

        /// <summary>
        ///  确定订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Confirm()
        {
            string email = Request.QueryString["mail"];
            string id = Request.QueryString["id"];
            string num = Request.QueryString["num"];
            string random = Request.QueryString["t"];
            
            ProductViewModel p = BProduct.SearchById(id);
            if(p==null)
            {
                return View();
            }
            double totalMoney = Convert.ToInt32(num) * p.Price;

            ViewBag.Email = email;
            ViewBag.Id = id;
            ViewBag.Num = num;
            ViewBag.Total = totalMoney;
            ViewBag.Name = p.Name;
            ViewBag.OrderNO = DTHelper.GetCurrentTimeOrderNo();
            ViewBag.SinglePrice = p.Price;
            return View();
        }
    }
}
