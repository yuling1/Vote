using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byssz.Wechat.database
{
    public class UserInfo
    {
        [Key]
        public Guid UserId { get; set; }
        public string OpenId { get; set; }
    }
}
