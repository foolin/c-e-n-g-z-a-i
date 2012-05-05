using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Helper;

namespace CengZai.Web.Controllers
{
    public class SquareController : BaseController
    {
        //
        // GET: /Square/



        /// <summary>
        /// 广场
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                BLL.Lover bllLover = new BLL.Lover();
                //取最新列表
                ViewBag.TopLoverList = bllLover.GetModelList(200, "State=1", "ApplyTime ASC,JoinDate ASC");
            }
            catch (Exception ex)
            {
                Log.Error("Square/Index异常", ex);
                return JumpTo("对不起，出错了", "哎呀，对不起！出错了，请刷新或者稍后访问！", "", 0);
            }
            return View();
        }

    }
}
