using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel;
using BLL;

namespace MissMeetCubilose.ApiControllers.Trade
{
    [RoutePrefix("api/Trade")]
    public class TradeController : ApiController
    {
        [HttpPost]
        public dynamic AddTradeInfo()
        {
            int customerId = Fun.Form("customerId", 0);
            int productId = Fun.Form("productId", 0);
            int num = Fun.Form("num", 0);
            string totalPrice = Fun.Form("totalPrice");
            int payType = Fun.Form("payType",0);
            string discountWay = Fun.Form("discountWay");
            string discountMoney = Fun.Form("discountMoney");
            string receivingInfo = Fun.Form("receivingInfo");
            TradeBLL bll = new TradeBLL();
            if (!bll.AddTrade(totalPrice.ToDecimal(), customerId, productId, num, discountWay, discountMoney.ToDecimal(), payType, receivingInfo))
                return "失败";
            else
                return "成功";
        }

        [HttpPost]
        public dynamic UpdateTradeInfo()//支付成功执行该方法
        {
            int id = Fun.Form("id", 0);
            int type = Fun.Form("type", -1);            
            int customerId = Fun.Form("customerId", 0);
            int productId = Fun.Form("productId", 0);
            TradeBLL bll = new TradeBLL();
            if (!bll.UpdateTrade(id, type))
            {
                return "失败";
            }
            else if(type == 1){
                new CustomerBLL().AddCustomerProduct(customerId, productId, 1);
            }               
            return "成功";            
        }
        
        [HttpGet]
        public dynamic GetTradeList()
        {
            string type = Fun.Query("type");  
            TradeBLL bll = new TradeBLL();
            List<TradeInfo> list = bll.GetTradeList(type);
            return list;
        }
        [HttpGet]
        public dynamic GetTradeSearchList()
        {
            string type = Fun.Query("type");
            string search = Fun.Query("search");
            string startTime = Fun.Query("startTime");
            string endTime = Fun.Query("endTime");
            TradeBLL bll = new TradeBLL();
            List<TradeInfo> list = bll.GetTradeSearchList(search, startTime, endTime, type);
            return list;
        }
        [HttpGet]
        public dynamic GetCustomerTradeList()
        {
            int customerId = Fun.Query("customerId", 0);
            TradeBLL bll = new TradeBLL();
            List<TradeInfo> list = bll.GetCustomerTradeList(customerId);
            return list;
        }

        [HttpGet]
        public dynamic GetCustomerTradeSearchList()
        {
            int customerId = Fun.Query("customerId", 0);
            string productName = Fun.Query("productName");
            string startTime = Fun.Query("startTime");
            string endTime = Fun.Query("endTime");
            TradeBLL bll = new TradeBLL();
            List<TradeInfo> list = bll.GetCustomerTradeSearchList(customerId, productName, startTime, endTime);
            return list;
        }
        [HttpGet]
        public dynamic GetTrade()
        {
            int tradeId = Fun.Query("id",0);
            TradeBLL bll = new TradeBLL();
            C_TradeInfo t = bll.GetTrade(tradeId);
            TradeInfo trade = new TradeInfo();
            trade.id = t.trade_id;
            trade.info = t.receiving_info;
            trade.money = t.trade_money;
            trade.num = t.trade_num;
            trade.time = t.trade_time;
            trade.discountMoney = t.discount_money;
            trade.discountWay = t.discount_way;
            trade.status = t.trade_status;
            trade.statusDescript = t.trade_status == 0 ? "未审核" : "审核通过";
            trade.type = t.trade_type;
            trade.typeDescript = t.trade_type == 0 ? "等待支付" : t.trade_type == 1 ? "已支付" : "已取消";
            trade.productId = t.trade_product;
            trade.productName = new ProductBLL().GetProduct(t.trade_product).product_name.Trim();
            trade.customerId = t.trade_customer;
            trade.customerName = new CustomerBLL().GetCustomer(t.trade_customer).cu_name.Trim();
            trade.payment = t.payment_type;
            trade.paymentType = new PaymentBLL().GetPayment(t.payment_type).payment_name.Trim();
            return trade;
        }
    }
}