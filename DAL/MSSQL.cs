using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Wx_DAL
{
    public class MSSQL : MSSQLMy
    {
        public MSSQL()
        {
            Databases db = new Databases();

            if (string.IsNullOrEmpty(db.ConnectionString))
            {
                MSSQLMy My = new MSSQLMy();

                string Domain = MSSQLMy.GetDomain;
                string sql = "select top 1 * from H_Station where ( B_DomainName=@Domain or B_DomainName_Default=@Domain or R_DomainName=@Domain or R_DomainName_Default=@Domain or ImagesDomain=@Domain or WeiXin_DomainName=@Domain or WeiXin_DomainName_Default=@Domain or WeiXinQiye_DomainName=@Domain or WeiXinQiye_DomainName_Default=@Domain)";
                var param = new
                {
                    Domain = Domain
                };

                
            }

            ConnectionString = db.ConnectionString;
        }
    }
}
