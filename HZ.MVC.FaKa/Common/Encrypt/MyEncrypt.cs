using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace HZ.MVC.FaKa.Common
{
    public class MyEncrypt
    {
        public static string MD5Encrypt(String strSource)
        {
            MD5 md5 = MD5.Create();
            String result = String.Empty;
            byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strSource));
            for (int i = 0; i < data.Length; i++)
            {
                result += data[i].ToString("x2");
            }
            return result;
        }
    }
}