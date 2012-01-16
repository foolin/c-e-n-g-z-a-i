using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CengZai.Web.Controllers
{
    public class SharedController : Controller
    {
        //
        // GET: /Shared/
        public ActionResult Error()
        {
            ViewData["Error"] = "很抱歉，您发生了请求时的错误处理.";
            return View();
        }

        public ActionResult UserAuthorizedError()
        {
            ViewData["Error"] = "您没有通过权限认证,请登录再操作!.";
            return View("Error");
        }

    }
}
