using hr.tr.wechatinterface.Configurations;
using hr.tr.wechatinterface.Messages.SendMessage;
using hr.tr.wechatinterface.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hr.tr.wechatinterface.Managers
{
    /// <summary>
    /// 客服管理者
    /// </summary>
    public class CustomManager
    {
        /// <summary>
        /// 发送客服信息
        /// </summary>
        /// <param name="globalToken"></param>
        /// <param name="message">SendMessage Json</param>
        public void SendCustomMessage(string globalToken, SendMessage message)
        {
            WeChatHttpUtility.SendHttpRequest(string.Format(WeChatConfiguration.SendCustomMessageUrl, globalToken), message.GetJsonString());
        }
    }
}
