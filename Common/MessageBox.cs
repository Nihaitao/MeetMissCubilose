using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public class MessageBox
{
    /// <summary>
    /// 执行脚本
    /// </summary>
    /// <param name="page"></param>
    /// <param name="script"></param>
    public static void ResponseScript(Page page, string script)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript'>" + script + "</script>");
    }

    /// <summary>
    /// 注册客户端脚本
    /// </summary>
    /// <param name="MethodsName">方法名字或要执行的Script语句</param>
    public static void RegisterScript(string MethodsName)
    {
        System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        if (!page.ClientScript.IsClientScriptBlockRegistered(page.GetType(), "clienterScript"))
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "clientScript", "<script language=javascript>\n" + MethodsName + "\n</script>");

    }

    /// <summary>
    /// 对话框
    /// </summary>
    /// <param name="msg"></param>
    public static void Alert(string msg)
    {
        msg = msg.Replace("\n", "");
        msg = msg.Replace("\r", "");
        msg = msg.Replace("'", "");
        RegisterScript("alert('" + msg + "')");

    }

    /// <summary>
    /// 弹出提示框
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg"></param>
    public static void Show(Page page, string msg)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
    }

    /// <summary>
    /// 弹出提示框 刷新
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg"></param>
    /// <param name="url"></param>
    public static void AlertAndRedirect(Page page, string msg, string url)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<script language='javascript' defer>");
        builder.AppendFormat("alert('{0}');", msg);
        builder.AppendFormat("location.href='{0}'", url);
        builder.Append("</script>");
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", builder.ToString());
    }

    /// <summary>
    /// 弹出提示框 刷新
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg"></param>
    /// <param name="url"></param>
    public static void AlertAndRedirectTop(Page page, string msg, string url)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<script language='javascript' defer>");
        builder.AppendFormat("alert('{0}');", msg);
        builder.AppendFormat("top.location.href='{0}'", url);
        builder.Append("</script>");
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", builder.ToString());
    }

    /// <summary>
    /// 添加点击提示确认
    /// </summary>
    /// <param name="Control"></param>
    /// <param name="msg"></param>
    public static void ShowConfirm(WebControl Control, string msg)
    {
        Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
    }

    /// <summary>
    /// 添加点击提示确认
    /// </summary>
    /// <param name="Control"></param>
    /// <param name="msg"></param>
    public static void ShowConfirm_Top(string msg, string YesUrl, string NoUrl)
    {
        msg = msg.Replace("\n", "");
        msg = msg.Replace("\r", "");
        RegisterScript("if (confirm('" + msg + "')){top.location.href='" + YesUrl + "'}else{top.location.href='" + NoUrl + "'}");
    }

    /// <summary>
    /// 添加点击提示确认
    /// </summary>
    /// <param name="Control"></param>
    /// <param name="msg"></param>
    public static void ShowConfirm(string msg, string YesUrl, string NoUrl)
    {
        msg = msg.Replace("\n", "");
        msg = msg.Replace("\r", "");
        RegisterScript("if (confirm('" + msg + "')){location.href='" + YesUrl + "'}else{location.href='" + NoUrl + "'}");
    }

    /// <summary>
    /// artDialog提示及刷新父窗口
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg"></param>
    public static void artDialogParentRefresh(Page page, string msg)
    {
        MessageBox.ResponseScript(page, "alert('" + msg + "');artDialog.open.origin.location.reload();");
    }
    /// <summary>
    /// artDialog提示、关闭当前窗口及刷新父窗口
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg"></param>
    public static void artDialogCloseParentRefresh(Page page, string msg)
    {
        Alert(msg);
        MessageBox.ResponseScript(page, "artDialog.open.origin.location.reload();artDialog.close();");
    }

    /// <summary>
    /// artDialog提示、关闭当前窗口及跳转页面
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg"></param>
    /// <param name="Url"></param>
    public static void artDialogCloseRefreshUrl(Page page, string msg, string Url)
    {
        Alert(msg);
        MessageBox.ResponseScript(page, "artDialog.open.origin.location.href='" + Url + "';artDialog.close();");
    }

}
