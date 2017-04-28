using hr.tr.wechatinterface.Interfaces;
using hr.tr.wechatinterface.Messages;
using hr.tr.wechatinterface.Messages.ReceiveMessage;
using hr.tr.wechatinterface.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hr.tr.wechatinterface.Handlers
{
    public class TextHandler: IHandler
    {
        /// <summary>
        /// 请求的xml
        /// </summary>
        private string RequestXml { get; set; }

        /// <summary>
        /// 以用户请求的xml初始化该处理
        /// </summary>
        /// <param name="requestXml"></param>
        public TextHandler(string requestXml)
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
          if (tm.Content.Contains("查询"))
            {
                tm.Content = "http://www.baidu.com";
            }
            else if (tm.Content.Contains("天气"))
            {
                tm.Content = "http://www.weather.com.cn/";
            }                
            else if (tm.Content.Contains("HR".ToLower()) || tm.Content.Contains("进入") || tm.Content.Contains("系统"))
            {
                
                tm.Content = "欢迎进入HR, 访问：http://hire.playtogather.net/Home/Index?OpenId=" +"0001";
            }
            else if (tm.Content.Contains("openid"))
            {
                tm.Content = tm.FromUserName;
            }     
            else
            {
                tm.Content = "欢迎进入HR, 你说的是：" + tm.Content;
            }
            string toOpenid = tm.ToUserName;
            tm.ToUserName = tm.FromUserName;
            tm.FromUserName = toOpenid;
            tm.CreateTime = WeChatHelper.WeChatNowTime();
            return tm.GenerateContent();
        }
    }
}
