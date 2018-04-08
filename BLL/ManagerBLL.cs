using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using DAL;

namespace BLL
{
    public class ManagerBLL
    {
        private static MSSQLMy ms = new MSSQLMy();
        public List<S_ManagerInfo> GetManagerList()
        {
            string sql = "select * from s_managerInfo";
            return ms.GetListModel<S_ManagerInfo>(sql);
        }

        public bool AddManager(string name, string headImg, int loginId)
        {
            DateTime time = DateTime.Now;
            string sql = "insert into s_managerInfo values(@loginId,@name,@headImg,@time)";
            return ms.Insert(sql, new { loginId, name, headImg, time });
        }

        public S_ManagerInfo GetManager(int id)
        {
            string sql = "select * from s_managerInfo where mgr_id = " + id;
            return ms.GetModel<S_ManagerInfo>(sql);
        }
        public S_ManagerInfo GetManagerByLoginId(int id)
        {
            string sql = "select * from s_managerInfo where mgr_login_id = " + id;
            return ms.GetModel<S_ManagerInfo>(sql);
        }

        public bool UpdateManager(int id, string name, string headImg)
        {
            string sql = "update s_managerInfo set mgr_name = @name,mgr_headimgurl = @headImg where mgr_id = @id";
            return ms.Update(sql, new { id, name, headImg });
        }

        public List<S_ManagerInfo> GetSearchManagerList(string name)
        {
            string sql = "select * from s_managerInfo where mgr_name like '%" + name + "%'";
            return ms.GetListModel<S_ManagerInfo>(sql);
        }

        public bool UpdateManagerName(int id, string name)
        {
            string sql = "update s_managerInfo set mgr_name = @name where mgr_id = @id";
            return ms.Update(sql, new { id, name });
        }

        public bool UpdateManagerImg(int id, string img)
        {
            string sql = "update s_managerInfo set mgr_headimgurl = @img where mgr_id = @id";
            return ms.Update(sql, new { id, img });
        }
    }
}