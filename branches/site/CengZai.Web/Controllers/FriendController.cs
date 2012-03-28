using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Common;
using CengZai.Helper;

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



    }
}
