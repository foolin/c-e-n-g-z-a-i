using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.Mvc.Ajax;

namespace System.Web.Mvc
{
    public static class AjaxExtend
    {

        /// <summary>
        /// 取登录用户
        /// </summary>
        /// <returns></returns>
        public static CengZai.Model.User GetLoginUser(this AjaxHelper helper)
        {
            CengZai.Model.User user = null;
            try
            {
                user = System.Web.HttpContext.Current.Session["LOGIN_USER"] as CengZai.Model.User;
            }
            catch { }
            return user;
        }

        /// <summary>
        /// 关注连接，如果已关注，则不显示连接
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="feedUserID"></param>
        /// <returns></returns>
        public static MvcHtmlString GetFriendAddLink(this AjaxHelper helper, int? friendUserID, string cssClass)
        {
            int relative = -1;  //未关注
            CengZai.Model.User user = GetLoginUser(helper);
            if (user != null && friendUserID != null)
            {
                if (user.UserID == friendUserID)
                {
                    relative = 0; //自己
                }
                else
                {
                    CengZai.Model.Friend friend = new CengZai.BLL.Friend().GetModel(user.UserID, (int)friendUserID);
                    if (friend != null)
                    {
                        relative = 1;   //已关注
                    }
                }

            }
            if (relative == 1)
            {
                return MvcHtmlString.Create("已关注");
            }
            if (relative == 0)
            {
                return MvcHtmlString.Create("");
            }
            return AjaxExtensions.ActionLink(helper
                , "关注"
                , "FriendAdd"
                , "Friend"
                , new { friendUserID = friendUserID, time = DateTime.Now.Ticks }
                , new AjaxOptions { OnSuccess = "$FR.addSuccess", OnFailure = "$FR.addFail" }
                , new { frienduserid = friendUserID, @class=cssClass });
                //, new { frienduserid = friendUserID });
        }
    }
}
