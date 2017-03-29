using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZ.MVC.FaKa.Areas.Admin.Models
{
    public class ProductTypeViewModel
    {
        public int Id
        {
            get;
            set;
        }
        public string ProductName
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