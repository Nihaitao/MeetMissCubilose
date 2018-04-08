using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel;
using BLL;

namespace MissMeetCubilose.ApiControllers.Customer
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        [HttpGet]
        public dynamic GetCustomerList()
        {
            CustomerBLL c = new CustomerBLL();
            List<C_CustomerInfo> list = c.GetCustomerList();
            List<CustomerInfo> mList = new List<CustomerInfo>();
            CustomerInfo customer = null;
            foreach (var item in list)
            {
                customer = new CustomerInfo();
                customer.id = item.cu_id;
                customer.name = item.cu_name.Trim();
                customer.headimgurl = item.cu_headimgurl;
                mList.Add(customer);
            }
            return mList;
        }
        [HttpGet]
        public dynamic GetSearchCustomerList()
        {
            string name = Fun.Query("name");
            CustomerBLL c = new CustomerBLL();
            List<C_CustomerInfo> list = c.GetSearchCustomerList(name);
            List<CustomerInfo> mList = new List<CustomerInfo>();
            CustomerInfo customer = null;
            foreach (var item in list)
            {
                customer = new CustomerInfo();
                customer.id = item.cu_id;
                customer.name = item.cu_name.Trim();
                customer.headimgurl = item.cu_headimgurl;
                mList.Add(customer);
            }
            return mList;
        }
        [HttpGet]
        public dynamic GetCustomer()
        {
            int id = Fun.Query("id", 0);
            CustomerBLL bll = new CustomerBLL();
            C_CustomerInfo c = bll.GetCustomer(id);
            CustomerInfo customer = new CustomerInfo();
            customer.id = c.cu_id;
            customer.name = c.cu_name.Trim();
            customer.headimgurl = c.cu_headimgurl;
            customer.phone = c.cu_phone;
            customer.addr = c.cu_addr;
            customer.sex = c.cu_sex == 1 ? "男" : c.cu_sex == 0 ? "女" : "未知";
            customer.birthday = c.cu_birthday.ToString("yyyy-MM-dd");
            customer.bdtype = c.cu_bd_type == 1 ? "阳历" : "阴历";
            customer.memo = c.cu_memo;
            customer.level = c.cu_level;
            customer.credit = c.cu_credit;
            customer.markDay = String.IsNullOrEmpty(c.cu_mark_day) ? "" : c.cu_mark_day;
            customer.state = c.cu_state;
            switch (customer.state)
            {
                case 0 :
                    customer.stateDescribe = "潜在";
                    break;
                case 1:
                    customer.stateDescribe = "正常";
                    break;
                case 2:
                    customer.stateDescribe = "预警";
                    break;
                case -1:
                    customer.stateDescribe = "流失";
                    break;

            }
            customer.managerName = new ManagerBLL().GetManager(c.cu_manager_id).mgr_name.Trim();
            customer.source = c.cu_source;
            customer.sourceDescribe = bll.GetSource(c.cu_source).source_name.Trim();
            return customer;
        }

        [HttpPost]
        public dynamic AddCustomer()
        {
            string name = Fun.Form("name");
            int sex = Fun.Form("sex",-1);
            string bday = Fun.Form("bday");
            int bdType = Fun.Form("bdType", 0);
            int source = Fun.Form("source", 0);
            int level = Fun.Form("level", 0);
            int manager = Fun.Form("manager", 0);
            int state = Fun.Form("state", 0);
            string markday = Fun.Form("markday");
            string phone = Fun.Form("phone");
            string addr = Fun.Form("addr");
            string memo = Fun.Form("memo");
            
            int credit = 100;//信用度
            string headImg = "";//默认头像
            int loginId = 0;//未绑定登陆账号
            CustomerBLL c = new CustomerBLL();
            if (!c.AddCustomer(loginId, name, sex, bday, bdType, source, level, manager, state, markday, phone, addr, memo, credit, headImg))
                return "失败";            
            else
                return "成功";
        }
        [HttpPost]
        public dynamic UpdateCustomer()
        {
            int id = Fun.Form("id", 0);
            string name = Fun.Form("name");
            int sex = Fun.Form("sex", -1);
            string bday = Fun.Form("bday");
            int bdType = Fun.Form("bdType", 0);
            int source = Fun.Form("source", 0);
            int level = Fun.Form("level", 0);
            int manager = Fun.Form("manager", 0);
            int state = Fun.Form("state", 0);
            string markday = Fun.Form("markday");
            string phone = Fun.Form("phone");
            string addr = Fun.Form("addr");
            string memo = Fun.Form("memo");
            int credit = Fun.Form("credit",100); 

            string headImg = "";//默认头像
            CustomerBLL c = new CustomerBLL();
            if (!c.UpdateCustomer(id, name, sex, bday, bdType, source, level, manager, state, markday, phone, addr, memo, credit, headImg))
                return "失败";
            else
                return "成功";
        }
        [HttpPost]
        public dynamic UpdateCustomerImg()
        {
            int id = Fun.Form("id", 0);
            string img = Fun.Form("img");
            CustomerBLL c = new CustomerBLL();
            if (!c.UpdateCustomerImg(id, img))
                return "失败";
            else
                return "成功";
        }
        [HttpPost]
        public dynamic UpdateCustomerName()
        {
            int id = Fun.Form("id", 0);
            string name = Fun.Form("name");
            CustomerBLL c = new CustomerBLL();
            if (!c.UpdateCustomerName(id, name))
                return "失败";
            else
                return "成功";
        }
        [HttpPost]
        public dynamic UpdateCustomerSex()
        {
            int id = Fun.Form("id", 0);
            int sex = Fun.Form("sex", -1);
            CustomerBLL c = new CustomerBLL();
            if (!c.UpdateCustomerSex(id, sex))
                return "失败";
            else
                return "成功";
        }
        [HttpPost]
        public dynamic UpdateCustomerPhone()
        {
            int id = Fun.Form("id", 0);
            string phone = Fun.Form("phone");
            CustomerBLL c = new CustomerBLL();
            if (!c.UpdateCustomerPhone(id, phone))
                return "失败";
            else
                return "成功";
        }
        [HttpPost]
        public dynamic UpdateCustomerAddress()
        {
            int id = Fun.Form("id", 0);
            string address = Fun.Form("address");
            CustomerBLL c = new CustomerBLL();
            if (!c.UpdateCustomerAddress(id, address))
                return "失败";
            else
                return "成功";
        }
        [HttpPost]
        public dynamic AddCustomerProduct()
        {
            int type = Fun.Form("type", -1);
            int productId = Fun.Form("productId", 0);
            int customerId = Fun.Form("customerId", 0);
            CustomerBLL bll = new CustomerBLL();
            if (type != -1 && bll.GetCustomerProduct(customerId, productId, type) != null)
            {
                bll.UpdateCustomerProduct(customerId, productId, type);
                return "成功";
            }
            if (type == -1 || !bll.AddCustomerProduct(customerId, productId, type))
                return "失败";
            else
                return "成功";
        }

        [HttpPost]
        public dynamic AddCustomerCubilose()
        {
            int customerId = Fun.Form("customerId", 0);
            int tradeId = Fun.Form("tradeId", 0);
            string cubilose = Fun.Form("cubilose");
            string num = Fun.Form("num");
            string[] cubiloseId = cubilose.Split('|');
            string[] totalNum = num.Split('|');
            CustomerBLL bll = new CustomerBLL();
            if (bll.AddCustomerCubilose(customerId, cubiloseId, totalNum, tradeId))//添加客户物品
            {
                if (new TradeBLL().UpdateTradeStatus(tradeId, 1))//修改订单状态
                {
                    CustomerBLL c = new CustomerBLL();
                    C_CustomerInfo customer = c.GetCustomer(customerId);
                    if (customer.cu_state == 1)//如果是正常客户，不做操作
                        return "成功";
                    else if (customer.cu_state == 0)//潜在客户，更改为正常客户，设置当日为第一次成为正式客户纪念日
                    {
                        return c.UpdateCustomerState(customerId, 1, DateTime.Now.ToString("yyyy-MM-dd")) ? "成功" : "失败";
                    }
                    else//预警&流失客户恢复正常
                    {
                        return c.UpdateCustomerState(customerId, 1, "") ? "成功" : "失败";//纪念日穿空值，不做修改
                    }

                }
                else
                    return "失败";
            }
            else
            {
                return "失败";
            }
        }

        [HttpPost]
        public dynamic UpdateCustomerCubilose()
        {
            int customerId = Fun.Form("customerId", 0);
            string cubilose = Fun.Form("cubilose");
            string num = Fun.Form("num");
            string[] cubiloseId = cubilose.Split('|');
            string[] totalNum = num.Split('|');
            CustomerBLL bll = new CustomerBLL();
            if (bll.AddCustomerCubilose(customerId, cubiloseId, totalNum, 0))            
                return "成功";            
            else            
                return "失败";            
        }

        [HttpGet]
        public dynamic GetCustomerData()
        {
            CustomerBLL bll = new CustomerBLL();
            List<C_CustomerInfo> list = bll.GetCustomerData();
            return list;
        }
        [HttpGet]
        public dynamic GetSearchCustomerData()
        {
            string name = Fun.Query("name");
            CustomerBLL bll = new CustomerBLL();
            List<C_CustomerInfo> list = bll.GetSearchCustomerData(name);
            return list;
        }
        [HttpGet]
        public dynamic GetCustomerCubilose()
        {
            int customerId = Fun.Query("customerId", 0);
            CustomerBLL bll = new CustomerBLL();
            List<CustomerCubilose> list = bll.GetCustomerCubilose(customerId);
            return list;
        }
        [HttpGet]
        public dynamic GetCustomerCubiloseSurplus()
        {
            int customerId = Fun.Query("customerId", 0);
            CustomerBLL bll = new CustomerBLL();
            List<CustomerCubilose> list = bll.GetCustomerCubiloseSurplus(customerId);
            return list;
        }
        [HttpGet]
        public dynamic GetCustomerAddressList()
        {
            int customerId = Fun.Query("customerId", 0);
            CustomerBLL c = new CustomerBLL();
            List<C_AddressInfo> list = c.GetCustomerAddressList(customerId);
            return list;
        }
        [HttpPost]
        public dynamic AddCustomerAddress()
        {
            int serial = Fun.Form("serial", 0);
            int customerId = Fun.Form("customerId", 0);
            string person = Fun.Form("person");
            string phone = Fun.Form("phone");
            string address = Fun.Form("address");
            CustomerBLL bll = new CustomerBLL();
            C_AddressInfo addr = null;
            if (serial > 0)
            {
                addr = bll.GetCustomerAddressDefault(customerId);
            }
            if (bll.AddCustomerAddress(customerId, person, phone, address, serial))
            {
                if (serial > 0 && addr != null)
                {
                    if (bll.UpdateCustomerAddressDefault(addr.addr_id, 0))
                    {
                        return "成功";
                    }
                    else
                    {
                        return "失败";
                    }

                }
                return "成功";
            }
            else
                return "失败";
        }
        [HttpPost]
        public dynamic DeleteCustomerAddress()
        {
            int id = Fun.Form("addrId", 0);
            CustomerBLL bll = new CustomerBLL();
            if (bll.DeleteCustomerAddress(id))
                return "成功";
            else
                return "成功";
        }
        [HttpGet]
        public dynamic GetCustomerDelivery()
        {
            int customerId = Fun.Query("customerId", 0);
            CustomerBLL bll = new CustomerBLL();
            List<P_Delivery> list = bll.GetCustomerDelivery(customerId);
            return list;
        }
        [HttpPost]
        public dynamic AddCustomerDelivery()
        {
            int customerId = Fun.Form("customerId", 0);
            string time = Fun.Form("time");
            string type = Fun.Form("type");
            string money = Fun.Form("money");
            string name = Fun.Form("name");
            string phone = Fun.Form("phone");
            string address = Fun.Form("address");
            string cubiloses = Fun.Form("cubilose");
            string nums = Fun.Form("num");
            string GPs = Fun.Form("GP");
            string memo = Fun.Form("memo");
            CustomerBLL bll = new CustomerBLL();
            if (bll.AddCustomerDelivery(time, type, money, customerId, name, phone, address, cubiloses, nums, GPs, memo))
            {
                string[] cubiloseId = cubiloses.Split('|');
                string[] totalNum = nums.Split('|');
                for (int i = 0; i < totalNum.Length; i++)
                {
                    totalNum[i] = "-" + totalNum[i];
                }
                if (bll.AddCustomerCubilose(customerId, cubiloseId, totalNum, -1))
                    return "成功";
                else
                    return "失败";
            }
            else
                return "失败";
        }
        [HttpGet]
        public dynamic GetDelivery()
        {
            int deliveryId = Fun.Query("deliveryId", 0);
            CustomerBLL bll = new CustomerBLL();
            P_Delivery d = bll.GetDelivery(deliveryId);
            Delivery delivery = new Delivery();
            delivery.time = d.delivery_time.ToString("yyyy-MM-dd");
            delivery.type = d.delivery_type;
            delivery.name = d.target_name;
            delivery.phone = d.target_phone;
            delivery.addr = d.target_addr;
            delivery.memo = d.delivery_memo;
            List<P_Cubilose> list = null;
            if (!string.IsNullOrEmpty(d.cubilose_ids))
            {
                list = new CubiloseBLL().GetCubiloseList();
                string[] ids = d.cubilose_ids.Split('|');
                string[] nums = d.cubilose_nums.Split('|');
                string[] gps = d.cubilose_GPs.Split('|');
                for (int i = 0; i < ids.Length; i++)
                {
                    delivery.goodsInfo += list.Where(x => x.cubilose_id == int.Parse(ids[i])).FirstOrDefault().cubilose_name.Trim() + "(" + nums[i] + ") EXP:" + gps[i].Replace("-","") + ",";
                }
                delivery.goodsInfo = delivery.goodsInfo.Substring(0, delivery.goodsInfo.Length - 1);
            }
            return delivery;
        }

    }
}
