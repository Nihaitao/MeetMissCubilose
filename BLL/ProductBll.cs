using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using DAL;
using System.Text;

namespace BLL
{
    public class ProductBLL
    {
        private static MSSQLMy ms = new MSSQLMy();
        public List<P_ProductInfo> GetProductList()
        {
            string sql = "select product_id,product_name,product_state,product_img from p_productInfo order by product_state desc,product_name";
            return ms.GetListModel<P_ProductInfo>(sql);
        }

        public bool AddProduct(string name, int type, string mode, string component, string num, decimal price, string info, string img)
        {
            string sql = "insert into p_productInfo values(@name,@type,@mode,@component,@num,@price,@info,@img,1)";
            return ms.Insert(sql, new { name, type, mode, component,num,price,info,img});
        }

        public P_ProductInfo GetProduct(int id)
        {
            string sql = "select * from p_productInfo where product_id = " + id;
            return ms.GetModel<P_ProductInfo>(sql);
        }

        public bool UpdateProduct(int id, string name, int type, string mode, string component, string num, decimal price, string info, string img)
        {
            string sql = "update p_productInfo set product_name=@name,product_type=@type,product_mode=@mode,product_component=@component,component_num=@num,product_price=@price,product_info=@info,product_img=@img where product_id = @id";
            return ms.Insert(sql, new { name, type, mode, component, num, price, info, img, id });
        }

        public List<P_ProductInfo> GetCustomerProductList(int customerId, int type)
        {
            string sql = @"select p.* from p_productInfo p
left join c_customer_product c on c.product_id = p.product_id
where c.cu_id = " + customerId + " and c.type = " + type + " order by c.time desc";
            return ms.GetListModel<P_ProductInfo>(sql);
        }

        public List<P_ProductInfo> GetNormalProductList()
        {
            string sql = "select * from p_productInfo where product_state = 1 order by product_name";
            return ms.GetListModel<P_ProductInfo>(sql);
        }
        public List<P_ProductInfo> GetSearchNormalProductList(string name)
        {
            string sql = "select * from p_productInfo where product_name like '%" + name + "%' and product_state = 1 order by product_name";
            return ms.GetListModel<P_ProductInfo>(sql);
        }

        public bool DeleteProduct(int id, int state)
        {
            string sql = "update p_productInfo set product_state=@state where product_id = @id";
            return ms.Update(sql, new { id, state });
        }

        public List<P_ProductInfo> GetSearchProductList(string name)
        {
            string sql = "select * from p_productInfo where product_name like '%" + name + "%' order by product_state desc, product_name";
            return ms.GetListModel<P_ProductInfo>(sql);
        }

        public List<P_ProductInfo> GetSearchCustomerProductList(int customerId, int type, string name)
        {
            string sql = @"select p.* from p_productInfo p
left join c_customer_product c on c.product_id = p.product_id
where c.cu_id = " + customerId + " and c.type = " + type + " and p.product_name like '%" + name + "%' order by c.time desc";
            return ms.GetListModel<P_ProductInfo>(sql);
        }

        public bool DeleteCustomerProducts(int productId, int customerId, int type)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from c_customer_product where cu_id = @customerId and type=@type");
            if (productId > 0)
                sql.Append(" and product_id	= @productId");
            return ms.Delete(sql.ToString(), new { productId, customerId, type });
        }
    }
}