using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace djdc_wage
{
    public partial class OAuthApi_sq_add : System.Web.UI.Page
    {
        string WeixinAppId = ConfigurationManager.AppSettings["WeixinAppId"];
        string WeixinAppSecret = ConfigurationManager.AppSettings["WeixinAppSecret"];
        protected void Page_Load(object sender, EventArgs e)
        {
            string uer_code = Request["code"].ToString();//code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。
                                                         //string uer_state = "oNOvqwo-qNYgd0d7bgV2rrn-s9Nc";// Request["state"].ToString();
            string uer_state = Request["state"].ToString();
            LogTextHelper.Info("传递进来的未知参数：" + uer_state);

            OAuthAccessTokenResult accToken = OAuthApi.GetAccessToken(WeixinAppId, WeixinAppSecret, uer_code);

            Response.Write("<br/>用户针对本公众号的唯一ID11" + accToken.access_token.ToString());
            //OAuthUserInfo uerinfo = OAuthApi.GetUserInfo(accToken.access_token.ToString(),uer_state);
            OAuthUserInfo uerinfo = OAuthApi.GetUserInfo(accToken.access_token.ToString(), uer_code);

            Response.Write("</br>");
            Response.Write("<b>用户的openid</b>:" + uerinfo.openid.ToString());
            Response.Write("</br>");
            Response.Write("<b>用户昵称</b>:" + uerinfo.nickname.ToString());
            Response.Write("</br>");
            Response.Write("<b>性别</b>:" + uerinfo.sex.ToString());
            Response.Write("</br>");
            Response.Write("<b>城市</b>:" + uerinfo.province.ToString());
            Response.Write("</br>");
            Response.Write("<img src=\"" + uerinfo.headimgurl.ToString() + "\"/ width=100 height=100 alt=\"" + uerinfo.nickname.ToString() + "\">");
            Response.Write("</br>");

        }
    }
}