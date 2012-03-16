using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CengZai.Web.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/

        public ActionResult Index()
        {
            return View();
        }

        //单博客
        public ActionResult Lover(int? loverid)
        {
            return View();
        }

        //单身博客
        public ActionResult User(int userid)
        {
            return View();
        }

    }
}
