using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class CustomerInfo
    {
        public int id { set; get; }
        public int cu_login_id { set; get; }
        public string name { set; get; }
        public string sex { set; get; }
        public string birthday { set; get; }
        public string bdtype { set; get; }
        public string phone { set; get; }
        public string addr { set; get; }
        public string headimgurl { set; get; }
        public int level { set; get; }
        public string levelDescribe { set; get; }
        public int credit { set; get; }
        public string creditDescribe { set; get; }
        public int state { set; get; }
        public string stateDescribe { set; get; }
        public string markDay { set; get; }
        public int source { set; get; }
        public string sourceDescribe { set; get; }
        public string memo { set; get; }
        public int managerId { set; get; }
        public string managerName { set; get; }
    }

    public class CustomerSource
    {
        public int id { set; get; }
        public string name { set; get; }
    }
}