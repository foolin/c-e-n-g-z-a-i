using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CengZai.Web.Controllers
{
    public class AboutController : BaseController
    {
        //
        // GET: /About/

        public ActionResult Us()
        {
            return View();
        }


        public ActionResult Agreement()
        {
            return View();
        }



        public ActionResult Privacy()
        {
            return View();
        }

    }
}
