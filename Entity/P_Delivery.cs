using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class P_Delivery
    {
        public int delivery_id { set; get; }
        public DateTime delivery_time { set; get; }
        public string delivery_type { set; get; }
        public decimal delivery_money { set; get; }
        public int customer_id { set; get; }
        public string target_name { set; get; }
        public string target_phone { set; get; }
        public string target_addr { set; get; }
        public string cubilose_ids { set; get; }
        public string cubilose_nums { set; get; }
        public string cubilose_GPs { set; get; }
        public string delivery_memo { set; get; }
    }
}