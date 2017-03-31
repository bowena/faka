using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZ.MVC.FaKa.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public double Price
        {
            get;
            set;
        }
        public int ProductType_Id
        {
            get;
            set;
        }

        public string ProductTypeName
        {
            get;
            set;
        }

        public DateTime AddedTime
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