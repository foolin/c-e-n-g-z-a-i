using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Code;
using CengZai;
using System.Data;

namespace CengZai.Web.Controllers
{
    public class ArticleController : BaseController
    {
        //
        // GET: /Article/
        [AuthorizedFilter]
        public ActionResult Index(string domain)
        {
            BLL.Article bll = new BLL.Article();
            int pageSize = 20;
            int pageIndex = 1;
            string where = "";

            List<Model.Article> artList = bll.GetModelList("State=1");
            
            ViewBag.Domain = domain + "";
            return View(artList);
        }

        [AuthorizedFilter]
        public ActionResult Post()
        {
            int type = 0;
            int.TryParse(Request["type"], out type);
            //分类设置
            List<Model.Category> categories = new BLL.Category().GetModelList(string.Format("UserID={0}", GetLoginUser().UserID));
            if (categories == null)
            {
                ViewBag.CategoryList = new List<SelectListItem>(){
                    new SelectListItem(){ Text = "默认分类", Value = "0"}
                };
            }
            else
            {
                ViewBag.CategoryList = new SelectList(categories, "CategoryID", "CategoryName");
            }
            //隐私设置
            var privateList = new List<SelectListItem>(){
                new SelectListItem(){ Text = "所有人可见", Value="0", Selected=true},
                new SelectListItem(){ Text = "仅好友可见", Value="1"},
                new SelectListItem(){Text = "仅自己可见", Value="2"},
            };
            ViewBag.PrivateList = privateList;

            if (type == (int)Model.ArtType.Audio)
            {
                return View("PostAudio");
            }
            else if (type == (int)Model.ArtType.Image)
            {
                return View("PostImage");
            }
            else if (type == (int)Model.ArtType.Link)
            {
                return View("PostLink");
            }
            else if (type == (int)Model.ArtType.Video)
            {
                return View("PostVideo");
            }
            else if (type == (int)Model.ArtType.Text)
            {
                //文章
                return View("PostText");
            }
            else if (type == (int)Model.ArtType.Weibo)
            {
                return View("PostText");
            }
            return View("PostText");
        }


        [HttpPost]
        [AuthorizedFilter]
        public ActionResult Post(FormCollection form, bool? bIsTop)
        {
            try
            {
                int type = 0;
                int.TryParse(form["type"], out type);
                int categoryID = 0;
                int.TryParse(form["type"], out categoryID);
                int isTop = 0;
                if (form["isTop"] == "1")
                {
                    isTop = 1;
                }
                int.TryParse(form["isTop"], out isTop);
                int privat = 0;
                int.TryParse(form["private"], out privat);

                BLL.Article bll = new BLL.Article();
                Model.Article model = new Model.Article();
                model.CategoryID = categoryID;
                model.Content = form["content"];
                model.DownCount = 0;
                model.IsTop = isTop;
                model.PostIP = Helper.Util.GetIP();
                model.PostTime = DateTime.Now;
                model.Private = privat;
                model.ReportCount = 0;
                model.State = 1;
                model.Title = form["title"];
                model.TopCount = 0;
                model.Type = type;
                model.UserID = GetLoginUser().UserID;
                model.ViewCount = 0;


                if (type == (int)Model.ArtType.Audio)
                {
                    if (string.IsNullOrEmpty(form["source"]))
                    {
                        return AlertAndBack("请上传图片");
                    }
                }
                else if (type == (int)Model.ArtType.Image)
                {
                    return View("PostImage");
                }
                else if (type == (int)Model.ArtType.Link)
                {
                    return View("PostLink");
                }
                else if (type == (int)Model.ArtType.Video)
                {
                    return View("PostVideo");
                }
                else if (type == (int)Model.ArtType.Text)
                {
                    if (string.IsNullOrEmpty(model.Title) && string.IsNullOrEmpty(model.Content))
                    {
                        return AlertAndBack("标题或者内容至少一项不能为空！");
                    }
                    //文章
                }
                //else if (strType == Model.ArtType.Weibo.ToString().ToLower())
                //{
                //    return View("PostText");
                //}

                bll.Add(model);
            }
            catch (Exception ex)
            {
                Helper.Log.AddErrorInfo("Article/Post操作异常:" + ex.Message);
                return AlertAndBack("操作异常，请检查输入是否合法！");
            }

            return AlertAndBack("提交成功！");
        }

    }
}
