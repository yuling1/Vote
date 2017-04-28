using hr.tr.wechatinterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace byssz.Wechat.VoteFavorite.Handlers
{
    public class VoteHandlerFactory
    {
        public static IHandler CreateHandler(string requestXml)
        {
            IHandler handler = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(requestXml);
            string msgType = doc.SelectSingleNode("//MsgType").InnerText;
            switch (msgType)
            {
                case "event":
                    handler = new VoteEventHandler(requestXml);
                    break;
                case "text":
                    handler = new VoteTextHandler(requestXml);
                    break;
                default:
                    throw new NotSupportedException("目前不支持该类型：" + msgType + "。");
            }

            return handler;
        }
    }
}