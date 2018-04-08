using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class C_CustomerInfo
    {
        public int cu_id { set; get; }
        public int cu_login_id { set; get; }
        public string cu_name { set; get; }
        public int cu_sex { set; get; }
        public DateTime cu_birthday { set; get; }
        public int cu_bd_type { set; get; }
        public string cu_phone { set; get; }
        public string cu_addr { set; get; }
        public string cu_headimgurl { set; get; }
        public int cu_level { set; get; }
        public int cu_credit { set; get; }
        public int cu_state { set; get; }
        public string cu_mark_day { set; get; }
        public int cu_source { set; get; }
        public string cu_memo { set; get; }
        public int cu_manager_id { set; get; }
    }
}