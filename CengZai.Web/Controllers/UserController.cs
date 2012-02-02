using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using CengZai;
using System.Text;
using System.Web.Security;
using CengZai.Helper;

namespace CengZai.Web.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (GetLoginUser() != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                {
                    ModelState.AddModelError("Email", "请输入正确的邮箱！");
                    return View();
                }
                if (string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("Password", "密码不能为空！");
                    return View();
                }
                Model.User user = null;
                BLL.User bll = new BLL.User();
                user = bll.GetModel(email);
                if (user == null)
                {
                    ModelState.AddModelError("Error", "用户或者密码不正确！");
                    return View();
                }
                string md5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                if (user.Password.ToLower() != md5Password.ToLower())
                {
                    ModelState.AddModelError("Error", "用户或者密码不正确！");
                    return View();
                }

                if (user.State < 0)
                {
                    ModelState.AddModelError("Error", "您的帐号已经冻结，如有问题，请联系客服！");
                    return View();
                }

                //更新用户到数据库
                user.LoginIp = Helper.Util.GetIP();
                user.LoginTime = DateTime.Now;
                if (user.LoginCount != null)
                {
                    user.LoginCount += 1;
                }
                else
                {
                    user.LoginCount = 1;
                }
                bll.Update(user);

                //登记登录
                Session["LOGIN_USER"] = user;

                if (Request["ReturnUrl"] == null || Request["ReturnUrl"].Length == 0)
                    return Content("<script>location.href='/';</script>");
                else
                    return Content("<script>location.href='" + Request["ReturnUrl"].ToString() + "';</script>");
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("User/Login异常：" + ex.Message);
                ModelState.AddModelError("Error", "登录异常，请稍后重试！");
                return View();
            }
        }


        public ActionResult Logout()
        {
            Session["LOGIN_USER"]  = null;
            return RedirectToAction("Login", "User");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string email, string nickname, string password, string repassword, string verifyCode)
        {
            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
            {
                ModelState.AddModelError("Email", "请输入正确的邮箱！");
                return View();
            }
            if (string.IsNullOrEmpty(nickname))
            {
                ModelState.AddModelError("NickName", "昵称不能为空！");
                return View();
            }
            if (nickname.Length > 20)
            {
                ModelState.AddModelError("NickName", "昵称必须20个字符以内！");
                return View();
            }
            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                ModelState.AddModelError("Password", "密码至少要6位");
                return View();
            }
            if (password.Length > 25)
            {
                ModelState.AddModelError("Password", "密码太长，密码长度为6-25位字符！");
                return View();
            }
            if (password != repassword)
            {
                ModelState.AddModelError("Repassword", "两次密码输入不一致！");
                return View();
            }
            if (string.IsNullOrEmpty(verifyCode))
            {
                ModelState.AddModelError("VerifyCode", "请输入验证码！");
                return View();
            }
            if (verifyCode.Trim().ToLower() != (Session["VerifyCode"] + "").Trim().ToLower())
            {
                ModelState.AddModelError("VerifyCode", "验证码不正确！");
                return View();
            }

            //Md5密码
            string md5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            

            try
            {
                Model.User user = new Model.User();
                BLL.User bll = new BLL.User();
                if (bll.Exists(email))
                {
                    ModelState.AddModelError("Email", "对不起，该邮箱已经注册！");
                    return View();
                }

                user.AreaID = 0;
                user.Birth = null;
                user.Email = email;
                user.Intro = "";
                user.LoginCount = 0;
                user.LoginIp = "";
                user.LoginTime = null;
                user.Mobile = "";
                user.Nickname = nickname;
                user.Password = md5Password;
                user.Private = 0;
                user.RegIp = Helper.Util.GetIP();
                user.RegTime = DateTime.Now;
                user.Sex = 0;
                user.Sign = "";
                user.State = 0;
                bll.Add(user);

                //激活码：邮箱#注册时间 转md5
                string activeCode = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Email + "#" + user.RegTime, "MD5");
                string domainUrl = Request.Url.GetLeftPart(UriPartial.Authority);

                StringBuilder mailContent = new StringBuilder();
                string strActivateCodeURL = domainUrl + "/User/Activate?Email="+ email +"&ActivateCode=" + activeCode;

                mailContent.Append("<div style=\"font-size:14px; line-height:25px;\">");
                mailContent.Append("尊敬的" + nickname + "：");
                mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                mailContent.Append("恭喜您在<b>"+ Config.SiteName+"</b>注册成功，你注册帐号是：" + email + "，请您妥善保管您的密码，如果忘记密码，请<a href=\"" +
                    domainUrl + "/User/FindPassword?Email=" + email  + "\">点击这里找回密码</a>。");
                mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                mailContent.Append("您注册的账号需要激活才能正常使用，请尽快激活您的账号。<a href=\"" + strActivateCodeURL + "\"  target=\"_blank\">点击这里</a>进行激活，或者点击下面链接激活：");
                mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                mailContent.Append("<a href=\"" + strActivateCodeURL + "\"  target=\"_blank\">" + strActivateCodeURL + "</a>");
                mailContent.Append("<br />");
                mailContent.Append("<br />");

                mailContent.Append("<a href=\"http://" + Config.SiteDomain + "\"  target=\"_blank\"><span style=\"font-weight:bold; color:#F00; text-decoration:none;\">" + Config.SiteName + "（" + Config.SiteDomain + ")</span></a>");
                mailContent.Append("<br />");
                mailContent.Append(DateTime.Now.ToString("yyyy年MM月dd日"));
                mailContent.Append("<hr />");
                mailContent.Append("此邮件为自动发送，切勿回复。");
                mailContent.Append("</div>");

                Mail.Send(email, Config.SiteName + "注册成功，请及时激活帐号！", mailContent.ToString());

                ViewData["Email"] = email;
                return View("RegisterOk");
            }
            catch (Exception ex)
            {
                Helper.Log.AddErrorInfo("User/Reginster异常：" + ex.Message);
                ModelState.AddModelError("ErrorMsg", "注册异常，请稍后重试！" + ex.Message);
            }

            return View();
        }


        /// <summary>
        /// 用户激活
        /// </summary>
        /// <returns></returns>
        public ActionResult Activate()
        {
            string email = Request["Email"] + "";
            string activateCode = Request["ActivateCode"] + "";
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(activateCode))
            {
                ViewData["Msg"] = "激活失败，连接不合法！";
                return View();
            }

            BLL.User bll = new BLL.User();
            Model.User user = bll.GetModel(email);
            if (user == null)
            {
                ViewData["Msg"] = "激活失败，邮箱" + email + "尚未注册！";
                return View();
            }
            if (user.State == 1)
            {
                ViewData["Msg"] = "您已经激活，无需再次激活！";
                return View();
            }

            ViewData["Msg"] = "激活失败，非法Url！";
            string verifyActiveCode = FormsAuthentication.HashPasswordForStoringInConfigFile(email + "#" + user.RegTime, "MD5");
            if (verifyActiveCode.ToLower() == activateCode.Trim().ToLower())
            {
                user.State = 1;
                bll.Update(user);
                ViewData["Msg"] = "恭喜您，激活帐号成功！";
                return View();
            }

            return View();
        }


        /// <summary>
        /// 找回用户密码
        /// </summary>
        /// <returns></returns>
        public ActionResult FindPassword()
        {
            //StringBuilder strContent = new StringBuilder();
            //string strActivateCodeURL = WebAgent.GetDomainURL() + "/User/ResetPassword.aspx?Email=" + user.Email + "&Code=" + code;

            //strContent.Append("<div style=\"font-size:14px; line-height:25px;\">");
            //strContent.Append("尊敬的" + user.UserName + "：");
            //strContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

            //strContent.Append("欢迎您使用<b>快乐网(www.kuaile.us)</b>找回密码功能，请在48小时内连重置您的密码。<a href=\"" + strActivateCodeURL + "\"  target=\"_blank\">点击这里</a>进行重置密码，或者点击下面链接重置密码：");
            //strContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

            //strContent.Append("<a href=\"" + strActivateCodeURL + "\"  target=\"_blank\">" + strActivateCodeURL + "</a>");
            //strContent.Append("<br />");
            //strContent.Append("<br />");

            //strContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");
            //strContent.Append("如果您没有申请密码找回，请忽略此邮件。");
            //strContent.Append("<br />");
            //strContent.Append("<br />");

            //strContent.Append("<a href=\"" + WebAgent.GetDomainURL() + "\"  target=\"_blank\"><span style=\"font-weight:bold; color:#F00; text-decoration:none;\">快乐网（www.kuaile.us)</span></a>");
            //strContent.Append("<br />");
            //strContent.Append(DateTime.Now.ToString("yyyy年MM月dd日"));
            //strContent.Append("<hr />");
            //strContent.Append("此邮件为自动发送，切勿回复。");
            //strContent.Append("</div>");

            return View();
        }

    }
}
