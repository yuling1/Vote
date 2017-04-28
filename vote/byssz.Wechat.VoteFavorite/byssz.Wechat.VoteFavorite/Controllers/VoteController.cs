using byssz.Wechat.VoteFavorite.DAL;
using byssz.Wechat.VoteFavorite.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sz_training_crowdsourcing_wechatinterface;

namespace byssz.Wechat.VoteFavorite.Controllers
{
    public class VoteController : Controller
    {
        public static List<string> maleIP = new List<string>();
        public static List<string> femaleIP = new List<string>();

        public ActionResult UploadInformation(string candidateInfo)
        {
            return View();
        }


        public ActionResult Vote(VoteUser vote, string index)
        {
            
            if(index==""){
                index = null;
            }
            JsonResult json = new JsonResult();
            SQLRepositry sqlre = new SQLRepositry();
            string ip = sqlre.GetClientIPAddr();
            if (sqlre.TryAddVote(index, vote.openId,ip))
            {
                json.Data = new { result = true };
            }
            return json;
        }
    }
}
