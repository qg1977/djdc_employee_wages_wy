using Senparc.Weixin.MessageHandlers;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;
using Senparc.Weixin.MP.MessageHandlers;

public partial class temp1 : System.Web.UI.Page
{
    string WeixinAppId = ConfigurationManager.AppSettings["WeixinAppId"];
    string WeixinAppSecret = ConfigurationManager.AppSettings["WeixinAppSecret"];
    string Token = ConfigurationManager.AppSettings["WeixinToken"];

    string WeixinEncodingAESKey = ConfigurationManager.AppSettings["WeixinEncodingAESKey"];//消息加密密锁
    protected void Page_Load(object sender, EventArgs e)
    {
        //post method - 当有用户想公众账号发送消息时触发
        var postModel = new PostModel()
        {
            Signature = Request.QueryString["signature"],
            Msg_Signature = Request.QueryString["msg_signature"],
            Timestamp = Request.QueryString["timestamp"],
            Nonce = Request.QueryString["nonce"],
            //以下保密信息不会（不应该）在网络上传播，请注意
            Token = Token,
            EncodingAESKey = WeixinEncodingAESKey,// "mNnY5GekpChwqhy2c4NBH90g3hND6GeI4gii2YCvKLY",//根据自己后台的设置保持一致
            AppId = WeixinAppId// "wx669ef95216eef885"//根据自己后台的设置保持一致
        };

        var maxRecordCount = 10;
        var messageHandler = new CustomMessageHandler(Request.InputStream, postModel, maxRecordCount);

        //string access_token = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(WeixinAppId, WeixinAppSecret);
        //LogTextHelper_x.Info("特别的你access_token是：" + access_token);


        messageHandler.RequestDocument.Save(
            Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Request_" +
                           messageHandler.RequestMessage.FromUserName + ".txt"));

        //string wxAppid = ConfigurationManager.AppSettings["WeixinAppid"];//

        //Response.Write("<br/>应该跳转才对");

        //string temp1 = OAuthApi.GetAuthorizeUrl(wxAppid, "http://www.dq006.com/sq_add.aspx", "a_z", Senparc.Weixin.MP.OAuthScope.snsapi_userinfo);

        ////Response.Write("<br/>地址为："+temp1);

        ////HttpContext.Current.Response.Redirect(temp1);
        //Response.Redirect(temp1);
    }
}