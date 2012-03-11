﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Common;
using CengZai.Helper;
using System.Data;

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
            if (user.Sex == 0)
            {
                return AlertAndBack("您的性别未知，请完善资料再来申请！");
            }
            BLL.Lover bllLover = new BLL.Lover();
            DataSet dsMyLovers = bllLover.GetList(string.Format("(BoyUserID={0} Or GirlUserID={0}) And State<>{1}", user.UserID, (int)Model.LoverState.Abolish));
            if (dsMyLovers != null && dsMyLovers.Tables.Count > 0 && dsMyLovers.Tables[0].Rows.Count > 0)
            {
                return AlertAndBack("您已经申请过了，爱情是神圣的，切勿当儿戏！");
            }
            return View();
        }

        /// <summary>
        /// 申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CheckAuthFilter]
        public ActionResult Apply(int? certificate, int? honeyUserID, string myName, string avatar, string mobile, DateTime? birth, DateTime? joinDate, string oath)
        {
            try
            {
                Model.User user = GetLoginUser();
                ViewBag.User = user;

                if (certificate == null)
                {
                    return AjaxReturn("certificate", "请选择证件类型");
                }
                if (honeyUserID == null || honeyUserID <= 0)
                {
                    return AjaxReturn("honeyUserID", "请选择对方的帐号");
                }
                if (honeyUserID == user.UserID)
                {
                    return AjaxReturn("honeyUserID", "亲，你没事吧？自己和自己搞？");
                }
                if (new BLL.User().GetModel(user.UserID) == null)
                {
                    return AjaxReturn("honeyUserID", "唉哟，对方帐号怎么不存在呢？");
                }
                myName = (myName + "").Trim();
                if (myName.Length == 0 || myName.Length > 20)
                {
                    return AjaxReturn("myName", "请输入您的名字");
                }
                if (string.IsNullOrEmpty(avatar) ||
                    !System.IO.File.Exists(Server.MapPath(Config.UploadMapPath + "/" + avatar))
                    )
                {
                    return AjaxReturn("avatar", "请上传你们的合照头像");
                }
                if (string.IsNullOrEmpty(mobile) || !System.Text.RegularExpressions.Regex.IsMatch(mobile, @"1[0-9]{10}"))
                {
                    return AjaxReturn("mobile", "请正确填写您的手机作为身份证号，我们会加密显示！");
                }
                if (birth == null)
                {
                    return AjaxReturn("birth", "请输入正确的生日");
                }
                if (joinDate == null)
                {
                    return AjaxReturn("joinDate", "请输入正确的登记日期，注意，不可更改噢！");
                }
                oath = Helper.Util.RemoveHtml(oath);
                if (string.IsNullOrEmpty(oath) || oath.Length < 10)
                {
                    return AjaxReturn("oath", "你的誓言也太短了吧？难道你就没话对对方的说？");
                }
                if (oath.Length > 1000)
                {
                    return AjaxReturn("oath", "誓言这么长啊？都超过1000个字符了！精简点吧，记得说重点哦...");
                }
               
                //更新申请者信息
                BLL.User bllUser = new BLL.User();
                user.Username = user.Nickname;
                user.Mobile = mobile;
                user.Birth = birth;
                bllUser.Update(user);
                //插入申请表
                Model.Lover lover = new Model.Lover();
                lover.ApplyTime = DateTime.Now;
                lover.ApplyUserID = user.UserID;
                lover.Avatar = avatar;
                lover.Certificate = certificate;
                lover.JoinDate = joinDate;
                lover.State = (int)Model.LoverState.Apply;
                if (user.Sex == 2)
                {
                    lover.BoyOath = "";
                    lover.BoyUserID = honeyUserID;  //对方ID
                    lover.GirlOath = oath;
                    lover.GirlUserID = user.UserID;
                }
                else
                {
                    lover.BoyOath = oath;
                    lover.BoyUserID = user.UserID;
                    lover.GirlOath = "";
                    lover.GirlUserID = honeyUserID;
                }
                int loverID = new BLL.Lover().Add(lover);
                if (loverID > 0)
                {
                    return AjaxReturn("success", "申请成功！");
                }
                else
                {
                    return AjaxReturn("success", "申请失败，请检查输入或者稍后重试！");
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("LoverController.Apply()出现异常", ex);
                return AjaxReturn("error", "操作异常，请检查输入或者稍后重试");
            }
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
            string fileName = string.Format("{0}{1}.jpg", DateTime.Now.ToString("yyyyMMddmmss"), new Random().Next(100, 999));
            //保存成自己的文件全路径,newfile就是你上传后保存的文件,
            //服务器上的UpLoadFile文件夹必须有读写权限

            //文件大小不为0
            System.Drawing.Image avatar = null;
            try
            {
                HttpPostedFileBase file = Request.Files["fileAvatar"];
                if (file == null)
                {
                    return AjaxReturn("1", "请选择上传文件！");
                }
                avatar = System.Drawing.Image.FromStream(file.InputStream);
                if (avatar == null)
                {
                    return AjaxReturn("2", "请选择图片文件！");
                }
                avatar.Save(Server.MapPath(Config.UploadMapPath + "/" + fileName));
                avatar.Dispose();
            }
            catch(Exception ex)
            {
                Log.AddErrorInfo("LoverController.UploadImage上传文件出错",  ex);
                return AjaxReturn("3", "上传图片出错，请确定您上传的是图片！");
            }
            
            
            //删除旧文件
            if (!string.IsNullOrEmpty(Request["avatar"]))
            {
                try
                {
                    string oldImage = Server.MapPath(Config.UploadMapPath + "/" + Request["avatar"]);
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }
                catch (Exception ex)
                {
                    Log.AddErrorInfo("删除文件出错：" + ex.Message);
                }
            }

            //return Content("nickname=" + nickname);
            return AjaxReturn("0", fileName);
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
        /// 接收
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        [HttpPost]
        public ActionResult Accept(int? loverID)
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
