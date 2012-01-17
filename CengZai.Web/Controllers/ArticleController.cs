using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Code;

namespace CengZai.Web.Controllers
{
    public class ArticleController : BaseController
    {
        //
        // GET: /Article/
        [AuthorizedFilter]
        public ActionResult Index()
        {
            BLL.Article bll = new BLL.Article();
            //List<Model.Article> artList = bll.GetListByPage(
            return View();
        }

        [AuthorizedFilter]
        public ActionResult List()
        {
            return View();
        }

        [AuthorizedFilter]
        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        [AuthorizedFilter]
        public ActionResult Post()
        {
            return View();
        }

    }
}
