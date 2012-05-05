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
            CengZai.Model.User user = WebHelper.LoadLoginUserFromSessionOrCookies(filterContext.HttpContext);
            if (user == null)
            {
                filterContext.Result = new RedirectToRouteResult("Default",
                    new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
        }
    }

}
