using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace byssz.Wechat.VoteFavorite.Models
{
    public class VoteItemModel
    {
        public int Votes
        {
            get;
            set;
        }

        public Candidate Candidate
        {
            get;
            set;
        }
    }
}