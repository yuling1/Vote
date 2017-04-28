using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace byssz.Wechat.VoteFavorite.Common
{
    public class VoteConfig
    {
        public static string WeChatSite = ConfigurationManager.AppSettings["WeChatSite"];
    }
}