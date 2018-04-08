using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class C_AddressInfo
    {
        public int addr_id { set; get; }
        public int cu_id { set; get; }
        public string address { set; get; }
        public string person { set; get; }
        public string phone { set; get; }
        public int serial { set; get; }
    }
}