<%@ WebHandler Language="C#" Class="weixin_login" %>

using System;
using System.Web;
using System.Configuration;//读取web.config中的appSettings的值
using Senparc.Weixin.MP.AdvancedAPIs;

public class weixin_login : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        string typeid=  context.Request.Form["typeid"];
        //string uerweixinid="a_"+context.Request["aid"]+"_z";

        //LogTextHelper.Info("我是用户验证中的用户的appid：" + uerweixinid);

        string wxAppid = ConfigurationManager.AppSettings["WeixinAppid"];//

        string temp1="";
        if (typeid == "1")
        {
            temp1 = OAuthApi.GetAuthorizeUrl(wxAppid, "http://www.dq006.com/sq_add.aspx", "a_z", Senparc.Weixin.MP.OAuthScope.snsapi_userinfo);
        }

        if (typeid == "2")
        {
            temp1 = OAuthApi.GetAuthorizeUrl(wxAppid, "http://www.dq006.com/money_sql.aspx", "a_z", Senparc.Weixin.MP.OAuthScope.snsapi_userinfo);
        }

        context.Response.ContentType = "text/plain";
        context.Response.Write(temp1);

        HttpContext.Current.Response.Redirect(temp1);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}