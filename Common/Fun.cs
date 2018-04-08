using System;
using System.Collections.Generic;
using System.Text;


public class Fun
{
    public static string Query(string key)
    {
        if (System.Web.HttpContext.Current.Request.QueryString[key] != null)
            return System.Web.HttpContext.Current.Request.QueryString[key];
        else
            return "";
    }
    public static int Query(string key, int value)
    {
        return ToInt(Query(key), value);
    }

    public static string Form(string key)
    {
        if (System.Web.HttpContext.Current.Request.Form[key] != null)
            return System.Web.HttpContext.Current.Request.Form[key];
        else
            return "";
    }
    public static int Form(string key, int value)
    {
        return ToInt(Form(key), value);
    }

    /// <summary>
    /// 转换为整形
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int ToInt(object obj, int defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        int result = int.TryParse(obj.ToString(), out result) ? result : defaultValue;
        return result;
    }

    public static string ToDecimal(object obj, decimal defaultValue)
    {
        if (obj == null)
        {
            return defaultValue.ToString();
        }
        decimal result = decimal.TryParse(obj.ToString(), out result) ? result : defaultValue;

        return result == 0 ? "0" : string.Format("{0:N}", result);
    }
}

