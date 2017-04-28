using byssz.Wechat.VoteFavorite.Handlers;
using hr.tr.wechatinterface.Interfaces;
using hr.tr.wechatinterface.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace byssz.Wechat.VoteFavorite.Controllers
{
    public class WeChatController : Controller
    {
        // GET: WeChat
        private static readonly string Token = "weixin";

        [HttpGet]
        public ActionResult Index(string signature, string timestamp, string nonce, string echoStr)
        {
            string[] array = { Token, timestamp, nonce };
            Array.Sort(array);

            string tempStr = string.Join("", array);
            tempStr = WeChatHelper.SHA1Encrypt(tempStr).ToLower();
            if (tempStr == signature)
            {
                return Content(echoStr);
            }
            else
            {
                return Content(string.Format("Failed - signature:{0}, tempStr: {1}", signature, tempStr));
            }
        }

        [HttpPost]
        public ActionResult Index()
        {
            string requestXml = WeChatHelper.ReadRequest(this.Request);

            IHandler handler = VoteHandlerFactory.CreateHandler(requestXml);

            return Content(handler.HandleRequest());
        }
    }
}