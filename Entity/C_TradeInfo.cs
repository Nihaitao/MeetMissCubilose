using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class C_TradeInfo
    {
        public int trade_id { set; get; }
        public string trade_time { set; get; }
        public decimal trade_money { set; get; }
        public int trade_customer { set; get; }
        public decimal discount_money { set; get; }
        public string discount_way { set; get; }
        public int trade_product { set; get; }
        public int trade_num { set; get; }
        public int payment_type { set; get; }
        public string receiving_info { set; get; }
        public int trade_type { set; get; }
        public int trade_status { set; get; }
    }
}