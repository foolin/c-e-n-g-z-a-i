using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Common;
using CengZai.Helper;

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
                if (birth == null)
                {
                    return AjaxReturn("birth", "请填写生日");
                }
                if (birth > DateTime.Now.AddYears(-14))
                {
                    return AjaxReturn("birth", "请填写正确的生日，未满14岁周岁禁止恋爱！");
                }
                BLL.User bllUser = new BLL.User();
                user.Nickname = nickname;
                user.Sex = sex;
                user.Mobile = mobile;
                user.Birth = birth;
                user.Sign = sign;
                user.Intro = intro;
                bllUser.Update(user);
                Session["LOGIN_USER"] = user;   //更新Session

                return AjaxReturn("success", "更新成功！");
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("Settings/Profile更新异常", ex);
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
                Session["LOGIN_USER"] = user;   //更新Session
                return AjaxReturn("success", "修改密码成功！请牢记最新的密码！");
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("Settings/Password更新异常", ex);
                return AjaxReturn("error", "修改密码失败！操作异常，请检查是否输入是否正确！");
            }
        }


        //上传头像
        [CheckAuthFilter]
        public ActionResult Avatar()
        {
            return View();
        }

    }
}
