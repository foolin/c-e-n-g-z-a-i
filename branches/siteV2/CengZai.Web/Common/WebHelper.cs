using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CengZai.Helper;

namespace CengZai.Web.Common
{
    public class WebHelper
    {
        /// <summary>
        /// 根据当前
        /// </summary>
        /// <param name="Current"></param>
        public static CengZai.Model.User LoadLoginUserFromSessionOrCookies(HttpContextBase Current)
        {
            if (Current == null) return null;
            CengZai.Model.User user = null;
            try
            {

                //判断权限
                user = Current.Session["LOGIN_USER"] as CengZai.Model.User;
                if (user == null)
                {
                    //Cookies处理
                    HttpCookie cookie = Current.Request.Cookies["LOGIN_USER"];
                    if (cookie != null)
                    {
                        string cookieVal = "";


                        cookieVal = DESEncrypt.Decrypt(cookie.Value, Config.SecrectKey);
                        if (!string.IsNullOrEmpty(cookieVal))
                        {
                            #region __验证处理__
                            //验证处理
                            //1.首先得到明文Val：用户ID|时间戳|“用户ID|时间戳”的MD5校验码
                            //2.把明文经过DESEncrypt加密得到密文SecrectVal
                            //3.把密文SecrectVal写入Cookies
                            string[] vals = cookieVal.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                            if (vals.Length >= 3)
                            {
                                int userID = int.Parse(vals[0]);
                                DateTime dt = new DateTime(long.Parse(vals[1]));
                                string verify = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(userID + "|" + dt.Ticks, "MD5");
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
                                            Current.Session["LOGIN_USER"] = user;
                                            /********** 更新用户登录信息 *********/
                                            //积分
                                            if (user.LoginTime != null)
                                            {
                                                DateTime lastLoginDate = Convert.ToDateTime(user.LoginTime);
                                                DateTime nowDate = DateTime.Now;
                                                //两个小时登录一次，积分+1
                                                if (lastLoginDate.AddHours(2) < nowDate)
                                                {
                                                    if (user.Credit == null)
                                                    {
                                                        user.Credit = 1;
                                                    }
                                                    else
                                                    {
                                                        user.Credit = user.Credit + 1;
                                                    }
                                                }
                                            }
                                            user.LoginTime = DateTime.Now;
                                            user.LoginIp = Helper.Util.GetIP();
                                            if (user.LoginCount != null)
                                            {
                                                user.LoginCount += 1;
                                            }
                                            else
                                            {
                                                user.LoginCount = 1;
                                            }
                                            try
                                            {
                                                new BLL.User().Update(user);
                                            }
                                            catch (Exception ex)
                                            {
                                                Log.Error("WebHelper.LoadLoginUserFromSessionOrCookies()更新用户积分信息异常！", ex);
                                            }
                                        }
                                        else
                                        {
                                            //禁止登录
                                            user = null;
                                            cookie.Expires = DateTime.Now.AddDays(-9999);
                                            if (string.IsNullOrEmpty(cookie.Domain))
                                            {
                                                cookie.Domain = Util.GetRootDomain(Config.SiteDomain);
                                            }
                                            HttpContext.Current.Response.SetCookie(cookie);
                                        }
                                    }
                                }
                            }
                            #endregion __验证处理__
                        }

                    }
                }
            }
            catch(Exception ex) 
            {
                Log.Error("WebHelper.LoadLoginUserFromSessionOrCookies(HttpContext Current)异常", ex);
            }

            return user;
        }
    }
}