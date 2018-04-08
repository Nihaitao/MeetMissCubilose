using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections;
namespace Common
{
    /// <summary>
    ///DataTableToJson 的摘要说明
    /// </summary>
    public class DataTableToJson
    {
        public DataTableToJson()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// Json转Table
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public DataTable ToDataTable(string json)
        {
            DataTable dataTable = new DataTable();  //实例化  
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }
                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中  
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }

        /// <summary>
        /// Table转Json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ToJson(DataTable dt)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            ArrayList arrayList = new ArrayList();
            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合  
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值  
            }
            return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串  
        }
        public string ToJson(DataTable dt, int Status)
        {
            string jsons = string.Empty;
            jsons = "[{\"Status\":\"" + Status + "\",\"Error\":\"\",\"List\":";
            if (dt != null && dt.Rows.Count > 0)
                jsons += new DataTableToJson().ToJson(dt);
            else
                jsons += "[]";
            jsons += "}]";
            return jsons;
        }
        public string ToJson(DataTable dt, int Status, string Error)
        {
            string jsons = string.Empty;
            jsons = "[{\"Status\":\"" + Status + "\",\"Error\":\"" + Error + "\",\"List\":";
            if (dt != null && dt.Rows.Count > 0)
                jsons += new DataTableToJson().ToJson(dt);
            else
                jsons += "[]";
            jsons += "}]";
            return jsons;
        }
    }
}