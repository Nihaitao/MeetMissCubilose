using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using DAL;
using ViewModel;

namespace BLL
{
    public class PurchaseBLL 
    {
        private static MSSQLMy ms = new MSSQLMy();

        public List<Purchase> GetPurchaseList()
        {
            string sql = "select p.pur_id id,p.pur_date date,p.pur_name name, s.supplier_name supplierName from p_purchaseInfo p left join s_supplierInfo s on s.supplier_id = p.pur_supplier";
            return ms.GetListModel<Purchase>(sql);
        }

        public Purchase GetPurchase(int id)
        {
            string sql = "select p.pur_id id,p.pur_date date,p.pur_name name,p.pur_num num,p.pur_price price,p.pur_money money, s.supplier_name supplierName from p_purchaseInfo p left join s_supplierInfo s on s.supplier_id = p.pur_supplier where p.pur_id = " + id;
            return ms.GetModel<Purchase>(sql);
        }


        public bool AddPurchase(string date, string name, string num, string price, string money, int supplierId)
        {
            string sql = "insert into p_purchaseInfo values(@date,@name,@num,@price,@money,@supplierId)";
            return ms.Insert(sql, new { date, name, num, price, money, supplierId });
        }


        public List<Purchase> GetSearchPurchaseList(string name,string startDate,string endDate)
        {
            string where = "";
            if (!String.IsNullOrEmpty(startDate))
            {
                where += " and p.pur_date >= '" + startDate + "'";
            }
            if (!String.IsNullOrEmpty(endDate))
            {
                where += " and p.pur_date <= '" + endDate + "'";
            }
            string sql = "select p.pur_id id,p.pur_date date,p.pur_name name, s.supplier_name supplierName from p_purchaseInfo p left join s_supplierInfo s on s.supplier_id = p.pur_supplier where (p.pur_name like '%" + name + "%' or s.supplier_name like '%"+ name + "%')" + where;
            return ms.GetListModel<Purchase>(sql);
        }
    }
}