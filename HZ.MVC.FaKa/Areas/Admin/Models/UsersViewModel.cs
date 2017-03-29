using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZ.MVC.FaKa.Areas.Admin.Models
{
    public class UsersViewModel
    {
        public int Id
        {
            get;
            set;
        }
        public string userName
        {
            get;
            set;
        }
        public string password
        {
            get;
            set;
        }
        public int isRemmber
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