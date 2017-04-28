using byssz.Wechat.VoteFavorite.Common;
using hr.tr.wechatinterface.Interfaces;
using hr.tr.wechatinterface.Messages.ReceiveMessage;
using hr.tr.wechatinterface.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace byssz.Wechat.VoteFavorite.Handlers
{
    public class VoteTextHandler : IHandler
    {
     private string RequestXml { get; set; }

        /// <summary>
        /// 以用户请求的xml初始化该处理
        /// </summary>
        /// <param name="requestXml"></param>
     public VoteTextHandler(string requestXml)
        {
            this.RequestXml = requestXml;
        }

        /// <summary>
        /// 处理请求并返回信息给用户
        /// </summary>
        /// <returns></returns>
        public string HandleRequest()
        {
            string response = string.Empty;

            TextMessage tm = TextMessage.LoadFromXml(this.RequestXml);

            if (tm != null)
            {
                switch (tm.Content.ToLower())
                {
                    case "?":
                    case "？":
                        response = QuestionMarkHandler(tm);
                        break;
                    default:
                        response = DefaultHandler(tm);
                        break;
                }
            }

            return response;
        }

        /// <summary>
        /// 用户输入？或者?输出当前时间
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        public string QuestionMarkHandler(TextMessage tm)
        {
            string toOpenid = tm.ToUserName;
            tm.ToUserName = tm.FromUserName;
            tm.FromUserName = toOpenid;
            tm.Content = "欢迎进入HR,现在时间：" + DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
            tm.CreateTime = WeChatHelper.WeChatNowTime();
            return tm.GenerateContent();
        }

        /// <summary>
        /// 输出用户输入的信息
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        public string DefaultHandler(TextMessage tm)
        {
            
                if (tm.Content.Contains("男神女神"))
                {
                    tm.Content = "点击链接加入男神女神评选：" + VoteConfig.WeChatSite + tm.FromUserName;
                }
                
                else
                {
                    tm.Content = "点击链接加入男神女神评选：" + VoteConfig.WeChatSite + tm.FromUserName;
                }
            
            string toOpenid = tm.ToUserName;
            tm.ToUserName = tm.FromUserName;
            tm.FromUserName = toOpenid;
            tm.CreateTime = WeChatHelper.WeChatNowTime();
            return tm.GenerateContent();
        }
    }
}