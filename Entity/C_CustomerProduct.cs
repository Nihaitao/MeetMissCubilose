using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class C_CustomerProduct
    {
        public int cu_id { set; get; }
        public int product_id { set; get; }
        public DateTime time { set; get; }
        public int type { set; get; }
    }
}