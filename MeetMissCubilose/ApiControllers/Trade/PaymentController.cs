using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel;
using BLL;

namespace MissMeetCubilose.ApiControllers.Trade
{
    [RoutePrefix("api/Trade")]
    public class PaymentController : ApiController
    {
        [HttpGet]
        public dynamic GetPaymentList()
        {
            PaymentBLL bll = new PaymentBLL();
            List<P_Payment> list = bll.GetPaymentList();
            return list;
        }
        [HttpGet]
        public dynamic GetSearchPaymentList()
        {
            string name = Fun.Query("name");
            PaymentBLL bll = new PaymentBLL();
            List<P_Payment> list = bll.GetSearchPaymentList(name);
            return list;
        }
        
        [HttpGet]
        public dynamic GetPayment()
        {
            int id = Fun.Query("id", 0);
            PaymentBLL bll = new PaymentBLL();
            P_Payment p = bll.GetPayment(id);
            Payment payment = new Payment();
            payment.id = p.payment_id;
            payment.name = p.payment_name.Trim();
            return payment;
        }

        [HttpPost]
        public dynamic AddPayment()
        {
            string name = Fun.Form("name");
            PaymentBLL bll = new PaymentBLL();
            if (!bll.AddPayment(name))
                return "失败";
            else
                return "成功";
        }

        [HttpPost]
        public dynamic UpdatePayment()
        {
            int id = Fun.Form("id", 0);
            string name = Fun.Form("name");
            PaymentBLL bll = new PaymentBLL();
            if (!bll.UpdatePayment(id, name))
                return "失败";
            else
                return "成功";
        }

        [HttpPost]
        public dynamic DeletePayment()
        {
            int id = Fun.Form("id", 0);
            PaymentBLL bll = new PaymentBLL();
            if (!bll.DeletePayment(id))
                return "失败";
            else
                return "成功";
        }
    }
}