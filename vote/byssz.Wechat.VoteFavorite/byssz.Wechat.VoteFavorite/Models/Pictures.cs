using byssz.Wechat.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace byssz.Wechat.VoteFavorite.Models
{
    public class Pictures
    {
        public Pictures()
        {   
        }

        public Pictures(Picture pic) {
            this.Height = pic.Height;
            this.IsCover = pic.IsCover;
            this.Url = pic.Url;
            this.Width = pic.Width;
        }
        public string Url
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