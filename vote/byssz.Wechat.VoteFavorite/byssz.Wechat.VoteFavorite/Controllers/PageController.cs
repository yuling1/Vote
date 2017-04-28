using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using byssz.Wechat.VoteFavorite.Models;
using byssz.Wechat.VoteFavorite.DAL;
using System.IO;
using byssz.Wechat.database;
using sz_training_crowdsourcing_wechatinterface;
using byssz.Wechat.VoteFavorite.Models.ModelTranslate;

namespace byssz.Wechat.VoteFavorite.Controllers
{
    public class PageController : Controller
    {
        private static int picIndex = 0;
        public ActionResult Index(VoteUser user, string openId)
        {
            SQLRepositry sqlre = new SQLRepositry();
            var user1 = sqlre.GetUserByOpenid(openId);
            if (user1 == null)
            {
                UserInfo newuser = new UserInfo()
                {
                    OpenId = openId,
                };
                sqlre.AddVoteUser(newuser);
            }
            // 根据OpenId获取user的其他信息，赋给user
            user.openId = openId;
            VotePage candidatelist = new VotePage();
            List<Candidate> list = new List<Candidate>();
            var allCandidate = sqlre.GetAllCandidatesInfo();
            foreach (var item in allCandidate)
            {
                var pictureByIndex = sqlre.GetPictureByIndex(item.Index);
                Candidate candidate = new Candidate()
                {
                    Discription = item.Discription,
                    EmployeeId = item.EmployeeId,
                    IsMale = item.IsMale,
                    IsDeleted = item.IsDeleted,
                    Name = item.Name,
                    MetaData = item.MetaData,
                    Index = item.Index,
                    Pictures = pictureByIndex.Select(x => new Pictures(x)).ToList(),
                    Department = item.Department,
                    Cover = pictureByIndex.Select(x => new Pictures(x)).ToList().First(x => x.IsCover == true),
                    voteItem = sqlre.GetVotes(item.Index)
                };
                list.Add(candidate);
            }
            candidatelist.Candidates = list;
            return View(candidatelist);
        }


        public ActionResult CandidateDetail(string index)
        {
            SQLRepositry sqlre = new SQLRepositry();
            var candidateDeatil = sqlre.GetCandidateInfo(index);
            var pictureByIndex = sqlre.GetPictureByIndex(index);
            Candidate candidate = new Candidate()
            {
                Discription = candidateDeatil.Discription,
                EmployeeId = candidateDeatil.EmployeeId,
                IsMale = candidateDeatil.IsMale,
                IsDeleted = candidateDeatil.IsDeleted,
                Name = candidateDeatil.Name,
                MetaData = candidateDeatil.MetaData,
                Index = candidateDeatil.Index,
                Pictures = pictureByIndex.Select(x => new Pictures(x)).ToList(),
                Department = candidateDeatil.Department,
                Cover = pictureByIndex.Select(x => new Pictures(x)).ToList().First(x => x.IsCover == true),
                voteItem = sqlre.GetVotes(candidateDeatil.Index)
            };
            return View("CandidateDetail", candidate);
        }

        public ActionResult Upload()
        {
            picIndex = 0;
            return View();
        }


        public ActionResult UploadInfo(VoteUser user, string empID, string name, bool? ismale, string department, string discription, string id)
        {
            SQLRepositry sqlre = new SQLRepositry();

            if (sqlre.HasImg(id))
            {
                if (!sqlre.CandidateExist(user.openId))
                {
                    CandidateInfo candidateInfo = new CandidateInfo()
                     {
                         Department = department,
                         Discription = discription,
                         EmployeeId = empID,
                         IsMale = ismale == null ? true : (bool)ismale,
                         Name = name,
                         Index = user.openId,
                         CandidateId = Guid.NewGuid(),
                         //Picture = listPic,
                     };
                    sqlre.UploadCandidateInfo(candidateInfo);
                    sqlre.UploadImgs(candidateInfo, id);
                    return Json(new { data = "上传成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "请勿重复提交！" }, JsonRequestBehavior.AllowGet);
                }
        }
            else
            {
                return Json(new { data = "上传失败！请添加图片后重新提交" }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult UploadImage()
        {
            SQLRepositry sqlre = new SQLRepositry();
            bool isSavedSuccessfully = true;
            string fName = "";
            picIndex++;
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];

                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);
                        string newFileName = string.Format("{0}_{1}.", Request.Form.GetValues(0)[0], picIndex) + "png";
                        var path = string.Format("{0}\\{1}", pathString, newFileName);
                        file.SaveAs(path);
                        Picture pictures = new Picture()
                        {
                            Url = "/Images/WallImages/imagepath/" + newFileName,
                        };
                        sqlre.UploadImg(pictures);
                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }


        }


        public ActionResult VoteResult()
        {
            SQLRepositry sqlre = new SQLRepositry();
            Dictionary<string, VotePage> god = new Dictionary<string, VotePage>();
            VotePage candidatelist = new VotePage();
            var suzhoutop10 = sqlre.GetSuzhouTopN("苏州");
            candidatelist = VoteItemTranslator.TranslatorVoteItem(suzhoutop10);
            god.Add("suzhou", candidatelist);

            var beijingtop10 = sqlre.GetSuzhouTopN("北京");
            candidatelist = VoteItemTranslator.TranslatorVoteItem(beijingtop10);
            god.Add("beijing", candidatelist);

            var shanghaitop10 = sqlre.GetSuzhouTopN("上海");
            candidatelist = VoteItemTranslator.TranslatorVoteItem(shanghaitop10);
            god.Add("shanghai", candidatelist);

            var xiantop10 = sqlre.GetSuzhouTopN("西安");
            candidatelist = VoteItemTranslator.TranslatorVoteItem(xiantop10);
            god.Add("xian", candidatelist);

            return View("VoteResult", god);
        }
    }
}
