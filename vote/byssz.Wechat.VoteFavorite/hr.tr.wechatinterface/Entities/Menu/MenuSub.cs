using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hr.tr.wechatinterface.Entities.Menu
{
    public sealed class MenuSub : MenuModelBase
    {
        public MenuModelBase[] sub_button { get; set; }
    }
}