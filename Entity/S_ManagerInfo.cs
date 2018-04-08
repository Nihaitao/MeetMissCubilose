using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class S_ManagerInfo
    {
        public int mgr_id { set; get; }
        public int mgr_login_id { set; get; }
        public string mgr_name { set; get; }
        public string mgr_headimgurl { set; get; }
        public DateTime mgr_time { set; get; }
    }
}