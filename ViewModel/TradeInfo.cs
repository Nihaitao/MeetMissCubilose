using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class TradeInfo
    {
        public int id { set; get; }
        public int customerId { set; get; }
        public string customerName { set; get; }
        public int type { set; get; }
        public string typeDescript { set; get; }
        public string info { set; get; }
        public string time { set; get; }
        public decimal money { set; get; }
        public decimal discountMoney { set; get; }
        public string discountWay { set; get; }
        public int productId { set; get; }
        public string productName { set; get; }
        public int num { set; get; }
        public int payment { set; get; }
        public string paymentType { set; get; }
        public int status { set; get; }
        public string statusDescript { set; get; }
    }
}