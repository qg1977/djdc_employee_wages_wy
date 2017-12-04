<%@ WebHandler Language="C#" Class="DIY_menu" %>

using System;
using System.Web;
using System.Configuration;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities.Menu;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;

public class DIY_menu : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
        //string WeixinAppId = ConfigurationManager.AppSettings["WeixinAppId"];
        //string WeixinAppSecret = ConfigurationManager.AppSettings["WeixinAppSecret"];

        //AccessTokenContainer.TryGetAccessToken(WeixinAppId, WeixinAppSecret);
        //string access_token = AccessTokenContainer.GetAccessToken(WeixinAppId);
        string WeixinAppId = System.Configuration.ConfigurationManager.AppSettings["WeixinAppId"];
        string WeixinAppSecret =System.Configuration.ConfigurationManager.AppSettings["WeixinAppSecret"];

        ButtonGroup bg = new ButtonGroup();

        //单击
        bg.button.Add(new SingleViewButton()
        {

            name = "1.提交申请",
            //key = "OneClick",
            //type = ButtonType.click.ToString(),//默认已经设为此类型，这里只作为演示
            //url = "http://www.dq006.com/weixin_login.ashx?typeid=1"
            url="http://www.dq006.com/sq_add.aspx"
        });

        bg.button.Add(new SingleViewButton()
        {
            name = "2.查询工资",
            //url = "http://www.dq006.com/weixin_login.ashx?typeid=2"
            url="http://www.dq006.com/money_sql.aspx"
        });

        //bg.button.Add(new SingleViewButton()
        //{
        //    name = "3.测试",
        //    url = "http://www.dq006.com/temp1.aspx"
        //});

        //二级菜单
        var subButton = new SubButton()
        {
            name = "二级菜单"
        };
        subButton.sub_button.Add(new SingleViewButton()
        {
            url = "http://www.dq006.com/temp.aspx",
            name = "temp测试"
        });
        subButton.sub_button.Add(new SingleViewButton()
        {
            url = "http://www.dq006.com/temp1.aspx",
            name = "temp1测试"
        });
        subButton.sub_button.Add(new SingleViewButton()
        {
            url = "http://www.dq006.com/weixin.aspx",
            name = "weixin测试"
        });
        bg.button.Add(subButton);
        //subButton.sub_button.Add(new SingleClickButton()
        //{
        //    key = "SubClickRoot_Text",
        //    name = "验证"
        //});
        //subButton.sub_button.Add(new SingleClickButton()
        //{
        //    key = "SubClickRoot_News",
        //    name = "返回图文"
        //});
        //subButton.sub_button.Add(new SingleClickButton()
        //{
        //    key = "SubClickRoot_Music",
        //    name = "返回音乐"
        //});
        //subButton.sub_button.Add(new SingleViewButton()
        //{
        //    url = "http://weixin.senparc.com",
        //    name = "Url跳转"
        //});
        //subButton.sub_button.Add(new SingleViewButton()
        //{
        //    url = "http://www.dq006.com/weixin_login.ashx",
        //    name = "用户验证"
        //});
        //bg.button.Add(subButton);

        var result = CommonApi.CreateMenu(WeixinAppId, bg);

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}