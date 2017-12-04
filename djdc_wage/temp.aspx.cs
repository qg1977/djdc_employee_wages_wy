using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace djdc_wage
{
    public partial class temp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string wxAppid = ConfigurationManager.AppSettings["WeixinAppid"];//

            Response.Write("<br/>应该跳转才对");

            string temp1 = OAuthApi.GetAuthorizeUrl(wxAppid, "http://www.dq006.com/sq_add.aspx", "a_z", Senparc.Weixin.MP.OAuthScope.snsapi_userinfo);

            //Response.Write("<br/>地址为："+temp1);

            //HttpContext.Current.Response.Redirect(temp1);
            Response.Redirect(temp1);
        }
    }
}