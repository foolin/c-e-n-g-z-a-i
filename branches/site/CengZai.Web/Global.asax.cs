﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CengZai.Helper;

namespace CengZai.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           // /********** 二级域名路由 ***********/
           // routes.Add("DomainArticleRoute", new DomainRoute(
           //"{username}.mvc.com"     // Domain with parameters 
           // , "blog/{username}/article/{artid}/{id}" // URL with parameters
           // , new { controller = "Blog", action = "Article", username = "", page = 0, @id = UrlParameter.Optional } // Parameter defaults
           // , new { username = @"[\w]{5}", artid = "[0-9]+", page = "[0-9]+" }
           //));

           // routes.Add("DomainBlogRoute", new DomainRoute(
           //"{username}." + Config.SiteDomain     // Domain with parameters 
           // , "{action}/{page}/{id}" // URL with parameters
           // , new { controller = "Blog", action = "Blog", username = "", page = 0, @id = UrlParameter.Optional } // Parameter defaults
           // , new { username = @"[\w]{5}", page = "[0-9]+" }
           //));

           // routes.Add("DomainDefault", new DomainRoute(
            //"{username}." + Config.SiteDomain     // Domain with parameters 
           // , "{controller}/{action}/{page}/{id}" // URL with parameters
           // , new { controller = "Blog", action = "Blog", username = "", page = 0, @id = UrlParameter.Optional } // Parameter defaults
           // , new { username = @"[\w]{5}", page = "[0-9]+" }
           //));

           // /********** 二级域名路由 end ***********/

            routes.MapLowerCaseUrlRoute(
                "BlogArticle", // Route name
                "blog/{username}/article/{artid}/{id}", // URL with parameters
                new { controller = "Blog", action = "Article", username = "", @id = UrlParameter.Optional } // Parameter defaults
                , new { username = @"[\w]*", artid = "[0-9]+" }
            );

            //博客{BlogController.Blog}
            routes.MapLowerCaseUrlRoute(
                "Blog", // Route name
                "blog/{username}/{action}/{page}/{id}", // URL with parameters
                new { controller = "Blog", action = "Blog", username = "",  page=0, @id = UrlParameter.Optional } // Parameter defaults
                , new { username = @"[\w]*", page = "[0-9]+" }
            );


            routes.MapLowerCaseUrlRoute(
                "Default", // Route name
                "{controller}/{action}/{page}/{id}", // URL with parameters
                new { controller = "Account", action = "Index", page=1, id = UrlParameter.Optional } // Parameter defaults
                , new { page = "[0-9]+" }
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }


        void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                //如果为xxx.com则重定向为www.xxx.com
                string strUrl = Request.Url.ToString().Trim().ToLower();
                if (System.Text.RegularExpressions.Regex.IsMatch(strUrl, @"^http://([a-zA-Z_\-0-9]+\.[a-zA-Z_\-0-9]+)/"
                    , System.Text.RegularExpressions.RegexOptions.Compiled 
                    | System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    Response.RedirectPermanent(
                        System.Text.RegularExpressions.Regex.Replace(
                        strUrl
                        , @"^http://([a-zA-Z_\-0-9]+\.[a-zA-Z_\-0-9]+)/"
                        ,@"http://www.$1/"
                        ));
                }
            }
            catch { }



            /* Fix for the Flash Player Cookie bug in Non-IE browsers.
             * Since Flash Player always sends the IE cookies even in FireFox
             * we have to bypass the cookies by sending the values as part of the POST or GET
             * and overwrite the cookies with the passed in values.
             * 
             * The theory is that at this point (BeginRequest) the cookies have not been read by
             * the Session and Authentication logic and if we update the cookies here we'll get our
             * Session and Authentication restored correctly
             */

            try
            {
                string session_param_name = "ASPSESSID";
                string session_cookie_name = "ASP.NET_SESSIONID";

                if (HttpContext.Current.Request.Form[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
                }
            }
            catch (Exception ex)
            {
                //Response.StatusCode = 500;
                //Response.Write("Error Initializing Session" + ex.Message);
            }

            try
            {
                string auth_param_name = "AUTHID";
                string auth_cookie_name = System.Web.Security.FormsAuthentication.FormsCookieName;

                if (HttpContext.Current.Request.Form[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
                }

            }
            catch (Exception ex)
            {
                //Response.StatusCode = 500;
                //Response.Write("Error Initializing Forms Authentication" + ex.Message);
            }
        }
        void UpdateCookie(string cookie_name, string cookie_value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
            if (cookie == null)
            {
                cookie = new HttpCookie(cookie_name);
                HttpContext.Current.Request.Cookies.Add(cookie);
            }
            cookie.Value = cookie_value;
            HttpContext.Current.Request.Cookies.Set(cookie);
        }

    }
}