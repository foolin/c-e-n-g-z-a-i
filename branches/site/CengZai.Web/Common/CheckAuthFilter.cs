using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using CengZai.Helper;


namespace CengZai.Web.Common
{

    public class CheckAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判断权限
            CengZai.Model.User user = filterContext.HttpContext.Session["LOGIN_USER"] as CengZai.Model.User;
            if (user == null)
            {
                //Cookies处理
                HttpCookie cookie = filterContext.HttpContext.Request.Cookies["LOGIN_USER"];
                if (cookie != null)
                {
                    string cookieVal = "";
                    
                    try
                    {
                        cookieVal = DESEncrypt.Decrypt(cookie.Value, Config.SecrectKey);
                        if (!string.IsNullOrEmpty(cookieVal))
                        {
                            //验证处理
                            //1.首先得到明文Val：用户ID|时间戳|“用户ID|时间戳”的MD5校验码
                            //2.把明文经过DESEncrypt加密得到密文SecrectVal
                            //3.把密文SecrectVal写入Cookies
                            string[] vals = cookieVal.Split(new char[]{'|'}, StringSplitOptions.RemoveEmptyEntries);
                            if (vals.Length >= 3)
                            {
                                int userID = int.Parse(vals[0]);
                                DateTime dt = new DateTime(long.Parse(vals[1]));
                                string verify = FormsAuthentication.HashPasswordForStoringInConfigFile(userID + "|" + dt.Ticks, "MD5");
                                if (verify == vals[2] && (dt.AddDays(30) > DateTime.Now))
                                {
                                    //如果MD5校验码一致，且在30天
                                    user = new BLL.User().GetModel(userID);
                                    if (user != null && user.State >= 0)
                                    {
                                        bool isAllowLogin = true;
                                        switch (Config.LoginLimit)
                                        {
                                            //禁止所有用户登录
                                            case 2:
                                                isAllowLogin = false;
                                            break;

                                            //只有激活用户可以登录
                                            case 1:
                                                if (user.State <= 0)
                                                {
                                                    isAllowLogin = false;
                                                }
                                                break;

                                            //全部用户可以登录，包括锁定用户
                                            case -1:
                                                isAllowLogin = true;
                                                //全部用户可以登录
                                                break;

                                            //非锁定用户可以登录
                                            case 0:
                                            default:
                                                if (user.State == -1)
                                                {
                                                    isAllowLogin = true;
                                                }
                                                break;

                                        }
                                        if (isAllowLogin)
                                        {
                                            //重新存储Session
                                            filterContext.HttpContext.Session["LOGIN_USER"] = user;
                                        }
                                        else
                                        {
                                            //禁止登录
                                            filterContext.HttpContext.Response.Cookies["LOGIN_USER"].Expires = DateTime.Now.AddDays(-9999);
                                            user = null;    
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            if (user == null)
            {
                filterContext.Result = new RedirectToRouteResult("Default",
                    new RouteValueDictionary(new { controller = "User", action = "Login" }));
            }
        }
    }

}
