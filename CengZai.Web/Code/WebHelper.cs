﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CengZai.Web.Code
{
    public class WebHelper
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
    }
}