using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZ.MVC.FaKa.Models.WxPayPost
{
    public class PostModel
    {
        /// <summary>
        /// 必填：是
        /// 应用ID 微信开放平台审核通过的应用APPID
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 必填：是
        /// 商户号 微信支付分配的商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 必填：否
        /// 设备号 	终端设备号(门店号或收银设备ID)，默认请传"WEB"
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 必填：是      
        /// 随机字符串 随机字符串，不长于32位。推荐随机数生成算法
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 必填：是
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 必填：否
        /// 签名类型 签名类型，目前支持HMAC-SHA256和MD5，默认为MD5
        /// </summary>
        public string sign_type { get; set; }

        /// <summary>
        /// 必填：是
        /// 商品描述 商品描述交易字段格式根据不同的应用场景按照以下格式：APP——需传入应用市场上的APP名字-实际商品名称，天天爱消除-游戏充值。
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 必填：否
        /// 商品详情 商品详细列表，使用Json格式，传输签名前请务必使用CDATA标签将JSON文本串保护起来。
        /// </summary>
        public string detail { get; set; }

        /// <summary>
        /// 必填：否
        /// 附加数据 附加数据，在查询API和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 必填：是
        /// 商户订单号 商户系统内部的订单号,32个字符内、可包含字母, 其他说明见商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 必填：否
        /// 货币类型 符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型
        /// </summary>
        public string fee_type { get; set; }

        /// <summary>
        /// 必填：是
        /// 总金额 订单总金额，单位为分，详见支付金额
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 必填：是
        /// 终端IP 用户端实际ip
        /// </summary>
        public string spbill_create_ip { get; set; }

        /// <summary>
        /// 必填：否
        /// 交易起始时间 订单生成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。其他详见时间规则
        /// </summary>
        public string time_start { get; set; }

        /// <summary>
        /// 必填：否
        /// 交易结束时间 订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。其他详见时间规则 注意：最短失效时间间隔必须大于5分钟
        /// </summary>
        public string time_expire { get; set; }

        /// <summary>
        /// 必填：否
        /// 商品标记 商品标记，代金券或立减优惠功能的参数，说明详见代金券或立减优惠
        /// </summary>
        public string goods_tag { get; set; }

        /// <summary>
        /// 必填：是
        /// 通知地址 接收微信支付异步通知回调地址，通知url必须为直接可访问的url，不能携带参数。
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 必填：是
        /// 交易类型 支付类型
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /// 必填：否
        /// 指定支付方式 no_credit--指定不能使用信用卡支付
        /// </summary>
        public string limit_pay { get; set; }

        /*示例：
          <xml>
            <appid>wx2421b1c4370ec43b</appid>
            <attach>支付测试</attach>
            <body>APP支付测试</body>
            <mch_id>10000100</mch_id>
            <nonce_str>1add1a30ac87aa2db72f57a2375d8fec</nonce_str>
            <notify_url>http://wxpay.wxutil.com/pub_v2/pay/notify.v2.php</notify_url>
            <out_trade_no>1415659990</out_trade_no>
            <spbill_create_ip>14.23.150.211</spbill_create_ip>
            <total_fee>1</total_fee>
            <trade_type>APP</trade_type>
            <sign>0CB01533B8C1EF103065174F50BCA001</sign>
          </xml>
         
         返回成功示例：
         <xml>
            <return_code><![CDATA[SUCCESS]]></return_code>
            <return_msg><![CDATA[OK]]></return_msg>
            <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
            <mch_id><![CDATA[10000100]]></mch_id>
            <nonce_str><![CDATA[IITRi8Iabbblz1Jc]]></nonce_str>
            <sign><![CDATA[7921E432F65EB8ED0CE9755F0E86D72F]]></sign>
            <result_code><![CDATA[SUCCESS]]></result_code>
            <prepay_id><![CDATA[wx201411101639507cbf6ffd8b0779950874]]></prepay_id>
            <trade_type><![CDATA[APP]]></trade_type>
        </xml> 
         
         * */
    }
}