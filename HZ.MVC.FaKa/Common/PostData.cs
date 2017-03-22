using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace HZ.MVC.FaKa.Common
{
    public class PostData
    {
        SortedDictionary<string, string> sortedDic = new SortedDictionary<string, string>();
        const string orderUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        public void Add(string key, string value)
        {
            if (!sortedDic.ContainsKey(key))
                sortedDic.Add(key, value);
        }

        public void Remove(string key)
        {
            if (sortedDic.ContainsKey(key))
                sortedDic.Remove(key);
        }

        /// <summary>
        /// 获取签名字符串
        /// </summary>
        /// <returns></returns>
        public string GetSignValue()
        {
            if (sortedDic.Count > 0)
            {
                //第一步
                string resultStr = "";
                foreach (var item in sortedDic)
                {
                    resultStr += item.Key + "=" + item.Value + "&";
                }

                //第二步
                string apiKey = "aDQ0u09o506q606o5059O5SU5zO0uU6s";
                string stringSignTemp = resultStr + "key=" + apiKey;
                string signValue = MyEncrypt.MD5Encrypt(stringSignTemp).ToUpper();

                return signValue;
            }
            else
            {
                return base.ToString();
            }
        }

        public override string ToString()
        {
            if (sortedDic.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<xml>");
                foreach (var item in sortedDic)
                {
                    builder.Append("<" + item.Key + ">").Append(item.Value).Append("</" + item.Key + ">");
                }
                builder.Append("</xml>");
                return builder.ToString();
            }
            return base.ToString();
        }

        public string Post()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(orderUrl);
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            req.Method = "POST";
            req.ContentType = "text/xml";
            string data = this.ToString();

            if(!string.IsNullOrEmpty(data))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                using (Stream stream = req.GetRequestStream())
                {
                    stream.Write(buffer, 0, buffer.Length);                    
                }
            }

            HttpWebResponse response = (HttpWebResponse)req.GetResponse();

            using (Stream resStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(resStream))
                {
                    string resStr = reader.ReadToEnd();
                }
            }

            return "";
        }
    }
}