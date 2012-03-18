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
        public ActionResult Blog(string username, int? pageid)
        {
            BLL.User bllUser = new BLL.User();
            BLL.Lover bllLover = new BLL.Lover();
            Model.User blogUser = bllUser.GetModelByCache(username);
            if (blogUser == null || blogUser.State == -1)
            {
                return JumpToHome("对不起！", "您访问的博客不存在！");
            }
            Model.Lover blogLover = bllLover.GetAwardLover(blogUser.UserID);
            if (blogLover == null)
            {
                return View("BlogSingle");
            }
            Model.User loverUser = null;
            if (blogLover.BoyUserID == blogUser.UserID)
            {
                loverUser = bllUser.GetModelByCache((int)blogLover.GirlUserID);
            }
            else
            {
                loverUser = bllUser.GetModelByCache((int)blogLover.BoyUserID);
            }
            if (loverUser == null)
            {
                return View("BlogSingle");
            }
            ViewBag.BlogUser = blogUser;
            ViewBag.BlogLover = blogLover;
            ViewBag.LoverUser = loverUser;

            return View();
        }

        //用户资料
        public ActionResult Profile(string username)
        {
            return View();
        }

        //证件信息
        public ActionResult Certificate(string username, int? loverid)
        {
            return View();
        }

        //文章信息
        public ActionResult Article(string username, int? artid)
        {
            return View();
        }

    }
}
