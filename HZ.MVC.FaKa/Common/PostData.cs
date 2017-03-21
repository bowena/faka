using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZ.MVC.FaKa.Common
{
    public class PostData
    {
        SortedDictionary<string, string> sortedDic = new SortedDictionary<string, string>();

        public void Add(string key, string value)
        {
            if (!sortedDic.ContainsKey(key))
                sortedDic.Add(key, value);
        }

        public override string ToString()
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
                string stringSignTemp = resultStr + "key=" +apiKey;
                string signValue = MyEncrypt.MD5Encrypt(stringSignTemp).ToUpper();


                //第三步

            }
            else
            {
                return base.ToString();
            }
        }
    }
}