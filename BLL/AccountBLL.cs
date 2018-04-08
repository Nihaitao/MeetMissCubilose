using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using DAL;
using ViewModel;

namespace BLL
{
    public class AccountBLL 
    {
        private static MSSQLMy ms = new MSSQLMy();
        

        public dynamic GetAccountList(int year, int month)
        {
            string where1 = "";
            string where2 = "";
            string where3 = "";
            if (year != 0)
            {
                where1 += " and YEAR(trade_time) = " + year;
                where2 += " where YEAR(pur_date) = " + year;
                where3 += " where YEAR(delivery_time) = " + year;
                if (month != 0)
                {
                    where1 += " and MONTH(trade_time) = " + month;
                    where2 += " and MONTH(pur_date) = " + month;
                    where3 += " and MONTH(delivery_time) = " + month;
                }
            }
            string sql = @"select SUM(trade_money) money from c_tradeInfo where trade_type =1 " + where1 + @"union all 
select SUM(pur_money) from p_purchaseInfo " + where2 + @"union all select SUM(delivery_money) from p_delivery_product " + where3;
            return ms.GetListModel<Account>(sql);
        }

        public dynamic GetRevenueList(int year, int month)
        {
            string where = "";
            if (year != 0)
            {
                where += " and YEAR(t.trade_time) = " + year;
                if (month != 0)
                {
                    where += " and MONTH(t.trade_time) = " + month;
                }
            }
            string sql = @"select t.trade_time time, t.trade_money money, p.product_name name from c_tradeInfo t left join p_productInfo p on p.product_id = t.trade_product where trade_type = 1 " + where;
            return ms.GetListModel<Account>(sql);
        }

        public dynamic GetPurchaseOutList(int year, int month)
        {
            string where = "";
            if (year != 0)
            {
                where += " where YEAR(pur_date) = " + year;
                if (month != 0)
                {
                    where += " and MONTH(pur_date) = " + month;
                }
            }
            string sql = @"select pur_date time, pur_money money, pur_name name from p_purchaseInfo " + where;
            return ms.GetListModel<Account>(sql);
        }

        public dynamic GetDeliveryOutList(int year, int month)
        {
            string where = "";
            if (year != 0)
            {
                where += " where YEAR(p.delivery_time) = " + year;
                if (month != 0)
                {
                    where += " and MONTH(p.delivery_time) = " + month;
                }
            }
            string sql = @"select p.delivery_time time, p.delivery_money money, c.cu_name name from p_delivery_product p left join c_customer c on c.cu_id = p.customer_id" + where;
            return ms.GetListModel<Account>(sql);
        }
    }
}