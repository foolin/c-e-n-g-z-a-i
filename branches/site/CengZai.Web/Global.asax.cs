using System;
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

            //用户相关
            routes.MapRoute(
                "Home", // Route name
                "Home/{action}/", // URL with parameters
                new { controller = "Home", action = "Index" } // Parameter defaults
            );

            //路由相关
            routes.MapRoute(
                "Article", // Route name
                "Article/{action}/", // URL with parameters
                new { controller = "Article", action = "Index",} // Parameter defaults
            );

            routes.MapRoute(
                "Blog", // Route name
                "blog/{domain}/{controller}/{action}/{id}", // URL with parameters
                new { controller = "Article", action = "Index", domain = @"", id = UrlParameter.Optional }
                , 
                new{domain = @"[a-zA-Z0-9_-]{5,30}",}

            );

            //routes.MapRoute(
            //    "Domain", // Route name
            //    "{domain}/{controller}/{action}/", // URL with parameters
            //    new { controller = "Article", action = "Index", domain = @"[a-zA-Z0-9_-]{5,30}", } // Parameter defaults
            //);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "User", action = "Login", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}