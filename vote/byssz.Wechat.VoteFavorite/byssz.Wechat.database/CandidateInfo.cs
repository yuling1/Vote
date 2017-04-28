using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byssz.Wechat.database
{
    public class CandidateInfo
    {
        [Key]
        public Guid CandidateId { get; set; }
        //候选人信息描述
        public string Discription
        {
            get;
            set;
        }
        //候选人图片
        public virtual IList<Picture> Picture { get; set; }
        public string MetaData
        {
            get;
            set;
        }
        //候选人编号
        public string EmployeeId
        {
            get;
            set;
        }

        public string Index
        {
            get;
            set;
        }
        //候选人姓名
        public string Name
        {
            get;
            set;
        }
        //true: 男;false: 女
        public bool IsMale
        {
            get;
            set;
        }

        public bool IsDeleted
        {
            get;
            set;
        }
        public string Department { get; set; }
    }
}
