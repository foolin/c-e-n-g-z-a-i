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
            int relative = -2;  //-2=未关注,-1=黑名单，0=自己，1=已关注，2=朋友
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
                        if (friend.Type == -1)
                        {
                            relative = -1;   //黑名单
                        }
                        else
                        {
                            relative = 1;   //已关注
                        }
                    }
                }

            }
            if (relative == 1)
            {
                if (CengZai.Helper.Config.OpenBlogDomain == 1)
                {
                    //已关注
                    return helper.HttpActionLink("已关注"
                    , "FriendDel"
                    , "Friend"
                    , new { friendUserID = friendUserID, time = DateTime.Now.Ticks }
                    , new AjaxOptions { OnSuccess = "$FR.delSuccess", OnFailure = "$FR.delFail" }
                    , new { frienduserid = friendUserID, @class = (cssClass + " muted"), title = "点击取消关注" });
                }
                else
                {
                    //已关注
                    return helper.ActionLink(
                    "已关注"
                    , "FriendDel"
                    , "Friend"
                    , new { friendUserID = friendUserID, time = DateTime.Now.Ticks }
                    , new AjaxOptions { OnSuccess = "$FR.delSuccess", OnFailure = "$FR.delFail" }
                    , new { frienduserid = friendUserID, @class = (cssClass + " muted"), title = "点击取消关注" });
                }
            }
            if (relative == -1)
            {
                //黑名单
                return MvcHtmlString.Create("黑名单");
            }
            if (relative == 0)
            {
                return MvcHtmlString.Create("");
            }
            if (CengZai.Helper.Config.OpenBlogDomain == 1)
            {
                return helper.HttpActionLink("关注Ta"
                    , "FriendAdd"
                    , "Friend"
                    , new { friendUserID = friendUserID, time = DateTime.Now.Ticks }
                    , new AjaxOptions { OnSuccess = "$FR.addSuccess", OnFailure = "$FR.addFail" }
                    , new { frienduserid = friendUserID, @class = cssClass, title = "点击关注Ta" }
                    );
            }
            else
            {
                return helper.ActionLink("关注Ta"
                    , "FriendAdd"
                    , "Friend"
                    , new { friendUserID = friendUserID, time = DateTime.Now.Ticks }
                    , new AjaxOptions { OnSuccess = "$FR.addSuccess", OnFailure = "$FR.addFail" }
                    , new { frienduserid = friendUserID, @class = cssClass, title = "点击关注Ta" }
                    );
            }
        }


        /// <summary>
        /// 取绝对路径
        /// </summary>
        /// <param name="ajaxHelper"></param>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static MvcHtmlString HttpActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName)
        {
            return ajaxHelper.HttpActionLink(linkText, actionName, controllerName, null, null, null);
        }

        /// <summary>
        /// 取绝对路径
        /// </summary>
        /// <param name="ajaxHelper"></param>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <param name="ajaxOptions"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString HttpActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return ajaxHelper.ActionLink(linkText, actionName, controllerName, "http", CengZai.Helper.Config.SiteDomain, "", routeValues, ajaxOptions, htmlAttributes);
        }
    }
}
