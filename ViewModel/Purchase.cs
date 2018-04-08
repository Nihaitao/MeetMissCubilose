using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class Purchase
    {
        public int id { set; get; }
        public string name { set; get; }
        public DateTime date { set; get; }
        public string num { set; get; }
        public decimal price { set; get; }
        public decimal money { set; get; }
        public int supplierId { set; get; }
        public string supplierName { set; get; }        
    }
}