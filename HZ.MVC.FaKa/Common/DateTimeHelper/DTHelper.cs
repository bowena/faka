using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZ.MVC.FaKa.Common
{
    public class DTHelper
    {
        static string[] RandomArr = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        public static string GetCurrentTimeOrderNo()
        {
            Random random = new Random();

            string tempStr = "";
            for (int i = 0; i < 3; i++)
            {
                int randomIndex = random.Next(RandomArr.Length);
                tempStr += RandomArr[randomIndex];
            }

            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds; // 相差毫秒数

            return tempStr.ToUpper() + timeStamp.ToString();
        }
    }
}