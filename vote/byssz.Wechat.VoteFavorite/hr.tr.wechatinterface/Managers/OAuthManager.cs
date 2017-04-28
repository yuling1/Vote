using hr.tr.wechatinterface.Configurations;
using hr.tr.wechatinterface.Entities.Token;
using hr.tr.wechatinterface.Entities.User;
using hr.tr.wechatinterface.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace com.yiqiwan.weixincore.Managers
{
    class OAuthGlobalTokenStore
    {
        internal DateTime Previous_Token_Time;
        internal OAuthGlobalToken Current;
    }

    /// <summary>
    /// 网页授权管理者
    /// </summary>
    public class OAuthManager
    {
        private static OAuthGlobalTokenStore OAuthGlobalTokenStore = new OAuthGlobalTokenStore();

        private const double timeOut = 7000;

        /// <summary>
        /// 获取弹出授权页面的Url
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public static string GetPopupOAuthRedirectUrl(string redirectUrl)
        {
            return string.Format(WeChatConfiguration.OAuthUserInfoUrl, redirectUrl);
        }

        /// <summary>
        /// 获取不弹出授权页面的Url
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public static string GetNotPopupOAuthRedirectUrl(string redirectUrl)
        {
            return string.Format(WeChatConfiguration.OAuthBaseInfoUrl, redirectUrl);
        }

        /// <summary>
        /// 获取Access Token获得,包括openId
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static OAuthToken GetAccessToken(string code)
        {
            string Str = WeChatHttpUtility.GetJson(string.Format(WeChatConfiguration.AccessTokenUrl, code));
            OAuthToken Oauth_Token_Model = JsonConvert.DeserializeObject<OAuthToken>(Str);
            return Oauth_Token_Model;
        }

        /// <summary>
        /// 获取全局Token
        /// </summary>
        /// <returns></returns>
        public static OAuthGlobalToken GetGlobalAccessToken()
        {
            TimeSpan timeSpan = DateTime.Now - OAuthGlobalTokenStore.Previous_Token_Time;
            if (timeSpan.TotalSeconds > timeOut || OAuthGlobalTokenStore.Previous_Token_Time == default(DateTime))
            {
                string Str = WeChatHttpUtility.GetJson(WeChatConfiguration.GlobalTokenUrl);
                OAuthGlobalToken Oauth_GlobalToken_Model = JsonConvert.DeserializeObject<OAuthGlobalToken>(Str);

                OAuthGlobalTokenStore.Previous_Token_Time = DateTime.Now;
                OAuthGlobalTokenStore.Current = Oauth_GlobalToken_Model;

                return Oauth_GlobalToken_Model;
            }
            else
            {
                OAuthGlobalToken Oauth_GlobalToken_Model = OAuthGlobalTokenStore.Current;

                return Oauth_GlobalToken_Model;
            }
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static OAuthUser GetUserInfo(string accessToken, string openId, ref string str)
        {
            str = WeChatHttpUtility.GetJson(string.Format(WeChatConfiguration.GetUserInfoUrl, accessToken, openId));
            OAuthUser OAuthUser_Model = JsonConvert.DeserializeObject<OAuthUser>(str);
            return OAuthUser_Model;
        }
    }
}
