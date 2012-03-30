using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Common;
using System.Data;

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
            BLL.Article bllArt = new BLL.Article();
            DataSet dsArtList = bllArt.GetListByPage("Privacy=0 And State=1", "ArtID DESC", mPageSize, mPageIndex, out mTotalCount);
            List<Model.Article> artList = null;
            if (dsArtList != null && dsArtList.Tables.Count > 0)
            {
                artList = bllArt.DataTableToList(dsArtList.Tables[0]);
            }
            ViewBag.ArtList = artList;
            SetPage();
            return View();
        }

        [CheckAuthFilter]
        public ActionResult User(int? uid)
        {
            if (uid == null || uid <= 0)
            {
                return RedirectToAction("Index");
            }
            Model.User user = new BLL.User().GetModel((int)uid);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.User = user;

            int pageSize = 20;
            int totalCount = 0;
            BLL.Article bllArt = new BLL.Article();
            DataSet dsArtList = bllArt.GetListByPage("Privacy=0 And State=1", "ArtID DESC", pageSize, GetPageIndex("page"), out totalCount);
            List<Model.Article> artList = null;
            if (dsArtList != null && dsArtList.Tables.Count > 0)
            {
                artList = bllArt.DataTableToList(dsArtList.Tables[0]);
            }
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.ArtList = artList;
            
            return View();
        }

        public ActionResult JumpTo()
        {
            return JumpTo("注册成功", "恭喜您，注册成功！", "/User/Login", 3);
        }

    }
}
