using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Common;
using CengZai.Helper;

namespace CengZai.Web.Controllers
{
    public class InboxController : BaseController
    {
        //
        // GET: /Inbox/
        [CheckAuthFilter]
        public ActionResult Index()
        {
            return View();
        }

        [CheckAuthFilter]
        public ActionResult Send(int? toUserID)
        {
            if (toUserID == null)
            {
                return JumpToTips("操作错误！", "请选择发送人");
            }
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return JumpToTips("操作错误", "您尚未登录！");
                }
                Model.User toUser = new BLL.User().GetModel((int)toUserID);
                if (toUser == null || toUser.State <= 0)
                {
                    return JumpToTips("操作错误！", "用户被锁定或者不存在！");
                }
                if (loginUser.UserID == toUserID)
                {
                    return JumpToTips("操作错误", "不能给自己发私信");
                }
                ViewBag.ToUser = toUser;
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("发送私信异常", ex);
            }
            return View();
        }

        [HttpPost]
        [CheckAuthFilter]
        public ActionResult Send(int? toUserID, string content)
        {
            if (toUserID == null)
            {
                return AjaxReturn("error", "请选择发送人");
            }
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return AjaxReturn("error", "您尚未登录！");
                }
                Model.User toUser = new BLL.User().GetModel((int)toUserID);
                if (toUser == null || toUser.State <= 0)
                {
                    return AjaxReturn("error", "用户被锁定或者不存在！");
                }
                if (loginUser.UserID == toUserID)
                {
                    return AjaxReturn("error", "不能给自己发私信");
                }
                if (string.IsNullOrEmpty(content) || content.Trim().Length <= 0 || content.Length > 300)
                {
                    return AjaxReturn("error", "内容长度只能是1到300个字符之间");
                }
                Model.Inbox inbox = new Model.Inbox();
                inbox.Content = content;
                inbox.FromUserID = loginUser.UserID;
                inbox.IsDelete = 0;
                inbox.IsRead = 0;
                inbox.IsSystem = 0;
                inbox.SendTime = DateTime.Now;
                inbox.Title = "";
                inbox.ToUserID = toUser.UserID;
                if (new BLL.Inbox().Add(inbox) > 0)
                {
                    return AjaxReturn("success", "发送成功！");
                }
                else
                {
                    return AjaxReturn("error", "发送失败，请稍后重试！");
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("发送私信异常", ex);
                return AjaxReturn("error", "发送出现异常");
            }
            
        }

        [CheckAuthFilter]
        public ActionResult Read(int msgid)
        {
            return View();
        }

    }
}
