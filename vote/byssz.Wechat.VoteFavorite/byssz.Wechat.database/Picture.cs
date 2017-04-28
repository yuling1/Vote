using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byssz.Wechat.database
{
    public class Picture
    {
        [Key]
        public Guid PicId { get; set; }

        public string Url
        {
            get;
            set;
        }
        public  CandidateInfo candidateInfo
        {
            get;
            set;
        }
        public int Height
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public bool IsCover
        {
            get;
            set;
        }
    }
}
