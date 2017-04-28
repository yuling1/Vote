using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hr.tr.wechatinterface.Interfaces
{
    public interface ITemplate
    {
        string Template { get; }

        string GenerateContent();
    }
}
