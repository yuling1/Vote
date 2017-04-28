using hr.tr.wechatinterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hr.tr.wechatinterface.Messages.ReceiveMessage
{
    public class Message: ITemplate
    {
        public string FromUserName { get; set; }

        public string ToUserName { get; set; }

        public string MsgType { get; set; }

        public string CreateTime { get; set; }

        public virtual string Template
        {
            get;
            set;
        }

        public virtual string GenerateContent()
        {
            throw new NotImplementedException();
        }
    }
}