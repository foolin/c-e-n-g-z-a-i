using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CengZai.Helper;


namespace System.Web.Mvc
{
    public static class HtmlExtendForUser
    {
        /// <summary>
        /// 取用户实体
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static CengZai.Model.User GetUser(this HtmlHelper helper, int? userID)
        {
            CengZai.Model.User user = null;
            if (userID == null)
            {
                return null;
            }
            try
            {
                user = new CengZai.BLL.User().GetModelByCache((int)userID);
            }
            catch (Exception ex)
            {
                Log.Error("HtmlExtendForUser.GetUser()异常：" + ex.Message);
            }
            return user;
        }


        /// <summary>
        /// 取用户实体
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static CengZai.Model.User GetUser(this HtmlHelper helper, string username)
        {
            CengZai.Model.User user = null;
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            try
            {
                user = new CengZai.BLL.User().GetModelByCache(username);
            }
            catch (Exception ex)
            {
                Log.Error("HtmlExtendForUser.GetUser()异常：" + ex.Message);
            }
            return user;
        }


        /// <summary>
        /// 取用户名
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetUserNickname(this HtmlHelper helper, int? userID)
        {
            string nickname = "";
            if (userID == null)
            {
                return null;
            }
            try
            {
                CengZai.Model.User user = new CengZai.BLL.User().GetModelByCache((int)userID);
                if (user != null)
                {
                    nickname = user.Nickname;
                }
            }
            catch (Exception ex)
            {
                Log.Error("HtmlExtendForUser.GetUserNickname()异常：" + ex.Message);
            }
            return nickname;
        }


        /// <summary>
        /// 取用户名
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetUserNickname(this HtmlHelper helper, string username)
        {
            string nickname = "";
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            try
            {
                CengZai.Model.User user = new CengZai.BLL.User().GetModelByCache(username);
                if (user != null)
                {
                    nickname = user.Nickname;
                }
            }
            catch (Exception ex)
            {
                Log.Error("HtmlExtendForUser.GetUserNickname()异常：" + ex.Message);
            }
            return nickname;
        }

        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin(this HtmlHelper helper)
        {
            CengZai.Model.User user = null;
            try
            {
                user = System.Web.HttpContext.Current.Session["LOGIN_USER"] as CengZai.Model.User;
            }
            catch { }
            return (user != null);
        }

        /// <summary>
        /// 取登录用户
        /// </summary>
        /// <returns></returns>
        public static CengZai.Model.User GetLoginUser(this HtmlHelper helper)
        {
            CengZai.Model.User user = null;
            try
            {
                user = System.Web.HttpContext.Current.Session["LOGIN_USER"] as CengZai.Model.User;
            }
            catch { }
            return user;
        }

        public static string GetUserSexName(this HtmlHelper helper, int? sex)
        {
            return new CengZai.BLL.User().GetSexName(sex);
        }
    }
}
