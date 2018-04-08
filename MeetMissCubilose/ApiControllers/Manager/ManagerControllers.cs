using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel;
using BLL;

namespace MissMeetCubilose.ApiControllers.Manager
{
    [RoutePrefix("api/Manager")]
    public class ManagerController : ApiController
    {
        [HttpGet]
        public dynamic GetManagerList()
        {
            ManagerBLL m = new ManagerBLL();
            List<S_ManagerInfo> list = m.GetManagerList();
            List<ManagerInfo> mList = new List<ManagerInfo>();
            ManagerInfo manager = null;
            foreach (var item in list)
            {
                manager = new ManagerInfo();
                manager.id = item.mgr_id;
                manager.name = item.mgr_name.Trim();
                manager.img = item.mgr_headimgurl;
                mList.Add(manager);
            }
            return mList;
        }
         [HttpGet]
        public dynamic GetSearchManagerList()
        {
            string name = Fun.Query("name");
            ManagerBLL m = new ManagerBLL();
            List<S_ManagerInfo> list = m.GetSearchManagerList(name);
            List<ManagerInfo> mList = new List<ManagerInfo>();
            ManagerInfo manager = null;
            foreach (var item in list)
            {
                manager = new ManagerInfo();
                manager.id = item.mgr_id;
                manager.name = item.mgr_name.Trim();
                manager.img = item.mgr_headimgurl;
                mList.Add(manager);
            }
            return mList;
        }
        
        [HttpGet]
        public dynamic GetManager()
        {
            int id = Fun.Query("id",0);
            ManagerBLL m = new ManagerBLL();
            S_ManagerInfo mgr = m.GetManager(id);            
            ManagerInfo manager = new ManagerInfo();
            manager.id = mgr.mgr_id;
            manager.loginId = mgr.mgr_login_id;
            manager.name = mgr.mgr_name.Trim();
            manager.img = mgr.mgr_headimgurl;
            manager.time = mgr.mgr_time.ToString("yyyy-MM-dd hh:MM:ss");          
            return manager;
        }

        [HttpPost]
        public dynamic AddManager()
        {
            string name = Fun.Form("name");
            int loginId = Fun.Form("loginId", 0);
            string headImg = "../../img/default/default-a.png";
            ManagerBLL m = new ManagerBLL();
            if (!m.AddManager(name, headImg, loginId))
                return "失败";
            else
                return "成功";
        }

        [HttpPost]
        public dynamic UpdateManager()
        {
            int id = Fun.Form("id", 0);
            string name = Fun.Form("name");
            string headImg = Fun.Form("headImg");
            ManagerBLL m = new ManagerBLL();
            if (id == 1 || !m.UpdateManager(id, name, headImg))
                return "失败";
            else
                return "成功";
        }

        [HttpPost]
        public dynamic UpdateManagerName()
        {
            int id = Fun.Form("id", 0);
            string name = Fun.Form("name");
            ManagerBLL m = new ManagerBLL();
            if (id == 1 || !m.UpdateManagerName(id, name))//系统管理员不能被修改名称
                return "失败";
            else
                return "成功";
        }
        [HttpPost]
        public dynamic UpdateManagerImg()
        {
            int id = Fun.Form("id", 0);
            string img = Fun.Form("img");
            ManagerBLL m = new ManagerBLL();
            if (!m.UpdateManagerImg(id, img))
                return "失败";
            else
                return "成功";
        }
    }
}
