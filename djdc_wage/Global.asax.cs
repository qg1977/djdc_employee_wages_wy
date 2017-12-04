using Senparc.Weixin.MP.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace djdc_wage
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //LogTextHelper.Info("网站第一次运行才有,改一下");
            AccessTokenContainer.Register(
                  System.Configuration.ConfigurationManager.AppSettings["WeixinAppId"],
                  System.Configuration.ConfigurationManager.AppSettings["WeixinAppSecret"]);//全局只需注册一次，例如可以放在Global的Application_Start()方法中。

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}