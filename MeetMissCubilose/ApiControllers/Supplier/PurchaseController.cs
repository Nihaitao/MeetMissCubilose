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
    [RoutePrefix("api/Purchase")]
    public class PurchaseController : ApiController
    {
        [HttpGet]
        public dynamic GetPurchaseList()
        {
            PurchaseBLL bll = new PurchaseBLL();
            List<Purchase> list = bll.GetPurchaseList();
            return list;
        }
        [HttpGet]
        public dynamic GetSearchPurchaseList()
        {
            string name = Fun.Query("name");
            string startDate = Fun.Query("startDate");
            string endDate = Fun.Query("endDate");
            PurchaseBLL bll = new PurchaseBLL();
            List<Purchase> list = bll.GetSearchPurchaseList(name, startDate, endDate);
            return list;
        }
        
        [HttpGet]
        public dynamic GetPurchase()
        {
            int id = Fun.Query("id", 0);
            PurchaseBLL bll = new PurchaseBLL();
            Purchase p = bll.GetPurchase(id);
            return p;
        }

        [HttpPost]
        public dynamic AddPurchase()
        {
            string name = Fun.Form("name");
            string date = Fun.Form("date");
            string num = Fun.Form("num");
            string price = Fun.Form("price");
            string money = Fun.Form("money");
            int supplierId = Fun.Form("supplierId", 0);
            PurchaseBLL bll = new PurchaseBLL();
            if (!bll.AddPurchase(date, name, num, price, money, supplierId))
                return "失败";
            else
                return "成功";
        }
    }
}