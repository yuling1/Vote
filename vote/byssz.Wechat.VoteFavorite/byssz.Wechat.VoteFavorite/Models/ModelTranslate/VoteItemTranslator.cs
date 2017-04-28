using byssz.Wechat.database;
using byssz.Wechat.VoteFavorite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace byssz.Wechat.VoteFavorite.Models.ModelTranslate
{
    public static class VoteItemTranslator
    {
        public static VotePage TranslatorVoteItem(List<VoteItem> voteItem)
        {
            SQLRepositry sqlre = new SQLRepositry();
            VotePage candidatelist = new VotePage();
            List<Candidate> list = new List<Candidate>();
            foreach (var item in voteItem)
            {
                var pictureByIndex = sqlre.GetPictureByIndex(item.Candidate.Index);
                Candidate candidate = new Candidate()
                {
                    Discription = item.Candidate.Discription,
                    EmployeeId = item.Candidate.EmployeeId,
                    IsMale = item.Candidate.IsMale,
                    IsDeleted = item.Candidate.IsDeleted,
                    Name = item.Candidate.Name,
                    MetaData = item.Candidate.MetaData,
                    Index = item.Candidate.Index,
                    Pictures = pictureByIndex.Select(x => new Pictures(x)).ToList(),
                    Department = item.Candidate.Department,
                    Cover = pictureByIndex.Select(x => new Pictures(x)).ToList().First(x => x.IsCover == true),
                    voteItem = sqlre.GetVotes(item.Candidate.Index)
                };
                list.Add(candidate);
            }
            candidatelist.Candidates = list;
            return candidatelist;
        }
    }
}