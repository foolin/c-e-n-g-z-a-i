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
        public ActionResult Index()
        {
            BLL.Article bll = new BLL.Article();
            //List<Model.Article> artList = bll.GetListByPage(
            return View();
        }

        [AuthorizedFilter]
        public ActionResult List()
        {
            return View();
        }

        [AuthorizedFilter]
        public ActionResult Post()
        {
            string strType = (Request["type"] + "").Trim().ToLower();

            if (strType == Model.ArtType.Audio.ToString().ToLower())
            {
                return View("PostAudio");
            }
            else if (strType == Model.ArtType.Image.ToString().ToLower())
            {
                return View("PostImage");
            }
            else if (strType == Model.ArtType.Link.ToString().ToLower())
            {
                return View("PostLink");
            }
            else if (strType == Model.ArtType.Video.ToString().ToLower())
            {
                return View("PostVideo");
            }
            //else if (strType == Model.ArtType.Weibo.ToString().ToLower())
            //{
            //    return View("PostText");
            //}
            else if (strType == Model.ArtType.Text.ToString().ToLower())
            {
                //文章
                return View();
            }
            return View();
        }

        [HttpPost]
        [AuthorizedFilter]
        public ActionResult PostText(string title, string content, string categoryid)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                return AlertAndBack("标题或者内容至少一项不能为空！");
            }

            List<Model.Category> categories = new BLL.Category().GetModelList(string.Format("UserID={0}", loginUser.UserID));
            ViewData["Categories"] = new SelectList(categories, "CategoryID", "CategoryName");
            return View();
        }


        [HttpPost]
        [AuthorizedFilter]
        public ActionResult PostText(string title, string content, string source, string type)
        {
            type = (type + "").Trim().ToLower();
            if (string.IsNullOrEmpty(source))
            {
                if (type == Model.ArtType.Image.ToString().ToLower())
                {
                    return AlertAndBack("请上传图片");
                }
            }

            return View();
        }

    }
}
