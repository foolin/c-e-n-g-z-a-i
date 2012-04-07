using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Common;
using CengZai.Helper;
using System.Data;

namespace CengZai.Web.Controllers
{
    public class InboxController : BaseController
    {
        //
        // GET: /Inbox/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0=全部，1=私信，2=系统</param>
        /// <returns></returns>
        [CheckAuthFilter]
        public ActionResult Index(string active)
        {
            try
            {
                Model.User user = GetLoginUser();
                if (user == null)
                {
                    return JumpToTips("您尚未登录", "您尚未登录");
                }
                List<Model.Inbox> list = null;
                
                BLL.Inbox bllInbox = new BLL.Inbox();
                string where = string.Format("ToUserID={0}", user.UserID);
                if (active == "privacy")
                {
                    where += " and IsSystem=0 ";
                }
                else if (active == "notice")
                {
                    where += " and IsSystem=1 ";
                }
                DataSet dsInboxList = bllInbox.GetList(0, where, "SendTime DESC");
                if (dsInboxList != null && dsInboxList.Tables.Count > 0)
                {
                    list = bllInbox.DataTableToList(dsInboxList.Tables[0]);
                }
                ViewBag.InboxList = list;
            }
            catch (Exception ex)
            {
                Log.Error("私信异常", ex);
            }
            return View();
        }

        [CheckAuthFilter]
        public ActionResult Send(int? toUserID)
        {
            
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return JumpToTips("操作错误", "您尚未登录！");
                }
                Model.User toUser = null;
                if (toUserID != null)
                {
                    toUser = new BLL.User().GetModel((int)toUserID);
                    
                }
                ViewBag.ToUser = toUser;
                if(toUser == null)
                {
                    //
                    int totalCount = 0;
                    DataSet dsList = new BLL.Friend().GetFriendUserListByPage(loginUser.UserID
                        , Model.FriendRelation.FollowOrFans
                        , ""
                        , 500, 1, out totalCount);
                    if (dsList != null && dsList.Tables.Count > 0)
                    {
                        ViewBag.FriendList = new BLL.User().DataTableToList(dsList.Tables[0]);
                    }
                }
                
                //if (toUser == null || toUser.State <= 0)
                //{
                //    return JumpToTips("操作错误！", "用户被锁定或者不存在！");
                //}
                //if (loginUser.UserID == toUserID)
                //{
                //    return JumpToTips("操作错误", "不能给自己发私信");
                //}
                
            }
            catch (Exception ex)
            {
                Log.Error("发送私信异常", ex);
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
                if (toUser == null || toUser.State < 0)
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
                Log.Error("发送私信异常", ex);
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
