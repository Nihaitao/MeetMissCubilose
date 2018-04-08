using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel;
using BLL;

namespace MissMeetCubilose.ApiControllers.Login
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        [HttpGet]
        public dynamic Login()
        {
            string name = Fun.Query("name");
            string password = Fun.Query("password");
            LoginBLL bll = new LoginBLL();
            S_UserInfo user = bll.Login(name, password);
            if (user == null)
            {
                return "失败";
            }
            UserInfo u = new UserInfo();
            u.id = user.user_id;
            u.name = user.user_name.Trim();
            ManagerBLL m = new ManagerBLL();
            S_ManagerInfo manager = m.GetManagerByLoginId(user.user_id);
            if (manager != null)
            {
                return new { data = u, manager = manager };
            }
            else
            {
                CustomerBLL c = new CustomerBLL();
                C_CustomerInfo customer = c.GetCustomerByLoginId(user.user_id);
                return new { data = u, customer = customer };
            }

        }


        [HttpPost]
        public dynamic Register()
        {
            string name = Fun.Form("name");
            string password = Fun.Form("password");
            string openId = Fun.Form("openId");
            LoginBLL c = new LoginBLL();
            if (c.GetLoginByName(name) != null)
                return "该名称已被注册，请重试！";
            int loginId = c.Register(name, password, openId);
            if (loginId == 0)
                return "注册失败";
            else
            {
                int cu_sourceID = 1;//此客户来源为系统原数据
                int manageID = 1;//此管理员为系统管理员
                string imgUrl = "../../img/default/default-min.png";//默认头像
                new CustomerBLL().AddCustomer(loginId, "游客" + name, -1, "", 1, cu_sourceID, 0, manageID, 0, "", "", "", "", 100, imgUrl);//客户来源1，管理员1为系统数据
            }
            return "注册成功";
        }


        [HttpGet]
        public dynamic GetLoginList()
        {
            BLL.LoginBLL l = new BLL.LoginBLL();
            List<S_UserInfo> list = l.GetLoginList();
            List<UserInfo> uList = new List<UserInfo>();
            UserInfo user = null;
            foreach (var item in list)
            {
                user = new UserInfo();
                user.id = item.user_id;
                user.name = item.user_name.Trim();
                uList.Add(user);
            }
            return uList;
        }

    }
}
