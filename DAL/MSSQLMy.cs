using DbAttribute;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class MSSQLMy
    {
        //连接数据库字符串。

        public string ConnectionString = "server=(local);database=mySql;Integrated Security=True";
        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }


        private string _ErrorMessage = "";
        public string ErrorMessage
        {
            set { _ErrorMessage = value; }
            get { return _ErrorMessage; }
        }

        public T GetModel<T>(T obj) where T : class
        {
            using (IDbConnection conn = OpenConnection())
            {
                try
                {
                    return conn.QueryByKey<T>(obj);
                }
                catch (Exception e)
                {
                    ErrorMessage = "查询失败：" + e.Message;
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public T GetModel<T>(string sql, object param = null)
        {
            using (IDbConnection conn = OpenConnection())
            {
                try
                {
                    return conn.Query<T>(sql, param).ToList().FirstOrDefault();
                }
                catch (Exception e)
                {
                    ErrorMessage = "查询失败：" + e.Message;
                    return default(T);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public List<T> GetListModel<T>(string sql, object param = null)
        {
            using (IDbConnection conn = OpenConnection())
            {
                try
                {
                    return conn.Query<T>(sql, param).ToList();
                }
                catch (Exception e)
                {
                    ErrorMessage = "查询失败：" + e.Message;
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<T> GetListModelByPage<T>(string sql, object param = null)
        {
            sql = "select * from (" + sql + ") page where rows between @StarCount and @EndCount";
            return GetListModel<T>(sql, param);
        }

        public bool Transaction(List<Dictionary<string, object>> List)
        {
            using (IDbConnection conn = OpenConnection())
            {
                var trans = conn.BeginTransaction();
                try
                {
                    //开户事务
                    foreach (Dictionary<string, object> Values in List) //循环IList<Dictionary<string, object>>
                    {
                        if (Values.Count > 2)
                        {
                            string sql = "";
                            switch (Values["tablesign"].ToString().ToLower())
                            {
                                case "insert":
                                    sql = "insert " + Values["tablename"].ToString() + " (";
                                    foreach (var item in Values.Where(a => a.Key != "tablesign" && a.Key != "tablename" && a.Key != "tablewhere"))
                                    {
                                        sql += item.Key + ",";
                                    }
                                    sql = sql.TrimEnd(',') + ") values(";
                                    foreach (var item in Values.Where(a => a.Key != "tablesign" && a.Key != "tablename" && a.Key != "tablewhere"))
                                    {
                                        sql += "'" + item.Value + "',";
                                    }
                                    sql = sql.TrimEnd(',') + ")";
                                    break;
                                case "update":
                                    sql = "update " + Values["tablename"].ToString() + " set ";
                                    foreach (var item in Values.Where(a => a.Key != "tablesign" && a.Key != "tablename" && a.Key != "tablewhere"))
                                    {
                                        sql += item.Key + "='" + item.Value + "',";
                                    }
                                    if (Values.Count > 3)
                                        sql = sql.TrimEnd(',') + " where " + Values["tablewhere"].ToString();
                                    break;
                                case "delete":
                                    sql = "delete " + Values["tablename"].ToString() + " where " + Values["tablewhere"].ToString();
                                    break;
                            }
                            conn.Execute(sql, null, trans, null, null);
                        }
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }


        /// <summary>
        /// 执行事务  SQL 
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public bool ExecuteSqlTran(List<String> SQLStringList, object obj = null)
        {
            using (IDbConnection conn = OpenConnection())
            {
                var trans = conn.BeginTransaction();
                try
                {

                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            conn.Execute(strsql, obj, trans, null, null);
                        }
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception)
                {

                    trans.Rollback();
                    return false;
                }
            }
        }
        public bool Insert<T>(T obj) where T : class
        {
            using (IDbConnection conn = OpenConnection())
            {
                try
                {
                    return conn.Insert(obj);
                }
                catch (Exception e)
                {
                    ErrorMessage = "插入失败：" + e.Message;
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool Insert<T>(object obj) where T : class
        {
            using (IDbConnection conn = OpenConnection())
            {
                try
                {
                    return conn.Insert(obj);
                }
                catch (Exception e)
                {
                    ErrorMessage = "插入失败：" + e.Message;
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool Insert(string sql, object obj = null)
        {
            using (IDbConnection conn = OpenConnection())
            {
                try
                {
                    return conn.Execute(sql, obj) > 0 ? true : false;
                }
                catch (Exception e)
                {
                    ErrorMessage = "更新失败：" + e.Message;
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 大数据插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TableName"></param>
        /// <param name="List"></param>
        /// <returns></returns>
        public bool Insert<T>(string TableName, List<T> List)
        {
            using (IDbConnection conn = OpenConnection())
            {
                try
                {
                    using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(ConnectionString, SqlBulkCopyOptions.UseInternalTransaction))
                    {
                        sqlbulkcopy.DestinationTableName = typeof(T).Name;

                        PropertyInfo[] props = typeof(T).GetProperties();
                        foreach (PropertyInfo prop in props)
                        {
                            sqlbulkcopy.ColumnMappings.Add(prop.Name, prop.Name);
                        }
                        sqlbulkcopy.WriteToServer(List.ToDataTable<T>());
                    }
                    return true;
                }
                catch (Exception e)
                {
                    ErrorMessage = "插入失败：" + e.Message;
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool Update(string sql, object obj = null)
        {
            using (IDbConnection conn = OpenConnection())
            {
                try
                {
                    return conn.Execute(sql, obj) >= 0 ? true : false;
                }
                catch (Exception e)
                {
                    ErrorMessage = "更新失败：" + e.Message;
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool Update<T>(T obj) where T : class
        {
            using (IDbConnection conn = OpenConnection())
            {
                try
                {
                    return conn.Update<T>(obj);
                }
                catch (Exception e)
                {
                    ErrorMessage = "更新失败：" + e.Message;
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool Update<T>(object obj) where T : class
        {
            using (IDbConnection conn = OpenConnection())
            {
                try
                {
                    return conn.Update<T>(obj);
                }
                catch (Exception e)
                {
                    ErrorMessage = "更新失败：" + e.Message;
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool Delete<T>(T obj) where T : class
        {
            using (IDbConnection conn = OpenConnection())
            {
                try
                {
                    return conn.DeleteByKey<T>(obj);
                }
                catch (Exception e)
                {
                    ErrorMessage = "删除失败：" + e.Message;
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool Delete(string sql, object obj = null)
        {
            using (IDbConnection conn = OpenConnection())
            {
                try
                {
                    return conn.Execute(sql, obj) >= 0 ? true : false;
                }
                catch (Exception e)
                {
                    ErrorMessage = "删除失败：" + e.Message;
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static string GetDomain
        {
            get
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Url.Authority))
                {
                    return HttpContext.Current.Request.Url.Authority;
                }
                else
                {
                    return "";
                }
            }
        }

    }
}
