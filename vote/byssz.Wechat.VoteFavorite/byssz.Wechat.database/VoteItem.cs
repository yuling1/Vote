using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byssz.Wechat.database
{
  public   class VoteItem
    {
      public Guid ID { get; set; }
        public int Votes
        {
            get;
            set;
        }

        public virtual CandidateInfo Candidate
        {
            get;
            set;
        }
    }
}
