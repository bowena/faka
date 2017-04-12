using Com.Alipay;
using Com.Alipay.Business;
using Com.Alipay.Domain;
using Com.Alipay.Model;
using F2FPay;
using HZ.MVC.FaKa.Areas.Admin.Models;
using HZ.MVC.FaKa.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThoughtWorks.QRCode.Codec;

namespace HZ.MVC.FaKa.Zhifu
{
    public partial class precreate : System.Web.UI.Page
    {
        private LogHelper log = new LogHelper(AppDomain.CurrentDomain.BaseDirectory + "/log/log.txt");

        IAlipayTradeService serviceClient = F2FBiz.CreateClientInstance(Config.serverUrl, Config.appId, Config.merchant_private_key, Config.version,
                             Config.sign_type, Config.alipay_public_key, Config.charset);
        protected void Page_Load(object sender, EventArgs e)
        {
            Alipay_RSA_Submit(sender, e);
        }

        protected void Alipay_RSA_Submit(object sender, EventArgs e)
        {

            AlipayTradePrecreateContentBuilder builder = BuildPrecreateContent();
            string out_trade_no = builder.out_trade_no;

            //如果需要接收扫码支付异步通知，那么请把下面两行注释代替本行。
            //推荐使用轮询撤销机制，不推荐使用异步通知,避免单边账问题发生。
            //AlipayF2FPrecreateResult precreateResult = serviceClient.tradePrecreate(builder);
            string notify_url = "http://www.51facaile.top/Zhifu/notify_url.aspx";  //商户接收异步通知的地址
            AlipayF2FPrecreateResult precreateResult = serviceClient.tradePrecreate(builder, notify_url);

            //以下返回结果的处理供参考。
            //payResponse.QrCode即二维码对于的链接
            //将链接用二维码工具生成二维码打印出来，顾客可以用支付宝钱包扫码支付。
            string result = "";

            switch (precreateResult.Status)
            {
                case ResultEnum.SUCCESS:
                    DoWaitProcess(precreateResult);
                    break;
                case ResultEnum.FAILED:
                    result = precreateResult.response.Body;
                    Response.Write("<script>alert('" + result + "')</script>");
                    break;

                case ResultEnum.UNKNOWN:
                    if (precreateResult.response == null)
                    {
                        result = "配置或网络异常，请检查后重试";
                    }
                    else
                    {
                        result = "系统异常，请更新外部订单后重新发起请求";
                    }
                    Response.Write("<script>alert('" + result + "')</script>");
                    break;
            }

        }

        /// <summary>
        /// 构造支付请求数据
        /// </summary>
        /// <returns>请求数据集</returns>
        private AlipayTradePrecreateContentBuilder BuildPrecreateContent()
        {
            //线上联调时，请输入真实的外部订单号。
            string out_trade_no = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString();
            string total_fee = "1";
            string subject = "test";

            if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
            {
                out_trade_no = Request.QueryString["tid"];
            }

            OrderViewModel order = BOrder.SearchByTradeNo(out_trade_no);
            if (order != null)
            {
                subject = BProduct.ExecuteSql("select Name from Products where Id=" + order.Product_Id).ToString();
                total_fee = (order.Price * order.Count).ToString();
            }
            //if (String.IsNullOrEmpty(WIDout_request_no.Text.Trim()))
            //{
            //    out_trade_no = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString();
            //}
            //else
            //{
            //out_trade_no = "WF87212910290";
            //}

            AlipayTradePrecreateContentBuilder builder = new AlipayTradePrecreateContentBuilder();
            //收款账号
            builder.seller_id = Config.pid;
            //订单编号
            builder.out_trade_no = out_trade_no;
            //订单总金额
            builder.total_amount = total_fee;
            //参与优惠计算的金额
            //builder.discountable_amount = "";
            //不参与优惠计算的金额
            //builder.undiscountable_amount = "";
            //订单名称
            builder.subject = subject;
            //自定义超时时间
            builder.timeout_express = "5m";
            //订单描述
            builder.body = "";
            //门店编号，很重要的参数，可以用作之后的营销
            builder.store_id = "test store id";
            //操作员编号，很重要的参数，可以用作之后的营销
            builder.operator_id = "test";

            //传入商品信息详情
            List<GoodsInfo> gList = new List<GoodsInfo>();
            if (order != null)
            {
                GoodsInfo goods = new GoodsInfo();
                goods.goods_id = order.Product_Id.ToString();
                goods.goods_name = subject;
                goods.price = order.Price.ToString();
                goods.quantity = order.Count.ToString(); ;
                gList.Add(goods);
            }
            else
            {
                GoodsInfo goods = new GoodsInfo();
                goods.goods_id = "520";
                goods.goods_name = "捐助";
                goods.price = "0.1";
                goods.quantity = "1";
                gList.Add(goods);
            }

            builder.goods_detail = gList;

            //系统商接入可以填此参数用作返佣
            //ExtendParams exParam = new ExtendParams();
            //exParam.sysServiceProviderId = "20880000000000";
            //builder.extendParams = exParam;

            return builder;

        }

        /// <summary>
        /// 生成二维码并展示到页面上
        /// </summary>
        /// <param name="precreateResult">二维码串</param>
        private void DoWaitProcess(AlipayF2FPrecreateResult precreateResult)
        {
            //打印出 preResponse.QrCode 对应的条码
            Bitmap bt;
            string enCodeString = precreateResult.response.QrCode;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            qrCodeEncoder.QRCodeScale = 3;
            qrCodeEncoder.QRCodeVersion = 8;
            bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);
            string filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString()
             + ".jpg";
            bt.Save(Server.MapPath("images/") + filename);
            //this.Image1.ImageUrl = "images/" + filename;
            Bitmap image = new Bitmap(Server.MapPath("images/") + filename);
            //清除该页输出缓存，设置该页无缓存
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");
            //将验证码图片写入内存流，并将其以"image/Jpeg" 格式输出
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(ms.ToArray());
            //轮询订单结果
            //根据业务需要，选择是否新起线程进行轮询
            //ParameterizedThreadStart ParStart = new ParameterizedThreadStart(LoopQuery);
            //Thread myThread = new Thread(ParStart);
            //object o = precreateResult.response.OutTradeNo;
            //myThread.Start(o);

        }

        /// <summary>
        /// 轮询
        /// </summary>
        /// <param name="o">订单号</param>
        public void LoopQuery(object o)
        {
            AlipayF2FQueryResult queryResult = new AlipayF2FQueryResult();
            int count = 100;
            int interval = 10000;
            string out_trade_no = o.ToString();

            for (int i = 1; i <= count; i++)
            {
                Thread.Sleep(interval);
                queryResult = serviceClient.tradeQuery(out_trade_no);
                if (queryResult != null)
                {
                    if (queryResult.Status == ResultEnum.SUCCESS)
                    {
                        DoSuccessProcess(queryResult);
                        return;
                    }
                }
            }
            DoFailedProcess(queryResult);
        }

        /// <summary>
        /// 请添加支付成功后的处理
        /// </summary>
        private void DoSuccessProcess(AlipayF2FQueryResult queryResult)
        {
            //支付成功，请更新相应单据
            log.WriteLine("扫码支付成功：外部订单号" + queryResult.response.OutTradeNo);

        }

        /// <summary>
        /// 请添加支付失败后的处理
        /// </summary>
        private void DoFailedProcess(AlipayF2FQueryResult queryResult)
        {
            //支付失败，请更新相应单据
        }
    }
}