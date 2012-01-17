﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Code;
using CengZai;

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
        public ActionResult Post(string title, string content, string type)
        {
            if (Request.IsAjaxRequest())
            {
                return AlertAndBack("Ajax提交！");
            }
            else
            {
                return AlertAndBack("非Ajax提交！");
            }
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                return AlertAndBack("标题或者内容至少1一项不能为空！");
            }
            return View();
        }

    }
}
