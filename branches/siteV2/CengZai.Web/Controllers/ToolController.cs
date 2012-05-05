using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Helper;
using System.Drawing.Imaging;
using System.Drawing;
using CengZai.Web.Common;

namespace CengZai.Web.Controllers
{
    public class ToolController : BaseController
    {
        //
        // GET: /Tool/

        //上传图片到临时目录
        [HttpPost]
        [CheckAuthFilter]
        public ActionResult AjaxUploadTempImage(string postname)
        {
            try
            {
                Model.User user = GetLoginUser();
                if (user == null)
                {
                    return AjaxReturn("error", "您尚未登录！");
                }
                if (string.IsNullOrEmpty(postname))
                {
                    postname = "file";
                    //return AjaxReturn("error", "非法操作！");
                }
                //文件名：UserID_日期.jpg，保存在temp目录下
                string fileName = string.Format("_temp/{0}_{1}{2}.jpg"
                    , user.UserID
                    , DateTime.Now.ToString("yyyyMMddmmss")
                    , new Random().Next(100, 999));
                string fileMapPath = Util.MapPath(Config.UploadMapPath + "/" + fileName);
                //文件大小不为0
                System.Drawing.Image thumbnail = null;
                System.Drawing.Image original = null;
                HttpPostedFileBase file = Request.Files[postname];
                if (file == null)
                {
                    return AjaxReturn("error", "请选择上传图片！");
                }
                original = System.Drawing.Image.FromStream(file.InputStream);
                if (original == null)
                {
                    return AjaxReturn("error", "请选择图片文件！");
                }

                thumbnail = ImageHelper.MakeThumbnail(original, Config.UploadImageMaxWidth, Config.UploadImageMaxHeight, ThubnailMode.Auto, ImageFormat.Jpeg);
                Util.EnsureFileDir(fileMapPath);    //确保文件存在
                thumbnail.Save(fileMapPath);
                original.Dispose();
                thumbnail.Dispose();

                return AjaxReturn("success", fileName);
            }
            catch (Exception ex)
            {
                Log.Error("Tool上传文件异常", ex);
                return AjaxReturn("error", "上传文件出错，出现错误！");
            }
        }

        ////裁剪或者直接保存文件
        //[HttpPost]
        //[CheckAuthFilter]
        //public ActionResult AjaxSaveImage(string filename, int? x, int? y, int? w, int? h)
        //{
        //    try
        //    {
        //        Model.User user = GetLoginUser();
        //        if (user == null)
        //        {
        //            return AjaxReturn("error", "您尚未登录！");
        //        }
        //        AjaxModel ajaxModel = CutAndSaveImageFromTemp(filename, x, y, w, h);
        //        if (ajaxModel == null)
        //        {
        //            return AjaxReturn("error", "保存文件出错！");
        //        }
        //        return AjaxReturn(ajaxModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("Tool裁剪文件出现异常", ex);
        //        return AjaxReturn("error", "裁剪出现错误！");
        //    }
        //}


        /// <summary>
        /// 裁剪并替换临时文件文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckAuthFilter]
        public ActionResult AjaxCutTempImage(string filename, int? x, int? y, int? w, int? h)
        {
            try
            {
                Model.User user = GetLoginUser();
                if (user == null)
                {
                    return AjaxReturn("error", "您尚未登录！");
                }
                //判断文件是否为空或者是否不在临时文件夹且属于用户上传的图片里面
                //文件名必须为以下格式：_temp/1_201204073214740.jpg，否则视为非法文件（防止串改其它用户或者目录文件）
                if (string.IsNullOrEmpty(filename) || filename.IndexOf("_temp/" + user.UserID + "_") != 0)
                {
                    return AjaxReturn("error", "非法操作！");
                }
                if (x == null || y == null || w == null || h == null || w<=0 || h<=0)
                {
                    return AjaxReturn("error", "请选取要裁剪图像的区域！");
                }
                string fileMapPath = Util.MapPath(Config.UploadMapPath + "/" + filename);
                if (!System.IO.File.Exists(fileMapPath))
                {
                    return AjaxReturn("error", "图片不存在！");
                }
                Image original = null;
                Image thumbnail = null;
                original = Image.FromFile(fileMapPath);
                if (original == null)
                {
                    return AjaxReturn("error", "图片不存在！");
                }
                thumbnail = ImageHelper.CutImage(original, (int)x, (int)y, (int)w, (int)h);
                original.Dispose(); //先关闭源图片，然后进行覆盖
                Util.EnsureFileDir(fileMapPath);    //确保文件存在
                thumbnail.Save(fileMapPath); //保存新文件
                thumbnail.Dispose();
                return AjaxReturn("success", filename);
            }
            catch (Exception ex)
            {
                Log.Error("Tool裁剪文件出现异常", ex);
                return AjaxReturn("error", "裁剪出现错误！");
            }
        }

    }
}
