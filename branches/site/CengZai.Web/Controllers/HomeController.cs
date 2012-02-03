using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Code;

namespace CengZai.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        [CheckAuthFilter]
        public ActionResult Index()
        {
            Model.User user = GetLoginUser();
            ViewBag.User = user;
            return View();
        }

        public ActionResult JumpTo()
        {
            return JumpTo("注册成功", "恭喜您，注册成功！", "/User/Login", 3);
        }

    }
}
