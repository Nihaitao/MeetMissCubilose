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
    public class CustomerBLL
    {
        private static MSSQLMy ms = new MSSQLMy();

        public bool AddCustomer(int loginId, string name, int sex, string bday, int bdType, int source, int level, int manager, int state, string markday, string phone, string addr, string memo, int credit, string headImg)
        {
            string sql = "insert into c_customer values(@loginId,@name,@sex,@bday,@bdType,@phone,@addr,@headImg,@level,@credit,@state,@markday,@source,@memo,@manager)";
            return ms.Insert(sql, new { loginId, name, sex, bday, bdType, phone, addr, headImg, level, credit, state, markday, source, memo, manager });
        }
        public bool UpdateCustomer(int id, string name, int sex, string bday, int bdType, int source, int level, int manager, int state, string markday, string phone, string addr, string memo, int credit, string headImg)
        {
            string sql = @"update c_customer set cu_name=@name,cu_sex=@sex,cu_birthday=@bday,cu_bd_type=@bdType,cu_phone=@phone,cu_addr=@addr,
            cu_headimgurl=@headImg,cu_level=@level,cu_credit=@credit,cu_state=@state,cu_mark_day=@markday,cu_source	=@source,cu_memo=@memo,cu_manager_id=@manager where cu_id=@id";
            return ms.Update(sql, new { id, name, sex, bday, bdType, phone, addr, headImg, level, credit, state, markday, source, memo, manager });
        }

        public List<C_CustomerInfo> GetCustomerList()
        {
            string sql = "select cu_id, cu_name, cu_headimgurl from c_customer order by cu_name";
            return ms.GetListModel<C_CustomerInfo>(sql);
        }
        public List<C_CustomerInfo> GetSearchCustomerList(string name)
        {
            string sql = "select cu_id, cu_name, cu_headimgurl from c_customer where cu_name like '%" + name + "%' order by cu_name";
            return ms.GetListModel<C_CustomerInfo>(sql);
        }

        public C_CustomerInfo GetCustomer(int id)
        {
            string sql = "select * from c_customer where cu_id = " + id;
            return ms.GetModel<C_CustomerInfo>(sql);
        }

        public List<C_CustomerSource> GetSourceList()
        {
            string sql = "select * from c_customer_source";
            return ms.GetListModel<C_CustomerSource>(sql);
        }

        public C_CustomerSource GetSource(int id)
        {
            string sql = "select * from c_customer_source where source_id = " + id;
            return ms.GetModel<C_CustomerSource>(sql);
        }


        public bool UpdateSource(int id, string name)
        {
            string sql = "update c_customer_source set source_name = @name where source_id = @id";
            return ms.Update(sql, new { id, name });
        }

        public bool AddSource(string name)
        {
            string sql = "insert into c_customer_source values(@name)";
            return ms.Insert(sql, new { name });
        }

        public bool DeleteSource(int id)
        {
            string sql = "delete c_customer_source where source_id = @id";
            return ms.Delete(sql, new { id });
        }
        public C_CustomerInfo GetCustomerByLoginId(int id)
        {
            string sql = "select * from c_customer where cu_login_id = " + id;
            return ms.GetModel<C_CustomerInfo>(sql);
        }

        public bool AddCustomerProduct(int customerId, int productId, int type)
        {
            DateTime time = DateTime.Now;
            string sql = "insert into c_customer_product values(@customerId,@productId,@type,@time)";
            return ms.Insert(sql, new { customerId, productId, type, time });
        }

        public bool UpdateCustomerProduct(int customerId, int productId, int type)
        {
            DateTime time = DateTime.Now;
            string sql = "updata c_customer_product set time = @time where cu_id = @customerId and product_id=@productId and type=@type";
            return ms.Insert(sql, new { customerId, productId, type, time });
        }

        public P_ProductInfo GetCustomerProduct(int customerId, int productId, int type)
        {
            string sql = @"select * from c_customer_product where cu_id = " + customerId + " and product_id = " + productId + " and type = " + type;
            return ms.GetModel<P_ProductInfo>(sql);
        }


        public List<C_CustomerSource> GetSearchSourceList(string name)
        {
            string sql = "select * from c_customer_source where source_name like '%" + name + "%'";
            return ms.GetListModel<C_CustomerSource>(sql);
        }

        public bool UpdateCustomerImg(int id, string img)
        {
            string sql = "update c_customer set cu_headimgurl=@img where cu_id=@id";
            return ms.Update(sql, new { id, img });
        }
        public bool UpdateCustomerName(int id, string name)
        {
            string sql = "update c_customer set cu_name=@name where cu_id=@id";
            return ms.Update(sql, new { id, name });
        }

        public bool UpdateCustomerSex(int id, int sex)
        {
            string sql = "update c_customer set cu_sex=@sex where cu_id=@id";
            return ms.Update(sql, new { id, sex });
        }

        public bool UpdateCustomerPhone(int id, string phone)
        {
            string sql = "update c_customer set cu_phone=@phone where cu_id=@id";
            return ms.Update(sql, new { id, phone });
        }

        public bool UpdateCustomerAddress(int id, string address)
        {
            string sql = "update c_customer set cu_addr=@address where cu_id=@id";
            return ms.Update(sql, new { id, address });
        }

        public bool AddCustomerAddress(int customerId, string person, string phone, string address, int serial)
        {
            string sql = "insert into c_addressInfo values(@customerId,@address,@person,@phone,@serial)";
            return ms.Insert(sql, new { customerId, address, person, phone, serial });
        }

        public C_AddressInfo GetCustomerAddressDefault(int customerId)
        {
            string sql = @"select * from c_addressInfo where serial = 1 and cu_id = " + customerId;
            return ms.GetModel<C_AddressInfo>(sql);
        }

        public bool UpdateCustomerAddressDefault(int id, int serial)
        {
            string sql = "update c_addressInfo set serial=@serial where addr_id=@id";
            return ms.Update(sql, new { id, serial });
        }
        public bool DeleteCustomerAddress(int id)
        {
            string sql = "delete from c_addressInfo  where addr_id=@id";
            return ms.Delete(sql, new { id });
        }
        public List<C_AddressInfo> GetCustomerAddressList(int customerId)
        {
            string sql = @"select * from c_addressInfo where cu_id = " + customerId + " order by serial desc";
            return ms.GetListModel<C_AddressInfo>(sql);
        }
        public bool AddCustomerCubilose(int customerId, string[] cubiloseId, string[] totalNum, int tradeId)
        {
            string sql = "insert into c_customer_cubilose values";
            string memo = "";
            if (tradeId > 0)
                memo = "来自订单号：" + tradeId;
            else if (tradeId == 0)
                memo = "管理员手动添加";
            else
                memo = "配送出库";
            StringBuilder values = new StringBuilder();
            for (int i = 0; i < cubiloseId.Length; i++)
            {
                values.Append("(" + customerId + "," + cubiloseId[i] + "," + totalNum[i] + ",0,'" + memo + "'),");
            }
            sql += values.ToString().Substring(0, values.Length - 1);
            return ms.Insert(sql, null);
        }

        public List<CustomerCubilose> GetCustomerCubilose(int customerId)
        {
            string sql = @"select a.cu_id cuId,a.cubilose_id cubiloseId, b.cubilose_name cubiloseName,b.cubilose_img img,b.cubilose_info info,SUM(a.totalNum) totalNum,SUM(a.alreadyNum) alreadyNum ,(SUM(a.totalNum) - SUM(a.alreadyNum)) surplusNum
from c_customer_cubilose a left join p_cubilose b on b.cubilose_id = a.cubilose_id where cu_id = @customerId
group by a.cu_id,b.cubilose_name,b.cubilose_img,b.cubilose_info,a.cubilose_id order by surplusNum desc";
            return ms.GetListModel<CustomerCubilose>(sql, new { customerId });
        }

        public List<CustomerCubilose> GetCustomerCubiloseSurplus(int customerId)
        {
            string sql = @"select a.cu_id cuId,a.cubilose_id cubiloseId, b.cubilose_name cubiloseName,b.cubilose_img img,b.cubilose_info info,SUM(a.totalNum) totalNum,SUM(a.alreadyNum) alreadyNum ,(SUM(a.totalNum) - SUM(a.alreadyNum)) surplusNum
from c_customer_cubilose a left join p_cubilose b on b.cubilose_id = a.cubilose_id where cu_id = @customerId
group by a.cu_id,b.cubilose_name,b.cubilose_img,b.cubilose_info,a.cubilose_id having SUM(a.totalNum) > SUM(a.alreadyNum) order by surplusNum desc";
            return ms.GetListModel<CustomerCubilose>(sql, new { customerId });
        }

        public dynamic UpdateCustomerState(int customerId, int state, string markDay)
        {
            string and = "";
            if (markDay != "")
                and = ", cu_mark_day = '" + markDay + "'";
            string sql = "update c_customer set cu_state = @state " + and + " where cu_id = @customerId";
            return ms.Update(sql, new { customerId, state });

        }

        public List<C_CustomerInfo> GetCustomerData()
        {
            string sql = "select * from c_customer order by cu_state desc,cu_level desc, cu_credit desc";
            return ms.GetListModel<C_CustomerInfo>(sql, "");
        }

        public List<C_CustomerInfo> GetSearchCustomerData(string name)
        {
            string sql = "select * from c_customer where cu_name like '%" + name + "%' order by cu_state desc,cu_level desc, cu_credit desc";
            return ms.GetListModel<C_CustomerInfo>(sql);
        }

        public List<P_Delivery> GetCustomerDelivery(int customerId)
        {
            string sql = "select * from p_delivery_product where customer_id = @customerId order by delivery_time desc";
            return ms.GetListModel<P_Delivery>(sql, new { customerId });
        }

        public bool AddCustomerDelivery(string time, string type, string money, int customerId, string name, string phone, string address, string cubiloses, string nums, string GPs, string memo)
        {
            string sql = "insert into p_delivery_product values(@time,@type,@money,@customerId,@name,@phone,@address,@cubiloses,@nums,@GPs,@memo)";
            return ms.Insert(sql, new { customerId, type,money, time, name, phone, address, cubiloses, nums, GPs, memo });
        }

        public P_Delivery GetDelivery(int deliveryId)
        {
            string sql = "select * from p_delivery_product where delivery_id = @deliveryId";
            return ms.GetModel<P_Delivery>(sql, new { deliveryId });
        }

    }
}