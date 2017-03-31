using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZ.MVC.FaKa.Areas.Admin.Models
{
    public class KaMiViewModel
    {
        public int Id
        {
            get;
            set;
        }
        public string Content
        {
            get;
            set;
        }
        public int State
        {
            get;
            set;
        }
        public int Product_Id
        {
            get;
            set;
        }
        public int ProductType_Id
        {
            get;
            set;
        }
        public string Remark
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