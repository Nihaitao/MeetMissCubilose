using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using DAL;

namespace BLL
{
    public class LoginBLL
    {
        private static MSSQLMy ms = new MSSQLMy();
        public S_UserInfo Login(string name, string password)
        {
            string sql = "select * from s_userInfo where user_name ='" + name + "' and user_pwd='" + password + "'";
            return ms.GetModel<S_UserInfo>(sql);
        }
        public int Register(string name, string password, string openId)
        {
            DateTime time = DateTime.Now;
            S_UserInfo user = new S_UserInfo();
            user.user_name = name;
            user.user_pwd = password;
            user.user_state = 1;
            user.user_time = time;
            user.user_wx_id = openId;
            if (ms.Insert(user))
                return user.user_id;
            else
                return 0; 
            
        }

        public List<S_UserInfo> GetLoginList()
        {
            string sql = "select user_id, user_name from s_userInfo";
            return ms.GetListModel<S_UserInfo>(sql);
        }

        public string GetLoginName(int user_id)
        {
            string sql = "select user_name from s_userInfo where user_id =" + user_id;
            return ms.GetModel<S_UserInfo>(sql).user_name;
        }

        public S_UserInfo GetLoginByName(string name)
        {
            string sql = "select * from s_userInfo where user_name ='" + name + "'";
            return ms.GetModel<S_UserInfo>(sql);
        }
    }
}