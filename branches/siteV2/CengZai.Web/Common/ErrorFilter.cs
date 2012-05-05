using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CengZai.Web.Common
{
    public class ErrorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new RedirectToRouteResult("Default",
                    new RouteValueDictionary(new { controller = "Shared", action = "Error" }));
            }
        }
    }
}
