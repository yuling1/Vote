using byssz.Wechat.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sz_training_crowdsourcing_wechatinterface;

namespace byssz.Wechat.VoteFavorite.Binders
{
    public class UserinfoModelBinder : IModelBinder
    {
        private string sessionKey = "vote_userinfo";
        public object BindModel(ControllerContext ControllerContext, ModelBindingContext ModelBindingContext)
        {
            VoteUser user = null;
            if (ControllerContext.RequestContext.HttpContext.Session[sessionKey] == null)
            {
                user = new VoteUser();
                ControllerContext.RequestContext.HttpContext.Session[sessionKey] = user;
            }
            else
            {
                user = (VoteUser)ControllerContext.RequestContext.HttpContext.Session[sessionKey];
            }
            return user;
        }
    }
}