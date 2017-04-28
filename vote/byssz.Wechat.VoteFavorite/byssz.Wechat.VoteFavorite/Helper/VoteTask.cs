using byssz.Wechat.VoteFavorite.Controllers;
using byssz.Wechat.VoteFavorite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace byssz.Wechat.VoteFavorite.Helper
{
    public class VoteTask
    {
        public static bool cleanedToday = false;

        public void ExecuteVote()
        {
            while (!End())
            {
                SQLRepositry sqlre = new SQLRepositry();
                var voteRequest = sqlre.GetAllVoteRequest();
                if (voteRequest != null)
                {
                    if (!cleanedToday && DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                    {
                        VoteController.femaleIP.Clear();
                        VoteController.maleIP.Clear();
                        cleanedToday = true;
                    }
                    else if(DateTime.Now.Hour == 0 && DateTime.Now.Minute == 1)
                    {
                        cleanedToday = false;
                    }
                    foreach (var item in voteRequest)
                    {
                        sqlre.addVotes(item.VoteIndex);
                        sqlre.updateRequest(item.RequestId);
                    }
                    Thread.Sleep(3000);
                }
            }
        }

        public bool End()
        {
            return DateTime.Today > new DateTime(2016, 4, 1);
        }
    }
}