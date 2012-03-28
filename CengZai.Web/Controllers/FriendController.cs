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
                    return RedirectToAction("Login", "User");
                }
                BLL.User bllUser = new BLL.User();
                DataSet dsList = bllUser.GetListBySearch(100, keyword, "RegTime DESC");
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
                    return RedirectToAction("Login", "User");
                }
                BLL.User bllUser = new BLL.User();
                DataSet dsList = bllUser.GetList(100, "State=1", "Vip DESC, RegTime DESC");
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
                    return RedirectToAction("Login", "User");
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


    }
}
