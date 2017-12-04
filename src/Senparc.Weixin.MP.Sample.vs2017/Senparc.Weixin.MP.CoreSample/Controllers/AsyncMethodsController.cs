﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;
using Senparc.Weixin.Entities;
using Microsoft.Extensions.Options;
//using ZXing.Aztec.Internal;


#if NET45
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Senparc.Weixin.MP.MvcExtension;
#else
using Microsoft.AspNetCore.Mvc;
#endif

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    public class AsyncMethodsController : Controller
    {
        private string appId;
        private string appSecret;

        IOptions<SenparcWeixinSetting> _senparcWeixinSetting;

        public AsyncMethodsController(IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
#if NET45
        string appId = WebConfigurationManager.AppSettings["WeixinAppId"];
        string appSecret = WebConfigurationManager.AppSettings["WeixinAppSecret"];
#else
            _senparcWeixinSetting = senparcWeixinSetting;
            appId = _senparcWeixinSetting.Value.WeixinAppId;
            appSecret = _senparcWeixinSetting.Value.WeixinAppSecret;
#endif
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 使用异步Action测试异步临时二维码接口
        /// </summary>
        /// <returns></returns>
        public async Task<RedirectResult> QrCodeTest()
        {
            var ticks = DateTime.Now.Ticks.ToString();
            var sceneId = int.Parse(ticks.Substring(ticks.Length - 7, 7));

            var qrResult = await QrCodeApi.CreateAsync(appId, 100, sceneId, QrCode_ActionName.QR_SCENE, "QrTest");
            var qrCodeUrl = QrCodeApi.GetShowQrCodeUrl(qrResult.ticket);

            return Redirect(qrCodeUrl);
        }

        /// <summary>
        /// 使用异步Action测试异步模板消息接口
        /// </summary>
        /// <param name="checkcode"></param>
        /// <returns></returns>
        public async Task<ActionResult> TemplateMessageTest(string checkcode)
        {
            var openId = CustomMessageHandler.TemplateMessageCollection.ContainsKey(checkcode)
                ? CustomMessageHandler.TemplateMessageCollection[checkcode]
                : null;

            if (openId == null)
            {
                return Content("验证码已过期或不存在！请在“盛派网络小助手”公众号输入“tm”获取验证码。");
            }
            else
            {
                CustomMessageHandler.TemplateMessageCollection.Remove(checkcode);


                var templateId = "cCh2CTTJIbVZkcycDF08n96FP-oBwyMVrro8C2nfVo4";
                var testData = new //TestTemplateData()
                {
                    first = new TemplateDataItem("【异步模板消息测试】"),
                    keyword1 = new TemplateDataItem(openId),
                    keyword2 = new TemplateDataItem("网页测试"),
                    keyword3 = new TemplateDataItem(DateTime.Now.ToString("O")),
                    remark = new TemplateDataItem("更详细信息，请到Senparc.Weixin SDK官方网站（http://sdk.weixin.senparc.com）查看！")
                };

                var result = await TemplateApi.SendTemplateMessageAsync(appId, openId, templateId, null, testData);
                return Content("异步模板消息已经发送到【盛派网络小助手】公众号，请查看。此前的验证码已失效，如需继续测试，请重新获取验证码。");
            }
        }

        #region 异步死锁测试

        /// <summary>
        /// 此方法会引发死锁，需要重启服务
        /// </summary>
        /// <returns></returns>
        public ActionResult DeadLockTest()
        {
            var result =
                Senparc.Weixin.HttpUtility.RequestUtility.HttpGetAsync("https://sdk.weixin.senparc.com",
                    cookieContainer: null).Result;
            return Content(result);
        }

        /// <summary>
        /// 此方法可以避免死锁
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> NoDeadLockTest()
        {
            var result = await Senparc.Weixin.HttpUtility.RequestUtility.HttpGetAsync("https://sdk.weixin.senparc.com",
                cookieContainer: null);
            return Content(result);
        }


        private async Task<string> GetRemoteData()
        {
            string result = null;
            await Task.Run(() =>
            {
                Task.Delay(1000);
                result = "hi " + DateTime.Now.ToString();
            });
            return result;
        }

        /// <summary>
        /// 此方法会引发死锁，需要重启服务
        /// </summary>
        /// <returns></returns>
        public ActionResult DeadLockTest2()
        {
            var result = GetRemoteData().Result;
            return Content(result);
        }


        /// <summary>
        /// 此方法可以避免死锁
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> NoDeadLockTest2()
        {
            var result = await GetRemoteData();
            return Content(result);
        }



        /// <summary>
        /// 此方法加上.ConfigureAwait(false)可以避免死锁
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetRemoteData2()
        {
            string result = null;
            await Task.Run(() =>
            {
                Task.Delay(1000);
                result = "hi " + DateTime.Now.ToString();
            }).ConfigureAwait(false);
            return result;
        }


        /// <summary>
        /// 此方法可以避免死锁
        /// </summary>
        /// <returns></returns>
        public ActionResult NoDeadLockTest3()
        {
            var result = GetRemoteData2().Result;
            return Content(result);
        }


        #endregion
    }
}