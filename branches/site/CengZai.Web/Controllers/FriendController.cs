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
                        return JumpToTips("暂无用户", "请输入用户的昵称、用户名或者邮箱进行查找...");
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
        public ActionResult Invite()
        {
            return View();
        }




    }
}
