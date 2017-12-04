using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace djdc_wage
{
    using Senparc.Weixin.MP;
    using Senparc.Weixin.MP.Entities.Request;
    using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;
    using System.Configuration;
    using System.IO;

    public partial class Weixin : System.Web.UI.Page
    {
        string WeixinAppId = ConfigurationManager.AppSettings["WeixinAppId"];
        string WeixinAppSecret = ConfigurationManager.AppSettings["WeixinAppSecret"];
        string Token = ConfigurationManager.AppSettings["WeixinToken"];

        string WeixinEncodingAESKey = ConfigurationManager.AppSettings["WeixinEncodingAESKey"];//消息加密密锁

        //private readonly string Token = "weixin";//与微信公众账号后台的Token设置保持一致，区分大小写。


        public DataAccess.Class1 dac = new DataAccess.Class1();
        string FileName;
        protected void Page_Load(object sender, EventArgs e)
        {
            string signature = Request["signature"];
            string timestamp = Request["timestamp"];
            string nonce = Request["nonce"];
            string echostr = Request["echostr"];

           
            if (Request.HttpMethod == "GET")
            {
                //get method - 仅在微信后台填写URL验证时触发
                if (CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    WriteContent(echostr); //返回随机字符串则表示验证通过
                }
                else
                {
                    WriteContent("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, Token) + "。" +
                                "看这里：：如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
                }
                Response.End();
            }
            else
            {
                //post method - 当有用户想公众账号发送消息时触发
                if (!CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    WriteContent("参数错误！");
                    return;
                }

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

                //v4.2.2之后的版本，可以设置每个人上下文消息储存的最大数量，防止内存占用过多，如果该参数小于等于0，则不限制
                var maxRecordCount = 10;

                var messageHandler = new CustomMessageHandler(Request.InputStream, postModel, maxRecordCount);



                try
                {

                    //自定义MessageHandler，对微信请求的详细判断操作都在这里面。


                    //使用完整的应用凭证获取Token
                    //Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(postModel.AppId, WeixinAppSecret);
                    string access_token = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken(WeixinAppId, WeixinAppSecret);
                    //string access_token = Senparc.Weixin.MP.Containers.AccessTokenContainer.GetAccessToken(WeixinAppId);
                    //LogTextHelper.Info("access_token是：" + access_token);

                    LogTextHelper.Info("\r\n\r\n第一步：用户名是(weixin)：" + messageHandler.RequestMessage.FromUserName);
                    //保存这次来访的accekn
                    FileName = System.Web.HttpContext.Current.Server.MapPath("~/");
                    dac = new DataAccess.Class1(FileName);
                    dac.set_accken(messageHandler.RequestMessage.FromUserName.Trim());

                    ////测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
                    //messageHandler.RequestDocument.Save(
                    //    Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Request_" +
                    //                   messageHandler.RequestMessage.FromUserName + ".txt"));
                    //执行微信处理过程
                    messageHandler.Execute();
                    //测试时可开启，帮助跟踪数据
                    //messageHandler.ResponseDocument.Save(
                    //    Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Response_" +
                    //                   messageHandler.ResponseMessage.ToUserName + ".txt"));
                    //WriteContent(messageHandler.ResponseDocument.ToString());
                    return;
                }
                catch (Exception ex)
                {
                    using (TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/Error_" + DateTime.Now.Ticks + ".txt")))
                    {
                        tw.WriteLine(ex.Message);
                        tw.WriteLine(ex.InnerException.Message);
                        if (messageHandler.ResponseDocument != null)
                        {
                            tw.WriteLine(messageHandler.ResponseDocument.ToString());
                        }
                        tw.Flush();
                        tw.Close();
                    }
                }
                finally
                {
                    Response.End();
                }
            }
        }

        private void WriteContent(string str)
        {
            Response.Output.Write(str);
        }

        /// <summary>
        /// 最简单的Page_Load写法（本方法仅用于演示过程，针对未加密消息，未实际在DEMO演示中使用到）
        /// </summary>
        private void MiniProcess()
        {
            string signature = Request["signature"];
            string timestamp = Request["timestamp"];
            string nonce = Request["nonce"];
            string echostr = Request["echostr"];

            if (Request.HttpMethod == "GET")
            {
                //get method - 仅在微信后台填写URL验证时触发
                if (CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    WriteContent(echostr); //返回随机字符串则表示验证通过
                }
                else
                {
                    WriteContent("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, Token));
                }

            }
            else
            {
                //post method - 当有用户想公众账号发送消息时触发
                if (!CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    WriteContent("参数错误！");
                }

                //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
                var messageHandler = new CustomMessageHandler(Request.InputStream, null);
                //执行微信处理过程
                messageHandler.Execute();
                //输出结果
                WriteContent(messageHandler.ResponseDocument.ToString());
            }
            Response.End();
        }
    }
}