using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using DAL;
using System.Text;
using ViewModel;

namespace BLL
{
    public class TradeBLL
    {
        private static MSSQLMy ms = new MSSQLMy();
        public bool AddTrade(decimal totalPrice, int customerId, int productId, int num, string discountWay, decimal discountMoney, int payType, string receivingInfo)
        {
            DateTime time = DateTime.Now;
            string sql = "insert into c_tradeInfo values(@time,@totalPrice,@customerId,@discountMoney,@discountWay,@productId,@num,@payType,@receivingInfo,0,0)";
            return ms.Insert(sql, new {time, totalPrice, customerId, productId, num, discountWay, discountMoney, payType, receivingInfo });
        }

        public bool UpdateTrade(int id, int type)
        {
            string sql = "update c_tradeInfo set trade_type	= @type where trade_id = @id";
            return ms.Update(sql, new { id, type });
        }

        public bool UpdateTradeStatus(int id, int status)
        {
            string sql = "update c_tradeInfo set trade_status = @status where trade_id = @id";
            return ms.Update(sql, new { id, status });
        }
        public List<TradeInfo> GetTradeList(string type)
        {
            string sql = @"select t.trade_id id,t.trade_type type,t.trade_time time,c.cu_name customerName,p.product_name productName from c_tradeInfo t
left join c_customer c on c.cu_id = t.trade_customer
left join p_productInfo p on p.product_id = t.trade_product 
where t.trade_type in (@type) order by t.trade_type, t.trade_time desc ";
            return ms.GetListModel<TradeInfo>(sql, new { type });
        }

        public List<TradeInfo> GetTradeSearchList(string search, string startTime, string endTime, string type)
        {
            string where = "where t.trade_type in (" + type + ") and (c.cu_name like '%" + search + "%' or p.product_name like '%" + search + "%')";
            if (!string.IsNullOrEmpty(startTime))
            {
                where += " and t.trade_time >= @startTime";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                endTime = endTime + " 23:59:59";
                where += " and t.trade_time < @endTime";
            }
            string sql = @"select t.trade_id id,t.trade_type type,t.trade_time time,t.trade_status status,c.cu_name customerName,p.product_name productName from c_tradeInfo t
left join c_customer c on c.cu_id = t.trade_customer left join p_productInfo p on p.product_id = t.trade_product " + where + " order by t.trade_type, t.trade_time desc ";
            return ms.GetListModel<TradeInfo>(sql, new { startTime, endTime });
        }

        public C_TradeInfo GetTrade(int tradeId)
        {
            string sql = "select * from c_tradeInfo where trade_id=@tradeId";
            return ms.GetModel<C_TradeInfo>(sql, new { tradeId });
        }

        public List<TradeInfo> GetCustomerTradeList(int customerId)
        {
            string sql = @"select t.trade_id id,t.trade_type type,t.trade_time time,t.trade_money money,p.product_name productName from c_tradeInfo t
left join p_productInfo p on p.product_id = t.trade_product
where t.trade_customer = @customerId order by trade_type, trade_time desc";
            return ms.GetListModel<TradeInfo>(sql, new { customerId });
        }

        public List<TradeInfo> GetCustomerTradeSearchList(int customerId, string productName, string startTime, string endTime)
        {
            string where = "where t.trade_customer = @customerId and p.product_name like '%" + productName + "%'";
            if (!string.IsNullOrEmpty(startTime))
            {
                where += " and t.trade_time >= @startTime";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                endTime = endTime + " 23:59:59";
                where += " and t.trade_time < @endTime";
            }
            string sql = @"select t.trade_id id,t.trade_type type,t.trade_time time,t.trade_money money,p.product_name productName from c_tradeInfo t
left join p_productInfo p on p.product_id = t.trade_product " + where + " order by t.trade_type, t.trade_time desc ";
            return ms.GetListModel<TradeInfo>(sql, new { customerId, startTime, endTime });
        }
    }
}