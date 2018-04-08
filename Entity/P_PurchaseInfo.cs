using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class P_PurchaseInfo
    {
        public int pur_id { set; get; }
        public DateTime pur_date { set; get; }
        public string pur_name { set; get; }
        public string pur_num { set; get; }
        public decimal pur_price { set; get; }
        public decimal pur_money { set; get; }
        public int pur_supplier { set; get; }
    }
}