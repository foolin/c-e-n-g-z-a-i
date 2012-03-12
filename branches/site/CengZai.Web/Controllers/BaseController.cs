using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Helper;

namespace CengZai.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            //初始化
            ViewData["SiteName"] = Config.SiteName;
            ViewData["SiteDomain"] = Config.SiteDomain;
            ViewData["SiteSlogan"] = Config.SiteSlogan;
        }

        /// <summary>
        /// 取当前登录用户权限
        /// </summary>
        /// <returns></returns>
        protected Model.User GetLoginUser()
        {
            return Session["LOGIN_USER"] as Model.User;
        }


        /// <summary>
        /// 弹出提示并跳转
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        protected ContentResult AlertAndGo(string msg, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return Content(string.Format("<script>alert('{0}');location.href='{1}';</script>", msg, url));
            }
            else
            {
                return Content(string.Format("<script>alert('{0}');history.back();</script>", msg, url));
            }
        }

        /// <summary>
        /// 弹出提示并返回
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected ContentResult AlertAndBack(string msg)
        {
            return Content(string.Format("<script>alert('{0}');history.back();</script>", msg));
        }


        /// <summary>
        /// 弹出提示并返回刷新
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected ContentResult AlertAndRefresh(string msg)
        {
            return Content(string.Format("<script>alert('{0}');location.href='{1}';</script>", msg, Request.UrlReferrer.PathAndQuery));
        }

        /// <summary>
        /// 跳转到其它页面
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        /// <param name="time">时间：单位秒</param>
        /// <returns></returns>
        protected ViewResult JumpTo(string title, string msg, string url, int time)
        {
            ViewBag.Title = title;
            ViewBag.Message = msg;
            ViewBag.Url = url;
            ViewBag.Time = 5;
            return View("JumpTo");
        }

        /// <summary>
        /// 取页码
        /// </summary>
        /// <param name="pageTag"></param>
        /// <returns></returns>
        protected int GetPageNum(string pageTag)
        {
            if (string.IsNullOrEmpty(pageTag))
            {
                pageTag = "page";
            }
            int pageIndex = 0;
            int.TryParse(Request[pageTag], out pageIndex);
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            return pageIndex;
        }

        /// <summary>
        /// Joson返回
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected JsonResult AjaxReturn(string id, string msg)
        {
            return Json(new _AjaxReturn(id, msg));
        }



        /// <summary>
        /// 错误信息实体
        /// </summary>
        protected class _AjaxReturn
        {
            public _AjaxReturn(string id, string msg)
            {
                this.id = id;
                this.msg = msg;
                
            }
            public _AjaxReturn()
            {
            }
            public string id;
            public string msg;
        }

    }
}
