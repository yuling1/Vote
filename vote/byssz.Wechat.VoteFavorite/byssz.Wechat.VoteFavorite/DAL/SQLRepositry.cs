using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using byssz.Wechat.database;
using byssz.Wechat.VoteFavorite.Helper;
using byssz.Wechat.VoteFavorite.Controllers;

namespace byssz.Wechat.VoteFavorite.DAL
{
    public class SQLRepositry
    {
        public BysSZContext Context { get; private set; }
        public SQLRepositry()
        {
            this.Context = new BysSZContext();
        }

        public bool AddVoteUser(UserInfo user)
        {
            UserInfo newUser = new UserInfo()
            {
                OpenId = user.OpenId,
                UserId = Guid.NewGuid(),
            };
            this.Context.UserInfo.Add(newUser);
            this.Context.SaveChanges();
            return true;
        }
        public List<CandidateInfo> GetAllCandidatesInfo()
        {
            return this.Context.CandidateInfos.ToList();
        }

        public UserInfo GetUserByOpenid(string OpenId)
        {
            return this.Context.UserInfo.FirstOrDefault(u => u.OpenId == OpenId);
        }

        public CandidateInfo GetCandidateInfo(string index)
        {
            CandidateInfo candidate = this.Context.CandidateInfos.FirstOrDefault(c => c.Index == index);
            return candidate;
        }

        public bool UploadCandidateInfo(CandidateInfo candidate)
        {
            CandidateInfo newcandidate = new CandidateInfo()
            {
                Discription = candidate.Discription,
                EmployeeId = candidate.EmployeeId,
                Index = candidate.Index,
                IsDeleted = false,
                IsMale = candidate.IsMale,
                MetaData = candidate.MetaData,
                Name = candidate.Name,
                Department = candidate.Department,
                CandidateId = candidate.CandidateId

            };
            this.Context.CandidateInfos.Add(newcandidate);
            this.Context.SaveChanges();
            return true;
        }

        public bool UpdateCandidateInfo(CandidateInfo candidate)
        {
            var candidateInfo = this.Context.CandidateInfos.First(x => x.Index == candidate.Index);
            candidateInfo.IsDeleted = candidate.IsDeleted;
            candidateInfo.IsMale = candidate.IsMale;
            candidateInfo.MetaData = candidate.MetaData;
            candidateInfo.Name = candidate.Name;

            this.Context.SaveChanges();
            return true;
        }

        public bool DeleteCandidate(string index)
        {
            var candidateInfo = this.Context.CandidateInfos.FirstOrDefault(x => x.Index == index);
            this.Context.CandidateInfos.Remove(candidateInfo);
            this.Context.SaveChanges();
            return true;
        }

        public bool TryAddVote(string index, string openid, string ip)
        {
            var ismale = this.Context.CandidateInfos.First(x => x.Index == index).IsMale;
            VoteRequest newrequest = new VoteRequest()
            {
                VoteOpenId = openid,
                IsExecuted = false,
                IsMale = ismale,
                RequestId = Guid.NewGuid(),
                RequestTime = DateTime.Now,
                VoteIndex = index
            };
            return AddVoteRequest(newrequest, ip);
        }
        /// <summary>
        /// vote
        /// </summary>
        /// <param name="openid"></param>
        /// <returns>4: can vote for male only, 3: can vote for female only, 2: cannote vote, 1: can vote for both</returns>
        public bool AddVoteRequest(VoteRequest request, string ip)
        {
            if (VoteChecker.CheckUser(request.VoteOpenId,ip) == 1)
            {
                if (request.IsMale == true) { VoteController.maleIP.Add(ip); } else { VoteController.femaleIP.Add(ip); };
                this.Context.VoteRequest.Add(request);
                this.Context.SaveChanges();
                return true;
            }
            else if (VoteChecker.CheckUser(request.VoteOpenId,ip) == 2)
            {
                return false;

            }
            else if (VoteChecker.CheckUser(request.VoteOpenId,ip) == 3)
            {
                if (request.IsMale == true)
                {
                    return false;
                }
                else
                {
                    VoteController.femaleIP.Add(ip);
                    this.Context.VoteRequest.Add(request);
                    this.Context.SaveChanges();
                    return true;
                }

            }
            else if (VoteChecker.CheckUser(request.VoteOpenId,ip) == 4)
            {
                if (request.IsMale == false)
                {
                    return false;
                }
                else
                {
                    VoteController.maleIP.Add(ip);
                    this.Context.VoteRequest.Add(request);
                    this.Context.SaveChanges();
                    return true;
                }

            }
            this.Context.SaveChanges();
            return true;
        }

        public bool FinishVoteRequest(Guid requestId)
        {
            return true;
        }

        public int GetVotes(string index)
        {
            int votes = this.Context.VoteItem.FirstOrDefault(x => x.Candidate.Index == index) == null ? 0 : this.Context.VoteItem.FirstOrDefault(x => x.Candidate.Index == index).Votes;
            return votes;
        }

        public List<VoteItem> GetSuzhouTopN(string location)
        {
            List<VoteItem> allsuzhou = null;
            var suzhou = this.Context.VoteItem.Where(x => x.Candidate.Department.Contains(location)).OrderByDescending(x => x.Votes).ToList();
            List<VoteItem> male4shuzhou = suzhou.Where(x => x.Candidate.IsMale == true).ToList();
            if (male4shuzhou.Count > 10)
            {
                allsuzhou = male4shuzhou.GetRange(0, 10);
            }
            else
            {
                allsuzhou = male4shuzhou;
            };
            List<VoteItem> female4suzhou = suzhou.Where(x => x.Candidate.IsMale == false).ToList();
            if (female4suzhou.Count > 10)
            {
                allsuzhou.AddRange(female4suzhou.GetRange(0, 10).ToList());
            }
            else
            {
                allsuzhou.AddRange(female4suzhou);
            };
            return allsuzhou;
        }

        public void LockTable(string tableName, string lockType)
        { }

        public void UnlockTable(string tableName, string lockType)
        { }

        public List<VoteRequest> GetVoteRequestByOpenId(string openID)
        {
            var voteRequestOneDay = this.Context.VoteRequest.Where(x => x.VoteOpenId == openID && ((DateTime.Now.Day - x.RequestTime.Day) < 1)).ToList();
            return voteRequestOneDay;
        }
        public List<VoteRequest> GetAllVoteRequest()
        {
            var allVoteRequest = this.Context.VoteRequest.Where(x => x.IsExecuted == false).ToList();
            return allVoteRequest;
        }

        public bool addVotes(string index)
        {
            VoteItem voteItem = null;
            if (this.Context.VoteItem.Any(x => x.Candidate.Index == index))
            {
                voteItem = this.Context.VoteItem.First(x => x.Candidate.Index == index);
            }
            else
            {
                voteItem = new VoteItem()
                {
                    Votes = 0,
                    ID = Guid.NewGuid(),
                    Candidate = this.Context.CandidateInfos.First(x => x.Index == index)
                };
                this.Context.VoteItem.Add(voteItem);
            }
            voteItem.Votes++;
            this.Context.SaveChanges();
            return true;
        }
        public bool updateRequest(Guid requestId)
        {
            var request = this.Context.VoteRequest.First(x => x.RequestId == requestId);
            request.IsExecuted = true;
            this.Context.SaveChanges();
            return true;
        }
        public List<Picture> GetPictureByIndex(string index)
        {
            return this.Context.Pictures.Where(x => x.candidateInfo.Index == index).ToList();
        }

        public bool UploadImg(Picture picture)
        {
            Picture pic = new Picture()
            {
                IsCover = false,
                Url = picture.Url,
                PicId = Guid.NewGuid()
            };

            this.Context.Pictures.Add(pic);
            this.Context.SaveChanges();
            return true;

        }

        public bool HasImg(string picNameID)
        {
            return this.Context.Pictures.Any(x => x.Url.Contains(picNameID));
        }

        public bool CandidateExist(string candidateId)
        {
            return this.Context.CandidateInfos.Any(x => x.Index == candidateId);
        }

        public bool UploadImgs(CandidateInfo candidate, string picNameID)
        {
            var pic = this.Context.Pictures.Where(x => x.Url.Contains(picNameID)).ToList();
            foreach (var item in pic)
            {
                string last = item.Url.Substring(0, item.Url.LastIndexOf("."));
                last = last.Substring(last.Length - 1);
                item.candidateInfo = this.Context.CandidateInfos.Where(x => candidate.CandidateId == x.CandidateId).First();
                if (last == "1")
                {
                    item.IsCover = true;
                }
                this.Context.SaveChanges();
            }
            return true;
        }
        public string GetClientIPAddr()
        {
            string ipAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddr))
                ipAddr = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(ipAddr))
                ipAddr = HttpContext.Current.Request.UserHostAddress;

            return ipAddr;
        }
    }
}