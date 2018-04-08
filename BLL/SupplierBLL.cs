using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using DAL;

namespace BLL
{
    public class SupplierBLL 
    {
        private static MSSQLMy ms = new MSSQLMy();

        public List<S_SupplierInfo> GetSupplierList()
        {
            string sql = "select * from s_supplierInfo";
            return ms.GetListModel<S_SupplierInfo>(sql);
        }

        public S_SupplierInfo GetSupplier(int id)
        {
            string sql = "select * from s_supplierInfo where supplier_id = " + id;
            return ms.GetModel<S_SupplierInfo>(sql);
        }


        public bool UpdateSupplier(int id, string name, string person, string phone, string address, string memo)
        {
            string sql = "update s_supplierInfo set supplier_name = @name, supplier_addr = @address, supplier_contacts = @person, supplier_phone = @phone, memo=@memo where supplier_id = @id";
            return ms.Update(sql, new { id, name, person, phone, address, memo });
        }

        public bool AddSupplier(string name, string person, string phone, string address, string memo)
        {
            string sql = "insert into s_supplierInfo values(@name,@address,@person,@phone,@memo)";
            return ms.Insert(sql, new { name, person, phone, address, memo });
        }

        public bool DeleteSupplier(int id)
        {
            string sql = "delete s_supplierInfo where supplier_id = @id";
            return ms.Delete(sql, new { id });
        }

        public List<S_SupplierInfo> GetSearchSupplierList(string name)
        {
            string sql = "select * from s_supplierInfo where supplier_name like '%" + name + "%'";
            return ms.GetListModel<S_SupplierInfo>(sql);
        }
    }
}