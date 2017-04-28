using byssz.Wechat.database;
using byssz.Wechat.VoteFavorite.Controllers;
using byssz.Wechat.VoteFavorite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace byssz.Wechat.VoteFavorite.Helper
{
    public static class VoteChecker
    {
        /// <summary>
        /// Check if the user can vote for someone
        /// </summary>
        /// <param name="openid"></param>
        /// <returns>4: can vote for male only, 3: can vote for female only, 2: cannote vote, 1: can vote for both</returns>
        public static int CheckUser(string openid, string ip)
        {
            SQLRepositry sqlre = new SQLRepositry();
            var user = sqlre.GetVoteRequestByOpenId(openid);
            if ((user.Count == 0) && !VoteController.maleIP.Contains(ip) && !VoteController.femaleIP.Contains(ip))
            {
                return 1;
            }

            else if ((user.FirstOrDefault(x => x.IsMale == false) == null) && (!VoteController.femaleIP.Contains(ip)))
            {
                return 3;
            }
            else if ((user.FirstOrDefault(x => x.IsMale == true) == null) && (!VoteController.maleIP.Contains(ip)))
            {
                return 4;
            }
            else
            {
                return 2;
            }
        }


        public static bool CheckCandidate(int index)
        {
            return true;
        }

        public static bool CheckIfVoteStarted()
        {
            return true;
        }
    }
}