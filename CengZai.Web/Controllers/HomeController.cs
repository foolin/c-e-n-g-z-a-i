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

        [AuthorizedFilter]
        public ActionResult Index()
        {
            Model.User user = GetLoginUser();
            ViewBag.User = user;
            return View();
        }

    }
}
