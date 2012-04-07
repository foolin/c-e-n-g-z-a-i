using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Common;
using CengZai.Helper;
using System.Drawing.Imaging;

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

        //个人资料
        [HttpPost]
        [CheckAuthFilter]
        public ActionResult Profile(string nickname, int? sex, string mobile, DateTime? birth, string sign, string intro)
        {
            try
            {
                Model.User user = GetLoginUser();
                if (user == null)
                {
                    return AjaxReturn("error", "您尚未登录！");
                }
                nickname = (nickname + "").Trim();
                if (nickname.Length <= 2 && nickname.Length > 20)
                {
                    return AjaxReturn("nickname", "昵称不能少于2个字符，大于20个字符！");
                }
                if (sex == null || sex < 0 || sex > 2)
                {
                    return AjaxReturn("sex", "请选择性别！");
                }
                if (!string.IsNullOrEmpty(user.Mobile) || !string.IsNullOrEmpty(mobile))
                {
                    if (string.IsNullOrEmpty(mobile) || !System.Text.RegularExpressions.Regex.IsMatch(mobile, @"1[0-9]{10}"))
                    {
                        return AjaxReturn("mobile", "请正确填写您的手机号码");
                    }
                }
                if (user.Birth != null && birth == null)
                {
                    return AjaxReturn("birth", "请填写生日");
                }
                if (birth != null && birth > DateTime.Now.AddYears(-14))
                {
                    return AjaxReturn("birth", "请填写正确的生日，未满14岁周岁禁止使用本站！");
                }
                if (sign != null && sign.Length > 50)
                {
                    return AjaxReturn("sign", "签名不能超过50个字符！");
                }
                BLL.User bllUser = new BLL.User();
                user.Nickname = nickname;
                user.Sex = sex;
                user.Mobile = mobile;
                user.Birth = birth;
                user.Sign = sign;
                user.Intro = intro;
                bllUser.Update(user);
                UpdateLoginUserSession(user);   //更新Session
                return AjaxReturn("success", "更新成功！");
            }
            catch (Exception ex)
            {
                Log.Error("Settings/Profile更新异常", ex);
                return AjaxReturn("error", "更新失败！操作异常，请检查是否输入是否正确！");
            }
        }


        // 修改密码
        [CheckAuthFilter]
        public ActionResult Password()
        {
            return View();
        }


        //个人资料
        [HttpPost]
        [CheckAuthFilter]
        public ActionResult Password(string oldpassword, string password, string repassword)
        {
            try
            {
                Model.User user = GetLoginUser();
                if (user == null)
                {
                    return AjaxReturn("error", "您尚未登录！");
                }
                if (string.IsNullOrEmpty(oldpassword))
                {
                    return AjaxReturn("oldpassword", "请输入当前密码！");
                }
                if (string.IsNullOrEmpty(password) || password.Length < 6 || password.Length > 20)
                {
                    return AjaxReturn("password", "请输入6~20个字符的新密码！");
                }
                if (string.IsNullOrEmpty(repassword) || repassword.Length < 6 || repassword.Length > 20)
                {
                    return AjaxReturn("repassword", "请重复输入6~20个字符的新密码！");
                }
                if (password != repassword)
                {
                    return AjaxReturn("repassword", "两次密码不一致！");
                }
                string oldMd5Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(oldpassword, "MD5");
                if (user.Password != oldMd5Password)
                {
                    return AjaxReturn("oldpassword", "您输入当前密码错误！！");
                }
                string md5Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(oldpassword, "MD5");
                BLL.User bllUser = new BLL.User();
                user.Password = md5Password;
                bllUser.Update(user);
                UpdateLoginUserSession(user);   //更新Session
                return AjaxReturn("success", "修改密码成功！请牢记最新的密码！");
            }
            catch (Exception ex)
            {
                Log.Error("Settings/Password更新异常", ex);
                return AjaxReturn("error", "修改密码失败！操作异常，请检查是否输入是否正确！");
            }
        }


        //上传头像
        [CheckAuthFilter]
        public ActionResult Avatar()
        {
            return View();
        }


        ///// <summary>
        ///// 接收
        ///// </summary>
        ///// <returns></returns>
        //[CheckAuthFilter]
        //[HttpPost]
        //public ActionResult UploadAvatar()
        //{

        //    Model.User user = GetLoginUser();
        //    if (user == null)
        //    {
        //        return AjaxReturn("0", "您尚未登录！");
        //    }

        //    string fileName = string.Format("{0}{1}.jpg", DateTime.Now.ToString("yyyyMMddmmss"), new Random().Next(100, 999));
        //    //保存成自己的文件全路径,newfile就是你上传后保存的文件,
        //    //服务器上的UpLoadFile文件夹必须有读写权限


        //    //文件大小不为0
        //    System.Drawing.Image thumbnail = null;
        //    System.Drawing.Image avatar = null;
        //    try
        //    {
        //        HttpPostedFileBase file = Request.Files["fileAvatar"];
        //        if (file == null)
        //        {
        //            return AjaxReturn("0", "请选择上传文件！");
        //        }
        //        avatar = System.Drawing.Image.FromStream(file.InputStream);
        //        if (avatar == null)
        //        {
        //            return AjaxReturn("0", "请选择图片文件！");
        //        }

        //        thumbnail = ImageHelper.MakeThumbnail(avatar, Config.AvatarWidth, Config.AvatarHeight, ThubnailMode.Cut, ImageFormat.Jpeg);
        //        thumbnail.Save(Util.MapPath(Config.UploadMapPath + "/" + fileName));
        //        avatar.Dispose();
        //        thumbnail.Dispose();

        //        string oldAvatar = user.Avatar;
        //        user.Avatar = fileName;
        //        bool isSuccess = new BLL.User().Update(user);
        //        UpdateLoginUserSession(user);   //更新Session

        //        //删除旧文件
        //        if (isSuccess && !string.IsNullOrEmpty(oldAvatar))
        //        {
        //            try
        //            {
        //                string oldImage = Util.MapPath(Config.UploadMapPath + "/" + oldAvatar);
        //                if (System.IO.File.Exists(oldImage))
        //                {
        //                    System.IO.File.Delete(oldImage);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("删除文件出错：" + ex.Message);
        //            }
        //        }
        //        if (isSuccess)
        //        {
        //            return AjaxReturn("1", fileName);
        //        }
        //        else
        //        {
        //            return AjaxReturn("1", "上传图片失败！");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("SettingsController.UploadAvatar上传文件出错", ex);
        //        return AjaxReturn("0", "上传图片出错，请确定您上传的是图片！");
        //    }
        //}

        /// <summary>
        /// 接收
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        [HttpPost]
        public ActionResult AjaxSaveAvatar(string filename, int? x, int? y, int? w, int? h)
        {
            try
            {
                Model.User user = GetLoginUser();
                if (user == null)
                {
                    return AjaxReturn("0", "您尚未登录！");
                }
                x = x ?? 0;
                y = y ?? 0;
                w = w ?? Config.AvatarWidth;
                h = h ?? Config.AvatarHeight;
                AjaxModel ajaxModel = SaveImageFromTemp(filename, x, y, w, h);
                if (ajaxModel.id == "success")
                {
                    string oldAvatar = user.Avatar;
                    user.Avatar = ajaxModel.msg;
                    bool isSuccess = new BLL.User().Update(user);
                    UpdateLoginUserSession(user);   //更新Session
                    //删除旧文件
                    try
                    {
                        string oldImage = Util.MapPath(Config.UploadMapPath + "/" + oldAvatar);
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("删除文件出错：" + ex.Message);
                    }
                }
                return AjaxReturn(ajaxModel);
            }
            catch (Exception ex)
            {
                Log.Error("SettingsController.AjaxSaveAvatar上传文件出错", ex);
                return AjaxReturn("0", "上传图片出错，请确定您上传的是图片！");
            }
        }


        ///// <summary>
        ///// 接收
        ///// </summary>
        ///// <returns></returns>
        //[CheckAuthFilter]
        //[HttpPost]
        //public ActionResult SaveAvatar(string filename)
        //{

        //    Model.User user = GetLoginUser();
        //    if (user == null)
        //    {
        //        return AjaxReturn("0", "您尚未登录！");
        //    }

        //    string fileName = string.Format("{0}{1}.jpg", DateTime.Now.ToString("yyyyMMddmmss"), new Random().Next(100, 999));
        //    //保存成自己的文件全路径,newfile就是你上传后保存的文件,
        //    //服务器上的UpLoadFile文件夹必须有读写权限


        //    //文件大小不为0
        //    System.Drawing.Image thumbnail = null;
        //    System.Drawing.Image avatar = null;
        //    try
        //    {
        //        HttpPostedFileBase file = Request.Files["fileAvatar"];
        //        if (file == null)
        //        {
        //            return AjaxReturn("0", "请选择上传文件！");
        //        }
        //        avatar = System.Drawing.Image.FromStream(file.InputStream);
        //        if (avatar == null)
        //        {
        //            return AjaxReturn("0", "请选择图片文件！");
        //        }

        //        thumbnail = ImageHelper.MakeThumbnail(avatar, Config.AvatarWidth, Config.AvatarHeight, ThubnailMode.Cut, ImageFormat.Jpeg);
        //        thumbnail.Save(Util.MapPath(Config.UploadMapPath + "/" + fileName));
        //        avatar.Dispose();
        //        thumbnail.Dispose();

        //        string oldAvatar = user.Avatar;
        //        user.Avatar = fileName;
        //        bool isSuccess = new BLL.User().Update(user);
        //        UpdateLoginUserSession(user);   //更新Session

        //        //删除旧文件
        //        if (isSuccess && !string.IsNullOrEmpty(oldAvatar))
        //        {
        //            try
        //            {
        //                string oldImage = Util.MapPath(Config.UploadMapPath + "/" + oldAvatar);
        //                if (System.IO.File.Exists(oldImage))
        //                {
        //                    System.IO.File.Delete(oldImage);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("删除文件出错：" + ex.Message);
        //            }
        //        }
        //        if (isSuccess)
        //        {
        //            return AjaxReturn("1", fileName);
        //        }
        //        else
        //        {
        //            return AjaxReturn("1", "上传图片失败！");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("SettingsController.UploadAvatar上传文件出错", ex);
        //        return AjaxReturn("0", "上传图片出错，请确定您上传的是图片！");
        //    }
        //}

    }
}
