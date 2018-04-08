using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using DAL;

namespace BLL
{
    public class PaymentBLL 
    {
        private static MSSQLMy ms = new MSSQLMy();

        public List<P_Payment> GetPaymentList()
        {
            string sql = "select * from p_payment";
            return ms.GetListModel<P_Payment>(sql);
        }

        public P_Payment GetPayment(int id)
        {
            string sql = "select * from p_payment where payment_id = " + id;
            return ms.GetModel<P_Payment>(sql);
        }


        public bool UpdatePayment(int id, string name)
        {
            string sql = "update p_payment set payment_name = @name where payment_id = @id";
            return ms.Update(sql, new { id, name});
        }

        public bool AddPayment(string name)
        {
            string sql = "insert into p_payment values(@name)";
            return ms.Insert(sql, new { name });
        }

        public bool DeletePayment(int id)
        {
            string sql = "delete p_payment where payment_id = @id";
            return ms.Insert(sql, new { id });
        }

        public List<P_Payment> GetSearchPaymentList(string name)
        {
            string sql = "select * from p_payment where payment_name like '%" + name + "%'";
            return ms.GetListModel<P_Payment>(sql);
        }
    }
}