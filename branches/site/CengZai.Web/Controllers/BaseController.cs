﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Helper;
using CengZai.Web.Common;

namespace CengZai.Web.Controllers
{

    public class BaseController : Controller
    {
        protected int mPageIndex = 0;
        protected int mPageSize = 0;
        protected int mTotalCount = 0;

        public BaseController()
        {
            
        }

        /// <summary>
        /// 代码
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //初始化分页
            mPageSize = Config.PageSize;
            mPageIndex = GetPageIndex("page");   
            mTotalCount = 0;

            //初始化
            ViewData["SiteName"] = Config.SiteName;
            ViewData["SiteDomain"] = Config.SiteDomain;
            ViewData["SiteSlogan"] = Config.SiteSlogan;
            WebHelper.LoadLoginUserFromSessionOrCookies(filterContext.HttpContext);
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
        /// 跳转页面到Url
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        protected ViewResult JumpBackAndRefresh(string title, string msg)
        {
            if (Request.UrlReferrer != null)
            {
                return JumpTo(title, msg, Request.UrlReferrer.PathAndQuery, 5);
            }
            else
            {
                return JumpToTips(title, msg);
            }
        }

        /// <summary>
        /// 跳转页面到Url
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        protected ViewResult JumpTo(string msg, string url)
        {
            return JumpTo("正在跳转", msg, url, 5);
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

            if (url != null && url=="BACK")
            {
                if (Request.UrlReferrer != null)
                {
                    url = Request.UrlReferrer.PathAndQuery;
                }
            }
            else if (url == "REFRESH")
            {
                url = Request.Url.PathAndQuery;
            }
            ViewBag.Url = url;
            ViewBag.Time = 5;
            return View("JumpTo");
        }

        /// <summary>
        /// 跳转到其它页面
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        /// <param name="time">时间：单位秒</param>
        /// <returns></returns>
        protected ViewResult JumpToAction(string title, string msg, string actionName, string controller, object routerValues, int time)
        {
            ViewBag.Title = title;
            ViewBag.Message = msg;
            if (routerValues == null && string.IsNullOrEmpty(controller))
            {
                ViewBag.Url = Url.Action(actionName);
            }
            else if (routerValues == null)
            {
                ViewBag.Url = Url.Action(actionName, controller);
            }
            else if (string.IsNullOrEmpty(controller))
            {
                ViewBag.Url = Url.Action(actionName, routerValues);
            }
            else
            {
                ViewBag.Url = Url.Action(actionName, controller, routerValues);
            }
            
            ViewBag.Time = time;
            return View("JumpTo");
        }

        /// <summary>
        /// 跳转到Action
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="actionName"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        protected ViewResult JumpToAction(string title, string msg, string actionName, object routerValues)
        {
            return JumpToAction(title, msg, actionName, "", routerValues, 5);
        }

        /// <summary>
        /// 跳转到Action
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="actionName"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        protected ViewResult JumpToAction(string title, string msg, string actionName, string controller)
        {
            return JumpToAction(title, msg, actionName, controller, null, 5);
        }

        /// <summary>
        /// 跳转到Action
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="actionName"></param>
        /// <param name="routerValues"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        protected ViewResult JumpToAction(string title, string msg, string actionName, object routerValues, int time)
        {
            ViewBag.Title = title;
            ViewBag.Message = msg;
            ViewBag.Url = Url.Action(actionName, routerValues);
            ViewBag.Time = 5;
            return View("JumpTo");
        }

        /// <summary>
        /// 跳转到Action
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        protected ViewResult JumpToAction(string title, string msg, string actionName)
        {
            return JumpToAction(title, msg, actionName, null, 5);
        }

        /// <summary>
        /// 跳转到Action
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        protected ViewResult JumpToHome(string title, string msg)
        {
            return JumpToAction(title, msg, "Index", "Home", null, 5);
        }

        /// <summary>
        /// 跳转到Action
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        protected ViewResult JumpToLogin()
        {
            return JumpToAction("操作错误", "您尚未登录或者操作超时！", "Login", "Account", null, 5);
        }

        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        protected ViewResult JumpToTips(string title, string msg)
        {
            return JumpTo(title, msg, "", 0);
        }

        /// <summary>
        /// 取页码
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        protected int GetPageIndex(string page)
        {
            if (string.IsNullOrEmpty(page))
            {
                page = "page";
            }
            int pageIndex = 0;
            if (!string.IsNullOrEmpty(Request[page]))
            {
                int.TryParse(Request[page], out pageIndex);
            }
            else
            {
                if (RouteData != null && RouteData.Values != null && RouteData.Values.ContainsKey(page))
                {
                    int.TryParse(RouteData.Values[page].ToString(), out pageIndex);
                }
            }
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            return pageIndex;
        }

        /// <summary>
        /// 设置页面
        /// </summary>
        protected void SetPage()
        {
            ViewBag.PageIndex = mPageIndex;
            ViewBag.PageSize = mPageSize;
            ViewBag.TotalCount = mTotalCount;
        }

        /// <summary>
        /// Joson返回
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected JsonResult AjaxReturn(string id, string msg)
        {
            return Json(new _AjaxReturn(id, msg),JsonRequestBehavior.AllowGet);
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
