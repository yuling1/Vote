using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hr.tr.wechatinterface.Entities.Menu
{
    public sealed class MenuView : MenuModelBase
    {
        private string  _type = "view";

        public string  type
        {
            get { return _type; }
        }

        public string url { get; set; }
    }
}