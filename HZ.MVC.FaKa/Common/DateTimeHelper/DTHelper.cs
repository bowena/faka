using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZ.MVC.FaKa.Common
{
    public class DTHelper
    {
        static string[] RandomArr = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        public static string GetCurrentTimeString()
        {
            int ranNum = new Random().Next(5);
            string tempStr = "";
            for (int i = 0; i < ranNum; i++)
            {
                tempStr += RandomArr[i];
            }
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + tempStr;
        }
    }
}