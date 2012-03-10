using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Common;
using CengZai.Helper;

namespace CengZai.Web.Controllers
{
    public class LoverController : BaseController
    {
        //
        // GET: /Lover/

        public ActionResult Index(int? loverID)
        {
            return View();
        }

        /// <summary>
        /// 申请
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        public ActionResult Apply()
        {
            Model.User user = GetLoginUser();
            ViewBag.User = user;

            return View();
        }

        /// <summary>
        /// 申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CheckAuthFilter]
        public ActionResult Apply(string nickname, int? honeyuserid,  int? lovestate)
        {
            Model.User user = GetLoginUser();
            ViewBag.User = user;

            nickname = (nickname + "").Trim();
            if (string.IsNullOrEmpty(nickname))
            {
                return Json(new AjaxError("nickname", "用户名不能为空！"));
            }

            //文件大小不为0
            System.Drawing.Image avatar = null;
            try
            {
                HttpPostedFileBase file = Request.Files["avatar"];
                if (file == null)
                {
                    return Json(new AjaxError("avatar", "请选择上传文件！"));
                }
                avatar = System.Drawing.Image.FromStream(file.InputStream);
            }
            catch { }
            if (avatar == null)
            {
                return Json(new AjaxError("avatar", "请选择图片文件！"));
            }
            
            //保存成自己的文件全路径,newfile就是你上传后保存的文件,
            //服务器上的UpLoadFile文件夹必须有读写权限
　　
            avatar.Save(Server.MapPath(@"test.jpg"));
            //return Content("nickname=" + nickname);


            return Json(null);
        }


        /// <summary>
        /// 接收
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        [HttpPost]
        public ActionResult UploadImage()
        {
            //文件大小不为0
            System.Drawing.Image avatar = null;
            try
            {
                HttpPostedFileBase file = Request.Files["fileAvatar"];
                if (file == null)
                {
                    return Json(new AjaxError("avatar", "请选择上传文件！"));
                }
                avatar = System.Drawing.Image.FromStream(file.InputStream);
            }
            catch { }
            if (avatar == null)
            {
                return Json(new AjaxError("avatar", "请选择图片文件！"));
            }

            //保存成自己的文件全路径,newfile就是你上传后保存的文件,
            //服务器上的UpLoadFile文件夹必须有读写权限
            string fileName = string.Format("{0}{1}.jpg", DateTime.Now.ToString("yyyyMMddmmss"), new Random().Next(100,999));
            avatar.Save(Server.MapPath( Config.UploadMapPath + "/" + fileName));
            //return Content("nickname=" + nickname);
            return Content(fileName);
        }


        /// <summary>
        /// 接收
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        public ActionResult Accept()
        {
            return View();
        }


        /// <summary>
        /// 详细
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        public ActionResult Detail(int? loverid)
        {
            return View();
        }

    }


}
