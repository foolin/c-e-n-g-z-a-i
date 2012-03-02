﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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

            routes.MapLowerCaseUrlRoute(
                "Blog", // Route name
                "blog/{domain}/{controller}/{action}/{id}", // URL with parameters
                new { controller = "Article", action = "Index", domain = @"", id = UrlParameter.Optional }
                , 
                new{domain = @"[a-zA-Z0-9_-]{5,30}",}

            );

            //routes.MapLowerCaseUrlRoute(
            //    "Domain", // Route name
            //    "{domain}/{controller}/{action}/", // URL with parameters
            //    new { controller = "Article", action = "Index", domain = @"[a-zA-Z0-9_-]{5,30}", } // Parameter defaults
            //);

            routes.MapLowerCaseUrlRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }


        //void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    /* Fix for the Flash Player Cookie bug in Non-IE browsers.
        //     * Since Flash Player always sends the IE cookies even in FireFox
        //     * we have to bypass the cookies by sending the values as part of the POST or GET
        //     * and overwrite the cookies with the passed in values.
        //     * 
        //     * The theory is that at this point (BeginRequest) the cookies have not been read by
        //     * the Session and Authentication logic and if we update the cookies here we'll get our
        //     * Session and Authentication restored correctly
        //     */

        //    try
        //    {
        //        string session_param_name = "ASPSESSID";
        //        string session_cookie_name = "ASP.NET_SESSIONID";

        //        if (HttpContext.Current.Request.Form[session_param_name] != null)
        //        {
        //            UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
        //        }
        //        else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
        //        {
        //            UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Response.StatusCode = 500;
        //        Response.Write("Error Initializing Session");
        //    }

        //    try
        //    {
        //        string auth_param_name = "AUTHID";
        //        string auth_cookie_name = System.Web.Security.FormsAuthentication.FormsCookieName;

        //        if (HttpContext.Current.Request.Form[auth_param_name] != null)
        //        {
        //            UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
        //        }
        //        else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
        //        {
        //            UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        Response.StatusCode = 500;
        //        Response.Write("Error Initializing Forms Authentication");
        //    }
        //}
        //void UpdateCookie(string cookie_name, string cookie_value)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(cookie_name);
        //        HttpContext.Current.Request.Cookies.Add(cookie);
        //    }
        //    cookie.Value = cookie_value;
        //    HttpContext.Current.Request.Cookies.Set(cookie);
        //}


    }
}