using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CengZai.Web.Controllers
{
    public class BlogController : BaseController
    {
        //
        // GET: /Blog/

        public ActionResult Index()
        {
            return View();
        }

        //用户博客
        public ActionResult Blog(int? userid, int? pageid)
        {
            return View();
        }

        //用户资料
        public ActionResult Profile(int? userid)
        {
            return View();
        }

        //证件信息
        public ActionResult Certificate(int? userid, int? loverid)
        {
            return View();
        }

        //文章信息
        public ActionResult Article(int? userid, int? artid)
        {
            return View();
        }

    }
}
