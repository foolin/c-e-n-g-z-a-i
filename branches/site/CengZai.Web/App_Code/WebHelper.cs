using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace CengZai.Web
{
    public static class WebHelper
    {
        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin()
        {
            Model.User user = null;
            try
            {
                user = System.Web.HttpContext.Current.Session["LOGIN_USER"] as Model.User;
            }
            catch { }
            return (user != null);
        }


        /// <summary>
        /// 取邮箱域名
        /// </summary>
        /// <param name="email"></param>
        public static string GetEmailDomain(string email)
        {
            if(string.IsNullOrEmpty(email) || email.IndexOf('@') == -1)
            {
                return email;
            }

            string domain = email.Substring(email.IndexOf('@') + 1);
            return domain;
        }







    }
}