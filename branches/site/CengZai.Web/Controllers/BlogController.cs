using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Helper;

namespace CengZai.Web.Controllers
{
    public class BlogController : BaseController
    {


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string username = filterContext.ActionParameters["username"] as string;

            #region __用户基本信息处理start__
            BLL.User bllUser = new BLL.User();
            BLL.Lover bllLover = new BLL.Lover();
            Model.User blogUser = bllUser.GetModelByCache(username);
            if (blogUser == null || blogUser.State == -1)
            {
                filterContext.Result = JumpToHome("对不起！", "您访问的博客不存在！");
                return;
            }
            ViewBag.BlogUser = blogUser;    //保存博客主
            Model.Lover blogLover = bllLover.GetAwardLover(blogUser.UserID);
            if (blogLover == null)
            {
                return;
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
                return;
            }
            CengZai.Model.User boy = null;
            CengZai.Model.User girl = null;
            if (blogUser.Sex == 2)
            {
                girl = blogUser;
                boy = loverUser;
            }
            else
            {
                girl = loverUser;
                boy = blogUser;
            }
            //保存情侣信息
            ViewBag.BlogLover = blogLover;
            ViewBag.LoverUser = loverUser;
            ViewBag.Boy = boy;
            ViewBag.Girl = girl;
            #endregion __用户基本信息处理end__
        }

        //用户博客
        public ActionResult Blog(string username, int? pageid)
        {
            Model.User blogUser = ViewBag.BlogUser;
            Model.User loverUser = ViewBag.LoverUser;
            Model.User boy = ViewBag.Boy;
            Model.User girl = ViewBag.Girl;
            ViewBag.TotalCount = 0;
            if (blogUser == null)
            {
                return JumpToHome("对不起！", "您访问的博客不存在！");
            }
            
            if (loverUser == null)
            {
                BLL.Article bllArt = new BLL.Article();
                int totalCount = 0;
                int page = GetPageIndex("page");
                List<Model.Article> blogArtList = bllArt.GetUserPublicListByPage(blogUser.UserID, "IsTop DESC,PostTime DESC", Config.PageSize, page, out totalCount);
                ViewBag.TotalCount = totalCount;
                ViewBag.BlogArtList = blogArtList;
                return View("BlogSingle");
            }
            else
            {
                BLL.Article bllArt = new BLL.Article();
                int boyTotalCount = 0;
                int girlTotalCount = 0;
                int page = GetPageIndex("page");
                List<Model.Article> boyArtList = bllArt.GetUserPublicListByPage(boy.UserID, "IsTop DESC,PostTime DESC", Config.PageSize, page, out boyTotalCount);
                List<Model.Article> girlArtList = bllArt.GetUserPublicListByPage(girl.UserID, "IsTop DESC,PostTime DESC", Config.PageSize, page, out girlTotalCount);
                ViewBag.TotalCount = boyTotalCount > girlTotalCount ? boyTotalCount : girlTotalCount;
                ViewBag.BoyArtList = boyArtList;
                ViewBag.GirlArtList = girlArtList;
                return View();
            }
        }

        //用户资料
        public ActionResult Profile(string username)
        {
            return View();
        }

        //证件信息
        public ActionResult Certificate(string username, int? loverid)
        {
            Model.Lover lover = ViewBag.BlogLover as Model.Lover;
            if (lover == null)
            {
                return JumpToHome("证书不存在！", "对不起，您访问的证书不存在！");
            }
            return View();
        }

        //文章信息
        public ActionResult Article(string username, int? artid)
        {
            if (artid == null)
            {
                return JumpToTips("对不起，网页不存在", "对不起，网页不存在！");
            }
            try
            {
                Model.Article art = new BLL.Article().GetModel((int)artid);
                if (art == null)
                {
                    return JumpToTips("对不起，网页不存在", "对不起，网页不存在！");
                }
                Model.User user = new BLL.User().GetModel((int)art.UserID);
                if (user.Username != username)
                {
                    //如果不是原作者的，则跳转
                    return RedirectToAction("Article", new { username = user.Username, artid = art.ArtID });
                }
                try
                {
                    art.ViewCount = (art.ViewCount == null ? 1 : (art.ViewCount + 1));
                    new BLL.Article().Update(art);
                }
                catch (Exception ex)
                {
                    Log.Error("BlogController.Article()更新阅读量异常", ex);
                }
                ViewBag.Article = art;
            }
            catch (Exception ex)
            {
                Log.Error("BlogController.Article()读取文章异常", ex);
                return JumpToTips("对不起，网页出现异常", "对不起，网页出现异常！");
            }
            return View();
        }

    }
}
