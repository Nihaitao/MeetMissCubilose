using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class P_ProductInfo
    {
        public int product_id { set; get; }
        public string product_name { set; get; }
        public int product_type { set; get; }
        public string product_mode { set; get; }
        public string product_component { set; get; }
        public string component_num { set; get; }
        public decimal product_price { set; get; }
        public string product_info { set; get; }
        public string product_img { set; get; }
        public int product_state { set; get; }
    }
}