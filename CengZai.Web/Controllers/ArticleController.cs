using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CengZai.Web.Controllers
{
    public class ArticleController : BaseController
    {
        //
        // GET: /Article/

        public ActionResult Index()
        {
            BLL.Article bll = new BLL.Article();
            //List<Model.Article> artList = bll.GetListByPage(
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

    }
}
