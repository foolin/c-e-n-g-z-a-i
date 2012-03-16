using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Common;

namespace CengZai.Web.Controllers
{
    public class InboxController : BaseController
    {
        //
        // GET: /Inbox/
        [CheckAuthFilter]
        public ActionResult Index()
        {
            return View();
        }

        [CheckAuthFilter]
        public ActionResult Send()
        {

            return View();
        }

        [CheckAuthFilter]
        public ActionResult Read(int msgid)
        {
            return View();
        }

    }
}
