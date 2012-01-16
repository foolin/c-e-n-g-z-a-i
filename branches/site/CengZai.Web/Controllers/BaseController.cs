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
        }

        /// <summary>
        /// 取当前登录用户权限
        /// </summary>
        /// <returns></returns>
        public Model.User GetLoginUser()
        {
            return Session["LOGIN_USER"] as Model.User;
        }


        /// <summary>
        /// 弹出提示并跳转
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public ContentResult AlertAndGo(string msg, string url)
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
        public ContentResult AlertAndBack(string msg)
        {
            return Content(string.Format("<script>alert('{0}');history.back();</script>", msg));
        }


        /// <summary>
        /// 弹出提示并返回刷新
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ContentResult AlertAndRefresh(string msg)
        {
            return Content(string.Format("<script>alert('{0}');location.href='{1}';</script>", msg, Request.UrlReferrer.PathAndQuery));
        }

    }
}
