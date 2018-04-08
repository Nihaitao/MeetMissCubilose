using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel;
using BLL;

namespace MissMeetCubilose.ApiControllers.Supplier
{
    [RoutePrefix("api/Supplier")]
    public class SupplierController : ApiController
    {
        [HttpGet]
        public dynamic GetSupplierList()
        {
            SupplierBLL bll = new SupplierBLL();
            List<S_SupplierInfo> list = bll.GetSupplierList();
            return list;
        }
        [HttpGet]
        public dynamic GetSearchSupplierList()
        {
            string name = Fun.Query("name");
            SupplierBLL bll = new SupplierBLL();
            List<S_SupplierInfo> list = bll.GetSearchSupplierList(name);
            return list;
        }
        
        [HttpGet]
        public dynamic GetSupplier()
        {
            int id = Fun.Query("id", 0);
            SupplierBLL bll = new SupplierBLL();
            S_SupplierInfo s = bll.GetSupplier(id);
            return s;
        }

        [HttpPost]
        public dynamic AddSupplier()
        {
            string name = Fun.Form("name");
            string person = Fun.Form("person");
            string address = Fun.Form("address");
            string phone = Fun.Form("phone");
            string memo = Fun.Form("memo");
            SupplierBLL bll = new SupplierBLL();
            if (!bll.AddSupplier(name, person, address, phone, memo))
                return "失败";
            else
                return "成功";
        }

        [HttpPost]
        public dynamic UpdateSupplier()
        {
            int id = Fun.Form("id", 0);
            string name = Fun.Form("name");
            string person = Fun.Form("person");
            string address = Fun.Form("address");
            string phone = Fun.Form("phone");
            string memo = Fun.Form("memo");
            SupplierBLL bll = new SupplierBLL();
            if (!bll.UpdateSupplier(id, name, person, address, phone, memo))
                return "失败";
            else
                return "成功";
        }

        [HttpPost]
        public dynamic DeleteSupplier()
        {
            int id = Fun.Form("id", 0);
            SupplierBLL bll = new SupplierBLL();
            if (!bll.DeleteSupplier(id))
                return "失败";
            else
                return "成功";
        }
    }
}