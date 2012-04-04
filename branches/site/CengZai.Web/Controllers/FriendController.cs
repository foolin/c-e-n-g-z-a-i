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
    public class FriendController : BaseController
    {
        //
        // GET: /Friend/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="active">friend=朋友（互相关注）, follow=我关注,fans=关注我，black=黑名单</param>
        /// <returns></returns>
        [CheckAuthFilter]
        public ActionResult List(string active)
        {
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                BLL.Friend bllFriend = new BLL.Friend();
                int pageSize = 20;
                int totalCount = 0;
                int pageIndex = GetPageIndex("page");
                DataSet dsList = null;
                //处理
                if (active == "follow")
                {
                    dsList = bllFriend.GetFriendUserListByPage(loginUser.UserID, Model.FriendRelation.Follow
                        , "", pageSize, pageIndex, out totalCount);
                }
                else if (active == "fans")
                {
                    dsList = bllFriend.GetFriendUserListByPage(loginUser.UserID, Model.FriendRelation.Fans
                        , "", pageSize, pageIndex, out totalCount);
                }
                else if (active == "black")
                {
                    dsList = bllFriend.GetFriendUserListByPage(loginUser.UserID, Model.FriendRelation.Black
                        , "", pageSize, pageIndex, out totalCount);
                }
                else
                {
                    dsList = bllFriend.GetFriendUserListByPage(loginUser.UserID, Model.FriendRelation.Friend
                        , "", pageSize, pageIndex, out totalCount);
                }
                if (dsList != null && dsList.Tables.Count > 0)
                {
                    List<Model.User> userList = new BLL.User().DataTableToList(dsList.Tables[0]);
                    ViewBag.UserList = userList;
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("批量关注朋友页面出现异常！", ex);
                return JumpToAction("页面出现异常", "即将跳转到完善资料页面...", "Avatar", "Settings");
            }
            return View();
        }


        /// <summary>
        /// 查找朋友
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        public ActionResult Find(string keyword)
        {
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                bool isUsePager = false;
                BLL.User bllUser = new BLL.User();
                DataSet dsList = null;
                if (!string.IsNullOrEmpty(keyword))
                {
                    dsList = bllUser.GetListBySearch(100, keyword, "RegTime DESC");
                    if (dsList == null || dsList.Tables.Count == 0 || dsList.Tables[0].Rows.Count == 0)
                    {
                        return JumpToAction("找不到关键词“" + keyword + "”的用户", "对不起，找不到关键词为“" + keyword + "”的用户，您可以试试搜索昵称、用户名或邮箱进行查找。", "Find");
                    }
                }
                else
                {
                    isUsePager = true;
                    dsList = bllUser.GetListByPage("State>=0", "UserID ASC", mPageSize, mPageIndex, out mTotalCount);
                    SetPage();
                }
                ViewBag.UsePager = isUsePager;  //是否使用分页
                if (dsList != null && dsList.Tables.Count > 0)
                {
                    List<Model.User> userList = bllUser.DataTableToList(dsList.Tables[0]);
                    Model.User currUser = userList.Find(x => x.UserID == loginUser.UserID);
                    if (currUser != null)
                    {
                        userList.Remove(currUser);  //移除自己
                    }
                    ViewBag.UserList = userList;
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("批量关注朋友页面出现异常！", ex);
                return JumpToAction("页面出现异常", "即将跳转到完善资料页面...", "Avatar", "Settings");
            }
            return View();
        }

        /// <summary>
        /// 关注朋友处理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CheckAuthFilter]
        public ActionResult Find(int[] userid)
        {
            try
            {
                int feedCount = 0;
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (userid != null && userid.Length > 0)
                {
                    BLL.Friend bllFriend = new BLL.Friend();
                    foreach (int friendUserID in userid)
                    {
                        if (bllFriend.Add(loginUser.UserID, friendUserID))
                        {
                            feedCount++;
                        }
                    }
                }
                if (feedCount > 0)
                {
                    return JumpBackAndRefresh("关注成功", "恭喜您，关注成功，一共关注" + feedCount + "个好友，您以后可以随时关注更多或者取消！");
                }
                else
                {
                    return JumpBackAndRefresh("关注失败", "对不起，关注" + feedCount + "个用户！");
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("Find(int[] userid)关注用户提交失败！", ex);
                return JumpBackAndRefresh("关注失败", "对不起，关注出现异常");
            }
        }

        /// <summary>
        /// Ajax添加朋友
        /// </summary>
        /// <returns></returns>
        public ActionResult FriendAdd(int? friendUserID)
        {
            try
            {
                if(friendUserID == null)
                {
                    return AjaxReturn("-1", "对方ID为空");
                }
                Model.User user = GetLoginUser();
                if(user == null)
                {
                    return AjaxReturn("-1", "您尚未登录，请先登录或者注册！");
                }
                BLL.Friend bllFriend = new BLL.Friend();
                Model.Friend isExist = bllFriend.GetModel(user.UserID, (int)friendUserID);
                if (isExist != null)
                {
                    return AjaxReturn("0", "您已经关注，无需重复关注！");
                }
                if (bllFriend.Add(user.UserID, (int)friendUserID))
                {
                    return AjaxReturn(friendUserID.ToString(), "关注成功！");
                }
                else
                {
                    return AjaxReturn("0", "关注失败，请稍后重试！");
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("添加用户异常", ex);
                return AjaxReturn("-1", "出现异常！");
            }
        }


        /// <summary>
        /// Ajax添加朋友
        /// </summary>
        /// <returns></returns>
        public ActionResult FriendDel(int? friendUserID)
        {
            try
            {
                if (friendUserID == null)
                {
                    return AjaxReturn("-1", "对方ID为空");
                }
                Model.User user = GetLoginUser();
                if (user == null)
                {
                    return AjaxReturn("-1", "您尚未登录，请先登录或者注册！");
                }
                BLL.Friend bllFriend = new BLL.Friend();
                Model.Friend isExist = bllFriend.GetModel(user.UserID, (int)friendUserID);
                if (isExist == null)
                {
                    return AjaxReturn("0", "您尚未关注！");
                }
                if (bllFriend.Delete(isExist.ID))
                {
                    return AjaxReturn(friendUserID.ToString(), "取消关注成功！");
                }
                else
                {
                    return AjaxReturn("0", "取消关注失败，请稍后重试！");
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("取消关注异常", ex);
                return AjaxReturn("-1", "出现异常！");
            }
        }


        /// <summary>
        /// 关注朋友
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        public ActionResult FeedFriendsForFirstLogin()
        {
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                BLL.User bllUser = new BLL.User();
                DataSet dsList = bllUser.GetList(100, "State=1 and UserID<>" + loginUser.UserID, "Vip DESC, RegTime DESC");
                if (dsList != null && dsList.Tables.Count > 0)
                {
                    ViewBag.UserList = bllUser.DataTableToList(dsList.Tables[0]);
                }
                else
                {
                    return JumpToAction("暂无用户", "即将跳转到完善资料页面...", "Avatar", "Settings");
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("批量关注朋友页面出现异常！", ex);
                return JumpToAction("页面出现异常", "即将跳转到完善资料页面...", "Avatar", "Settings");
            }
            return View();
        }


        /// <summary>
        /// 关注朋友处理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CheckAuthFilter]
        public ActionResult FeedFriendsForFirstLogin(int[] userid)
        {
            try
            {
                int feedCount = 0;
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (userid != null && userid.Length > 0)
                {
                    BLL.Friend bllFriend = new BLL.Friend();
                    foreach (int friendUserID in userid)
                    {
                        if (bllFriend.Add(loginUser.UserID, friendUserID))
                        {
                            feedCount++;
                        }
                    }
                }
                if (feedCount > 0)
                {
                    return JumpToAction("关注成功", "恭喜您，关注成功，一共关注" + feedCount + "个好友，您以后可以随时关注更多或者取消！", "Avatar", "Settings", null, 5);
                }
                else
                {
                    return JumpToAction("关注失败", "对不起，关注" + feedCount + "个用户！", "Avatar", "Settings", null, 5);
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("FeedFriendsForFirstLogin(int[] userid)关注用户提交失败！", ex);
                return JumpToAction("关注失败", "对不起，关注出现异常", "FeedFriendsForFirstLogin", null, 5);
            }
        }



        /// <summary>
        /// 关注朋友处理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CheckAuthFilter]
        public ActionResult EditFriend(int[] userid, int? type)
        {
            try
            {
                if (type == null)
                {
                    return JumpToTips("操作失败", "出现异常，请正常操作");
                }
                int feedCount = 0;
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return JumpToAction("操作失败！", "您尚未登录或者登录超时", "Login", "Account");
                }
                if (userid != null && userid.Length > 0)
                {
                    BLL.Friend bllFriend = new BLL.Friend();
                    foreach (int friendUserID in userid)
                    {
                        if (bllFriend.Update(loginUser.UserID, friendUserID, (int)type))
                        {
                            feedCount++;
                        }
                    }
                }
                if (feedCount > 0)
                {
                    return JumpBackAndRefresh("操作成功", "一共对" + feedCount + "个好友进行操作！");
                }
                else
                {
                    return JumpBackAndRefresh("操作成功", "一共对" + feedCount + "个好友进行操作！");
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("EditFriend(int[] userid, int? type)关注用户提交失败！", ex);
                return JumpBackAndRefresh("操作失败", "对不起，操作出现异常");
            }
        }




        /// <summary>
        /// 邀请好友
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        public ActionResult Invite()
        {
            Model.User loginUser = GetLoginUser();
            if (loginUser == null)
            {
                return JumpToLogin();
            }
            ViewBag.InviteList = new BLL.InviteCode().GetModelList("UserID=" + loginUser.UserID);
            return View();
        }

        /// <summary>
        /// 邀请好友
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CheckAuthFilter]
        public ActionResult Invite(string email, int? sendmail)
        {
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return JumpToLogin();
                }
                ViewBag.InviteList = new BLL.InviteCode().GetModelList("UserID=" + loginUser.UserID);

                if (!Util.IsEmail(email))
                {
                    ModelState.AddModelError("error", "邮箱不合法，请输入正确邮箱！");
                    return View();
                }
                //检查用户是否已经注册
                if (new BLL.User().GetModel(email) != null)
                {
                    ModelState.AddModelError("error", "对不起，该邮箱已经注册！");
                    return View();
                }
                BLL.InviteCode bllInvite = new BLL.InviteCode();
                Model.InviteCode mInvite = bllInvite.GetModelByEmail(email);
                if (mInvite != null && !string.IsNullOrEmpty(mInvite.Invite))
                {
                    ModelState.AddModelError("error", "对不起，该邮箱已经被邀请，无需重复邀请！");
                    return View();
                }
                if (mInvite == null)
                {
                    mInvite = new Model.InviteCode();
                }
                string strInviteCode = Guid.NewGuid().ToString().Replace("-","").Substring(0, 16);
                mInvite.Email = email;
                mInvite.Invite = strInviteCode;
                mInvite.UserID = loginUser.UserID;
                bllInvite.Add(mInvite);
                try
                {
                    if (Config.InviteCredit < 0)
                    {
                        loginUser.Credit = loginUser.Credit + Config.InviteCredit;
                        new BLL.User().Update(loginUser);
                        UpdateLoginUserSession(loginUser);
                    }
                }
                catch { }
                string inviteUrl = Util.GetCurrDomainUrl() +  Url.Action("Register", "Account", new { invite = strInviteCode });
                if (sendmail == 1)
                {
                    try
                    {
                        string mailContent = string.Format(@"您的朋友{0}邀请您注册{1}，{2}，快快加入吧！点击下面连接即可注册：<a href='{3}' target='_blank'>{3}</a>"
                            , loginUser.Nickname, Config.SiteName, Config.SiteSlogan, inviteUrl);
                        Mail.Send(email, loginUser.Nickname + "邀请您注册" + Config.SiteName, mailContent);
                        return JumpToTips("邀请成功！", "恭喜您，生成邀请连接并发送通知邮件成功！您只要把网址发送您朋友即可，邀请连接地址为：<p>" + inviteUrl + "</p>");
                    }
                    catch
                    {
                        return JumpToTips("邮件发送不成功！", loginUser.Nickname + "，生成邀请连接成功！但发送通知邮件失败！您只要把网址发送您朋友即可，邀请码为：" + strInviteCode + "，邀请网址：<br />" + inviteUrl + "");
                    }
                }
                return JumpToTips("邀请成功！", "恭喜您，生成邀请连接成功！您只要把网址发送您朋友即可，邀请码为：" + strInviteCode + "，邀请网址：<br />" + inviteUrl + "");
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("发送邀请连接异常", ex);
                ModelState.AddModelError("error", "对不起，邀请连接出现异常，请稍后重试！");
                return View();
            }
        }


    }
}
