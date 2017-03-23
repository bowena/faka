using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

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

        public void SetValue(string key, string value)
        {
            sortedDic[key] = value;
        }

        public void Remove(string key)
        {
            if (sortedDic.ContainsKey(key))
                sortedDic.Remove(key);
        }

        public string GetValue(string key)
        {
            string o = null;
            sortedDic.TryGetValue(key, out o);
            return o;
        }

        public bool IsSet(string key)
        {
            string o = null;
            sortedDic.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
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

        public SortedDictionary<string, string> FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new Exception("将空的xml串转换为WxPayData不合法!");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                sortedDic[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }

            try
            {
                //错误是没有签名
                if (sortedDic["return_code"] != "SUCCESS")
                {
                    return sortedDic;
                }
                CheckSign();//验证签名,不通过会抛异常
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return sortedDic;
        }

        /**
       * 
       * 检测签名是否正确
       * 正确返回true，错误抛异常
       */
        public bool CheckSign()
        {
            //如果没有设置签名，则跳过检测
            if (!IsSet("sign"))
            {
                throw new Exception("WxPayData签名存在但不合法!");
            }
            //如果设置了签名但是签名为空，则抛异常
            else if (GetValue("sign") == null || GetValue("sign").ToString() == "")
            {
                throw new Exception("WxPayData签名存在但不合法!");
            }

            //获取接收到的签名
            string return_sign = GetValue("sign").ToString();

            //在本地计算新的签名
            string cal_sign = GetSignValue();

            if (cal_sign == return_sign)
            {
                return true;
            }

            throw new Exception("WxPayData签名验证错误!");
        }

        public SortedDictionary<string, string> Post()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(orderUrl);
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            req.Method = "POST";
            req.ContentType = "text/xml";
            string data = this.ToString();

            if (!string.IsNullOrEmpty(data))
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
                    return FromXml(resStr);
                }
            }
        }
    }
}