﻿using Com.Alipay;
using HZ.MVC.FaKa.Areas.Admin.Models;
using HZ.MVC.FaKa.BLL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HZ.MVC.FaKa.Zhifu
{
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                //Notify aliNotify = new Notify();
                Notify aliNotify = new Notify(Config.charset, Config.sign_type, Config.pid, Config.mapiUrl, Config.alipay_public_key);

                //对异步通知进行验签
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);
                //对验签结果
                //bool isSign = Aop.Api.Util.AlipaySignature.RSACheckV2(sPara, Config.alipay_public_key ,Config.charset,Config.sign_type,false );

                if (verifyResult && CheckParams()) //验签成功 && 关键业务参数校验成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号
                    string out_trade_no = Request.Form["out_trade_no"];


                    //支付宝交易号
                    string trade_no = Request.Form["trade_no"];

                    //交易状态
                    //在支付宝的业务通知中，只有交易通知状态为TRADE_SUCCESS或TRADE_FINISHED时，才是买家付款成功。
                    string trade_status = Request.Form["trade_status"];

                    if (trade_status.ToUpper() == "TRADE_SUCCESS" || trade_status.ToUpper() == "TRADE_FINISHED")
                    {
                        //查询订单是否存在
                        OrderViewModel model = BOrder.SearchByTradeNo(out_trade_no);
                        if (model != null)
                        {
                            //存在
                            BOrder.Update("update Orders set LocalStatus='已付款' where Id=" + model.Id);
                        }
                    }
                    //判断是否在商户网站中已经做过了这次通知返回的处理
                    //如果没有做过处理，那么执行商户的业务程序
                    //如果有做过处理，那么不执行商户的业务程序

                    Response.Write("success");  //请不要修改或删除

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }

        /// <summary>
        /// 对支付宝异步通知的关键参数进行校验
        /// </summary>
        /// <returns></returns>
        private bool CheckParams()
        {
            bool ret = true;

            //获得商户订单号out_trade_no
            string out_trade_no = Request.Form["out_trade_no"];
            //TODO 商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，

            //获得支付总金额total_amount
            string total_amount = Request.Form["total_amount"];
            //TODO 判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），

            //获得卖家账号seller_email
            string seller_email = Request.Form["seller_email"];
            //TODO 校验通知中的seller_email（或者seller_id) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id / seller_email）

            //获得调用方的appid；
            //如果是非授权模式，appid是商户的appid；如果是授权模式（token调用），appid是系统商的appid
            string app_id = Request.Form["app_id"];
            //TODO 验证app_id是否是调用方的appid；。

            //验证上述四个参数，完全吻合则返回参数校验成功
            return ret;

        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
    }
}