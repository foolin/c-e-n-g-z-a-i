using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Helper;
using CengZai.Web.Common;
using System.Drawing;
using System.Drawing.Imaging;

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
        /// 更新登录用户状态
        /// </summary>
        /// <param name="user"></param>
        protected void UpdateLoginUserSession(Model.User loginUser)
        {
            Session["LOGIN_USER"] = loginUser;
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
            return Json(new AjaxModel(id, msg),JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Joson返回
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected JsonResult AjaxReturn(AjaxModel ajaxModel)
        {
            return Json(ajaxModel, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 错误信息实体
        /// </summary>
        protected class AjaxModel
        {
            public AjaxModel(string id, string msg)
            {
                this.id = id;
                this.msg = msg;
                
            }
            public AjaxModel()
            {
            }
            public string id;
            public string msg;
        }


        /// <summary>
        /// 发送系统消息
        /// </summary>
        /// <param name="toUserID"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        protected bool SendSysNotice(int toUserID, string content)
        {
            try
            {
                Model.Inbox inbox = new Model.Inbox();
                inbox.Content = content;
                inbox.FromUserID = 0;
                inbox.IsDelete = 0;
                inbox.IsRead = 0;
                inbox.IsSystem = 1;
                inbox.SendTime = DateTime.Now;
                inbox.Title = "系统消息";
                inbox.ToUserID = toUserID;
                new BLL.Inbox().Add(inbox);
                return true;
            }
            catch(Exception ex)
            {
                Log.Error("发送系统消息异常", ex);
                return false;
            }
        }


        /// <summary>
        /// 直接保存
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        protected AjaxModel SaveImageFromTemp(string filename)
        {
            return CutAndSaveImageFromTemp(filename, 0, 0, 0, 0);
        }


        /// <summary>
        /// 裁剪并保存
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        protected AjaxModel CutAndSaveImageFromTemp(string filename, int? x, int? y, int? w, int? h)
        {
            try
            {
                Model.User user = GetLoginUser();
                if (user == null)
                {
                    return new AjaxModel("error", "您尚未登录！");
                }
                //判断文件是否为空或者是否不在临时文件夹且属于用户上传的图片里面
                //文件名必须为以下格式：_temp/1_201204073214740.jpg，否则视为非法文件（防止串改其它用户或者目录文件）
                if (string.IsNullOrEmpty(filename) || filename.IndexOf("_temp/" + user.UserID + "_") != 0)
                {
                    return new AjaxModel("error", "非法操作！");
                }
                if (x == null || y == null || w == null || h == null)
                {
                    return new AjaxModel("error", "请选取要裁剪图像的区域！");
                }
                string fileMapPath = Util.MapPath(Config.UploadMapPath + "/" + filename);
                if (!System.IO.File.Exists(fileMapPath))
                {
                    return new AjaxModel("error", "图片不存在！");
                }
                Image original = null;
                Image thumbnail = null;
                original = Image.FromFile(fileMapPath);
                if (original == null)
                {
                    return new AjaxModel("error", "图片不存在！");
                }
                //新文件名-把文件转移到用户自己的目录
                //旧文件名：_temp/1_201204073214740.jpg
                //新文件名：1/1_201204073214740.jpg
                string newFilename = filename.Replace("_temp/" + user.UserID + "_", user.UserID + "/" + user.UserID + "_");
                string newFileMapPath = Util.MapPath(Config.UploadMapPath + "/" + newFilename);
                if (w > 0 && h > 0)
                {
                    thumbnail = ImageHelper.CutImage(original, (int)x, (int)y, (int)w, (int)h);
                }
                else if (w > Config.UploadImageMaxWidth || h > Config.UploadImageMaxHeight)
                {
                    thumbnail = ImageHelper.MakeThumbnail(
                        original
                        , Config.UploadImageMaxWidth
                        , Config.UploadImageMaxHeight
                        , ThubnailMode.Auto
                        , ImageFormat.Jpeg);
                }
                else
                {
                    thumbnail = original.Clone() as Image;   //复制一个副本
                }
                Util.EnsureFileDir(newFileMapPath);    //确保文件存在
                thumbnail.Save(newFileMapPath); //保存新文件
                original.Dispose();
                thumbnail.Dispose();
                try
                {
                    System.IO.File.Delete(fileMapPath); //删除旧文件
                }
                catch { }
                return new AjaxModel("success", newFilename);
            }
            catch (Exception ex)
            {
                Log.Error("Tool裁剪文件出现异常", ex);
                return new AjaxModel("error", "裁剪出现错误！");
            }
        }

    }
}
