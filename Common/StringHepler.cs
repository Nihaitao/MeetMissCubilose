using DbAttribute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;


public class UpdateHelper
{
    public static void SetModelValue<TSource>(TSource source, NameValueCollection collection)
    {
        var sourceType = source.GetType();
        var properties = sourceType.GetProperties();
        foreach (var key in collection.AllKeys)
        {
            var value = collection[key];
            var property = properties.Where(x => x.Name.Equals(key, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (property != null)
            {
                object _value = null;
                if (property.PropertyType.FullName.StartsWith("System."))
                {
                    _value = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(source, _value, null);
                }
            }
        }
    }
}

public static class StringHepler
{
    public static bool IsInt(this string str)
    {
        return IsMatch(str, @"^\d+");
    }

    public static T ToModel<T>(this System.Collections.Specialized.NameValueCollection ValueCollection) where T : new()
    {
        T ts = new T();
        Type t = typeof(T);
        foreach (var key in ValueCollection.AllKeys)
        {
            try
            {
                Type tt = t.GetProperty(key).GetMethod.ReturnType;
                t.GetProperty(key).SetValue(ts, Convert.ChangeType(Fun.Query(key), tt));
            }
            catch (Exception ex) { }
        }

        return (T)ts;
    }


    public static DataTable ToDataTable<T>(this IEnumerable<T> list, bool flg = false)
    {
        //创建属性的集合    
        List<PropertyInfo> pList = new List<PropertyInfo>();
        //获得反射的入口    

        Type type = typeof(T);
        DataTable dt = new DataTable();
        //把所有的public属性加入到集合 并添加DataTable的列    
        Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
        foreach (var item in list)
        {
            //创建一个DataRow实例    
            DataRow row = dt.NewRow();
            //给row 赋值    
            pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
            //加入到DataTable    
            dt.Rows.Add(row);
        }
        if (flg)
        {

            var t = typeof(T);
            var classAttribute = (VersionAttribute)Attribute.GetCustomAttribute(t, typeof(VersionAttribute));
            if (classAttribute != null)
            {
                IEnumerable<PropertyInfo> pi = GetSettableProps(t);
                pi = pi.Where(p => p.GetCustomAttribute(typeof(VersionAttribute)) != null).ToArray();
                if (pi.Count() > 0)
                {
                    foreach (DataColumn item in dt.Columns)
                    {
                        PropertyInfo pp = pi.Where(p => p.Name == item.ColumnName).FirstOrDefault();
                        if (pp != null)
                        {
                            var classAttribute1 = (VersionAttribute)Attribute.GetCustomAttribute(pp, typeof(VersionAttribute));

                            if (classAttribute1 != null)
                            {
                                item.ColumnName = classAttribute1.Name;
                            }
                        }
                    }
                }
            }

        }
        return dt;
    }

    internal static MethodInfo GetPropertySetter(PropertyInfo propertyInfo, Type type)
    {
        return propertyInfo.DeclaringType == type ?
            propertyInfo.GetSetMethod(true) :
            propertyInfo.DeclaringType.GetProperty(propertyInfo.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetSetMethod(true);
    }

    internal static List<PropertyInfo> GetSettableProps(Type t)
    {
        return t
              .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
              .Where(p => GetPropertySetter(p, t) != null)
              .ToList();
    }


    public static int ToInt(this string str)
    {
        return ToInt(str, 0);
    }
    public static int ToInt(this string str, int defaultvalue)
    {
        int result = int.TryParse(str, out result) ? result : defaultvalue;
        return result;
    }
    public static string ToSubstring(this string str, int starIndex, int Length)
    {
        if (str.Length >= Length)
            str = str.Substring(0, Length);
        else
            str = str.Substring(0, str.Length);
        return str;
    }
    public static string ToSubstring(this string str, int starIndex, int Length, string fuhao)
    {
        if (str.Length >= Length)
            str = str.Substring(0, Length) + fuhao;
        else
            str = str.Substring(0, str.Length);
        return str;
    }
    public static bool IsDateTime(this string str)
    {
        DateTime date;
        return DateTime.TryParse(str, out date);
    }
    public static DateTime ToDateTime(this string str)
    {
        return ToDateTime(str, new DateTime());
    }
    public static DateTime ToDateTime(this string str, DateTime defaultvalue)
    {
        DateTime result = DateTime.TryParse(str, out result) ? result : defaultvalue;
        return result;
    }
    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="ds"></param>
    /// <returns></returns>
    public static bool IsNotNull(this System.Data.DataSet ds)
    {
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            return true;
        else
            return false;
    }
    public static bool IsNotNull(this System.Data.DataTable dt)
    {
        if (dt != null && dt.Rows.Count > 0)
            return true;
        else
            return false;
    }
    public static float ToFloat(this string str)
    {
        return ToFloat(str, 0);
    }
    public static float ToFloat(this string str, float defaultvalue)
    {
        float result = float.TryParse(str, out result) ? result : defaultvalue;
        return result;
    }

    public static decimal ToDecimal(this string str)
    {
        return ToDecimal(str, 0);
    }
    public static decimal ToDecimal(this string str, decimal defaultvalue)
    {
        decimal result = decimal.TryParse(str, out result) ? result : defaultvalue;
        return result;
    }

    public static bool IsMatch(this string str, string strRegex)
    {
        return Regex.IsMatch(str, strRegex);
    }

    public static bool IsEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }
    /// <summary>   
    /// 得到字符串的长度，一个汉字算2个字符   
    /// </summary>   
    /// <param name="str">字符串</param>   
    /// <returns>返回字符串长度</returns>   
    public static int Length(this string str)
    {
        if (str.Length == 0) return 0;

        ASCIIEncoding ascii = new ASCIIEncoding();
        int tempLen = 0;
        byte[] s = ascii.GetBytes(str);
        for (int i = 0; i < s.Length; i++)
        {
            if ((int)s[i] == 63)
            {
                tempLen += 2;
            }
            else
            {
                tempLen += 1;
            }
        }
        return tempLen;
    }

    public static string ListTostring<T>(this IEnumerable<T> list, string fg=",")
    {
        var str = new StringBuilder();
        var enumerable = list as T[] ?? list.ToArray();
        for (int i = 0; i < enumerable.Count(); i++)
        {
            str.Append(enumerable[i].ToString());
            if (i < enumerable.Count() - 1)
            {
                str.Append(fg);
            }
        }
        return str.ToString();
    }

    public static List<string> StringToList(this string str, string fg=",")
    {
        return str.Split(new string[] {fg}, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static List<int> StringToIntList(this string str, string fg=",")
    {
        List<string> strs = str.Split(new string[] { fg }, StringSplitOptions.RemoveEmptyEntries).ToList();
        List<int> ints=new List<int>();
        foreach (var item in strs)
        {
            int resout;
            int.TryParse(item, out resout);
            ints.Add(resout);
        }
        return ints;
    }


}

