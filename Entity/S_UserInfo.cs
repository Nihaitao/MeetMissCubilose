using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DbAttribute;

namespace Entity
{
    public class S_UserInfo
    {
        [Auto]
        [Key]
        public int user_id { set; get; }
        public string user_name { set; get; }
        public string user_pwd { set; get; }
        public string user_wx_id { set; get; }
        public int user_state { set; get; }
        public DateTime user_time { set; get; }
    }
}