using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace byssz.Wechat.VoteFavorite.Models
{
    public class Candidate
    {
        public string Discription
        {
            get;
            set;
        }

        public List<Pictures> Pictures
        {
            get;
            set;
        }

        public string MetaData
        {
            get;
            set;
        }

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

        public string Name
        {
            get;
            set;
        }

        public bool IsMale
        {
            get;
            set;
        }

        public Pictures Cover
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

        public int voteItem { get; set; }
    }
}