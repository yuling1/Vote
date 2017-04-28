using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byssz.Wechat.database
{
    public class VoteRequest
    {
        [Key]
        public Guid RequestId
        {
            get;
            set;
        }

        public string VoteIndex
        {
            get;
            set;
        }

        public bool IsMale
        {
            get;
            set;
        }

        public string VoteOpenId
        {
            get;
            set;
        }

        public DateTime RequestTime
        {
            get;
            set;
        }

        public bool IsExecuted
        {
            get;
            set;
        }
    }
}
