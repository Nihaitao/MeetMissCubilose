using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class S_SupplierInfo
    {
        public int supplier_id { set; get; }
        public string supplier_name { set; get; }
        public string supplier_addr { set; get; }
        public string supplier_contacts { set; get; }
        public string supplier_phone { set; get; }
        public string memo { set; get; }
    }
}