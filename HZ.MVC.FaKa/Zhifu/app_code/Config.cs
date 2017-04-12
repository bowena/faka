using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Web;

/// <summary>
/// 基础配置类
/// </summary>
namespace Com.Alipay
{
    public class Config
    {
        public static string alipay_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAi+vNqiBJSFk7lqI1gq9evXBRgoYorjM4xgelfax+FL8i4EkA6/dCbvjp6uLDxm+i2sbtxStLkKbHYGQcMyftBHYIgS03TUwZq42TiWtzd0hAlpr3splqiTrasye4ACD5WHS45H4v/DfyxF4RU2+WSli4HlZB75JpDikNLsHstHDKMZtvS7GIB7YuiDutLmoJz/MoDg8Q5veXwQ38C8Zj/CtbERvdYFPhf+8YcOrqB9m2DEcBHlaoWUQgLb7kYCp3wssOG4ZbeWfoIGGPfUc+9kEIc31k3/I/+XodToqkAZQ1Hz3H4CRX5iNXBGl2rQCCtDmrdcMdyM5GZ5IU0iVL6QIDAQAB";
        
        
        //这里要配置没有经过的原始私钥

        //开发者私钥
        public static string merchant_private_key = @"MIIEpAIBAAKCAQEAlwRoFZthA9kBlacJceD7pIvbOhEcIa7B+rPndLoe54EY2cuN4DDD0foWtWPmEcOZly1YTaN3xu+PeDlZIFEUhYr//gPIyRl3gBheLfXrx+ZIRN5kmnlcebdRS4HYn4/Wwvbj5gvaOWL4Lr1036wi6PlNMs3ufUBGJ3vc9W7b4CtwkRQBxpvSMM+sD2b7N7LPw6Qgze0tVc/wb4wChE6DinO4BGiNXiAQS+eN0ohT8rv5aYU4BHC9NK9fSFja9xGzZTVsMhPy9Xe70DHmDa/Wqt1vUeecUt7LC50a1dqbsDNRT3HspOV2gBwBj/3YBbkBHYxwDgYJ3HuF0E8ESv28+wIDAQABAoIBAA0DLTDHbamWNkO929t7JlO8VUyAkur4EvDWNZhiPS13ezuxBW2O5iCeqvxAOl/HeeGD37r43eY/WB5k2qAlPF5xZrNtggyJ7DkWYG8XZJSZ7Bo7C3IgCO7X8+JMsuy0yS2Ndn+o/8m4FGh6nKp1O6DVcmhxRB54vlLConxna/y5v5sIDgRlp5MG/Soya8pQz5VqKVO/s+0SJ4oRGBg7gOiCrP4gn47gWYRsERG0HBWNVNoEJw8GOigTasCK4WpHk0k4J7eL90sNnBnv/0LlycZdFOFC1jXgLJNzs3uZYAArZXlR9Ikc+Eh/ZTFdBA5Fsrh4IgJtpT/c88K9NxhzAIkCgYEAx2W9AzA2loP+M7AF7cngRP8j6SFqgEG77RCSEPfkr4YoH5AIPGCkADpzt/2yQi5vk1Der7dkpk6iwDdmHs4vDWWxOpzDXj47ejNU7yFTNeHNYNRFhIlKnVXx8Cj79kiTe31tywsjcGSOkAoRC13/OmnPzpz4tv9Qdw6tiEoWlH8CgYEAweLd0KTTk66yXM0bDsFvoNW9P5yBmlILvBiiiFv4Qrim65sQOc1MiPvY3K/tJ2gGecz8+whmHTtL0xDfyZI91ZugQhko680XNinv/ugkvw++oETMJC00gPWfPzKn/d8mz2VU4Ja+MqZJ3WIaEaPiKz6xDH6fW8WKRpOHBFZl6YUCgYEAxlB0naAtGqQqhgPS0b5PlK/hanO2tdsq6kvpyoCSbNRUQZgB6PMBCc0Y3GyxB0uh4vWylTckHpflHKG8qnTMrVlv3GifkrL4esBk//S4CsYKevyEK55UnIknOIG6nydtr4t/UEtCsv0AIVgxvYl+V/13FfpfD7p8r/zkMMHnFUcCgYAp+6UKW9aQoQBwRd15CXvReNbc4lfSAbgWx8LSAhA06mUCmXYe/gx3kQk5aELTCJx2XWPeO8hYxhE6J+o9bJRFH8jI50fMH4HKMbSNHtluIcumSp9lqgA3eHg+KArmVx7BQ/DoHNwcfZN7A5YfgbwknxOZYAXDyneRprpSvx6cPQKBgQCPi7faZGLvTSBaBFNC7byt9N4DABhLsJ9Xeq/TOMLW7lL10BA1L2Qh5Vq1AW7V+uSo8sMKveXmgC7qLuyR9Osf46fcS8SGmwvnLHLGPZZVN3L3DBrrwQRy+i2eod32TzZVnpCw61jQ94lCBgsf5VTnhx3WC+Fl8YkQs6Zy27+ufA==";
        
        //开发者公钥
        public static string merchant_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAlwRoFZthA9kBlacJceD7pIvbOhEcIa7B+rPndLoe54EY2cuN4DDD0foWtWPmEcOZly1YTaN3xu+PeDlZIFEUhYr//gPIyRl3gBheLfXrx+ZIRN5kmnlcebdRS4HYn4/Wwvbj5gvaOWL4Lr1036wi6PlNMs3ufUBGJ3vc9W7b4CtwkRQBxpvSMM+sD2b7N7LPw6Qgze0tVc/wb4wChE6DinO4BGiNXiAQS+eN0ohT8rv5aYU4BHC9NK9fSFja9xGzZTVsMhPy9Xe70DHmDa/Wqt1vUeecUt7LC50a1dqbsDNRT3HspOV2gBwBj/3YBbkBHYxwDgYJ3HuF0E8ESv28+wIDAQAB";

        //应用ID
        public static string appId = "2017041006616768";

        //合作伙伴ID：partnerID
        public static string pid = "2088621592358744";
        

        //支付宝网关
        public static string serverUrl = "https://openapi.alipay.com/gateway.do";
        public static string mapiUrl = "https://mapi.alipay.com/gateway.do";
        public static string monitorUrl = "http://mcloudmonitor.com/gateway.do";

        //编码，无需修改
        public static string charset = "utf-8";
        //签名类型，支持RSA2（推荐！）、RSA
        public static string sign_type = "RSA2";
        //public static string sign_type = "RSA";
        //版本号，无需修改
        public static string version = "1.0";
         

        /// <summary>
        /// 公钥文件类型转换成纯文本类型
        /// </summary>
        /// <returns>过滤后的字符串类型公钥</returns>
        public static string getMerchantPublicKeyStr()
        {
            StreamReader sr = new StreamReader(merchant_public_key);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
              pubkey=  pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
              pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
              pubkey = pubkey.Replace("\r", "");
              pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }

        /// <summary>
        /// 私钥文件类型转换成纯文本类型
        /// </summary>
        /// <returns>过滤后的字符串类型私钥</returns>
        public static string getMerchantPriveteKeyStr()
        {
            StreamReader sr = new StreamReader(merchant_private_key);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }

    }
}