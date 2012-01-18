using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace CengZai.Web.Code
{
    public class AuthorizedFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判断权限
            Model.User user = filterContext.HttpContext.Session["LOGIN_USER"] as Model.User;
            if (user == null)
            {
                filterContext.Result = new RedirectToRouteResult("Default",
                    new RouteValueDictionary(new { controller = "User", action = "Login" }));
            }
        }
    }
}