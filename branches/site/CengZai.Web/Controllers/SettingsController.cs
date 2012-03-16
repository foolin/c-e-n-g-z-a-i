using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Common;

namespace CengZai.Web.Controllers
{
    public class SettingsController : BaseController
    {
        //
        // GET: /Settings/

        public ActionResult Index()
        {
            Model.User user = GetLoginUser();
            ViewBag.User = user;

            return View();
        }

        //个人资料
        [CheckAuthFilter]
        public ActionResult Profile()
        {
            return View();
        }


        // 修改密码
        [CheckAuthFilter]
        public ActionResult Password()
        {
            return View();
        }


        //上传头像
        [CheckAuthFilter]
        public ActionResult Avatar()
        {
            return View();
        }

    }
}
