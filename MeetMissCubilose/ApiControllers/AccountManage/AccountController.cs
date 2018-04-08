using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel;
using BLL;

namespace MissMeetCubilose.ApiControllers.AccountManage
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        [HttpGet]
        public dynamic GetAccountList()
        {
            int year = Fun.Query("year",0);
            int month = Fun.Query("month",0);
            AccountBLL bll = new AccountBLL();
            return bll.GetAccountList(year, month);
        }

        [HttpGet]
        public dynamic GetRevenueList()
        {
            int year = Fun.Query("year", 0);
            int month = Fun.Query("month", 0);
            AccountBLL bll = new AccountBLL();
            return bll.GetRevenueList(year, month);
        }

        [HttpGet]
        public dynamic GetPurchaseOutList()
        {
            int year = Fun.Query("year", 0);
            int month = Fun.Query("month", 0);
            AccountBLL bll = new AccountBLL();
            return bll.GetPurchaseOutList(year, month);
        }

        [HttpGet]
        public dynamic GetDeliveryOutList()
        {
            int year = Fun.Query("year", 0);
            int month = Fun.Query("month", 0);
            AccountBLL bll = new AccountBLL();
            return bll.GetDeliveryOutList(year, month);
        }
       
    }
}