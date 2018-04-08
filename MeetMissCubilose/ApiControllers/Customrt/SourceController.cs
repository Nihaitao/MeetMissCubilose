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
    [RoutePrefix("api/Source")]
    public class SourceController : ApiController
    {
        [HttpGet]
        public dynamic GetSourceList()
        {
            CustomerBLL c = new CustomerBLL();
            List<C_CustomerSource> list = c.GetSourceList();
            return list;
        }
        [HttpGet]
        public dynamic GetSearchSourceList()
        {
            string name = Fun.Query("name");
            CustomerBLL c = new CustomerBLL();
            List<C_CustomerSource> list = c.GetSearchSourceList(name);
            return list;
        }
        [HttpGet]
        public dynamic GetSource()
        {
            int id = Fun.Query("id", 0);
            CustomerBLL c = new CustomerBLL();
            C_CustomerSource s = c.GetSource(id);
            CustomerSource source = new CustomerSource();
            source.id = s.source_id;
            source.name = s.source_name.Trim();
            return source;
        }

        [HttpPost]
        public dynamic AddSource()
        {
            string name = Fun.Form("name");
            CustomerBLL c = new CustomerBLL();
            if (!c.AddSource(name))
                return "失败";
            else
                return "成功";
        }

        [HttpPost]
        public dynamic UpdateSource()
        {
            int id = Fun.Form("id", 0);
            string name = Fun.Form("name");
            CustomerBLL c = new CustomerBLL();
            if (id == 1 || !c.UpdateSource(id, name))//ID为1是系统数据，不能修改
                return "失败";
            else
                return "成功";
        }
        [HttpPost]
        public dynamic DeleteSource()
        {
            int id = Fun.Form("id", 0);
            CustomerBLL c = new CustomerBLL();
            if (id == 1 || !c.DeleteSource(id))//ID为1是系统数据，不能删除
                return "失败";
            else
                return "成功";
        }
    }
}