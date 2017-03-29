using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZ.MVC.FaKa.Areas.Admin.Models
{
    public class OrderViewModel
    {
        public int Id
        {
            get;
            set;
        }
        public string NO
        {
            get;
            set;
        }
        public int Product_Id
        {
            get;
            set;
        }
        public string Type
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }
        public double Price
        {
            get;
            set;
        }
        public int Count
        {
            get;
            set;
        }
        public string LocalStatus
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }
        public DateTime UpdateTime
        {
            get;
            set;
        }
    }
}