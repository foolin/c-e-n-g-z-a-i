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
using System.Data;

namespace CengZai.Web.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            if (GetLoginUser() != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
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
        public ActionResult Login(string email, string password, int? remember)
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

                switch(Config.LoginLimit)
                {
                    //禁止所有用户登录
                    case 2:
                        ModelState.AddModelError("Error", "系统正在维护，暂停登录，请稍后再试！");
                        return View();
                        //break;

                    //只有激活用户可以登录
                    case 1:
                        if (user.State == 0)
                        {
                            ModelState.AddModelError("Error", "您的帐号尚未激活，请先登录邮箱激活帐号！");
                            return View();
                        }
                        else if (user.State == -1)
                        {
                            ModelState.AddModelError("Error", "您的帐号被冻结，暂时无法登录！");
                            return View();
                        }
                        break;
                    
                    //全部用户可以登录，包括锁定用户
                    case -1:
                        //全部用户可以登录
                        break;

                    //非锁定用户可以登录
                    case 0:
                    default:
                        if (user.State == -1)
                        {
                            ModelState.AddModelError("Error", "您的帐号被冻结，暂时无法登录！");
                            return View();
                        }
                        break;
                    
                }

                //更新用户到数据库
                bool isCreditPlus = false;
                if (user.LoginTime != null)
                {
                    DateTime lastLoginDate = Convert.ToDateTime(user.LoginTime);
                    DateTime nowDate = DateTime.Now;
                    //两个小时登录一次，积分+1
                    if (lastLoginDate.AddHours(2) < nowDate)
                    {
                        isCreditPlus = true;
                    }
                }
                else
                {
                    isCreditPlus = true;
                }
                if(isCreditPlus)
                {
                    if (user.Credit == null)
                    {
                        user.Credit = 1;
                    }
                    else
                    {
                        user.Credit = user.Credit + 1;
                    }
                }
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

                
                /******* 写入Cookies ********/
                //算法：
                //1.首先得到明文Val：用户ID|时间戳|“用户ID|时间戳”的MD5校验码
                //2.把明文经过DESEncrypt加密得到密文SecrectVal
                //3.把密文SecrectVal写入Cookies
                string cookieVal = user.UserID + "|" + DateTime.Now.Ticks.ToString();
                string cryptVal = cookieVal + "|" + System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(cookieVal, "MD5");
                HttpCookie cookie = new HttpCookie("LOGIN_USER");
                cookie.Value = DESEncrypt.Encrypt(cryptVal, Config.SecrectKey);    //加密存储
                if (remember == 1)
                {
                    cookie.Expires = DateTime.Now.AddDays(30);  //30天不过期
                }
                Response.Cookies.Add(cookie);

                //写入Session登录
                Session["LOGIN_USER"] = user;

                if (user.LoginCount <= 1)
                {
                    return RedirectToAction("FeedFriendsForFirstLogin", "Friend");
                }
                else
                {
                    if (Request["ReturnUrl"] == null || Request["ReturnUrl"].Length == 0)
                        return RedirectToAction("Index", "Home");
                    else
                        return Content("<script>location.href='" + Request["ReturnUrl"].ToString() + "';</script>");
                }
            }
            catch (Exception ex)
            {
                Log.Error("User/Login异常：" + ex.Message);
                ModelState.AddModelError("Error", "登录异常，请稍后重试！");
                return View();
            }
        }


        public ActionResult Logout()
        {
            if (Response.Cookies["LOGIN_USER"] != null)
            {
                Response.Cookies["LOGIN_USER"].Expires = DateTime.Now.AddDays(-9999);
            }
            Session["LOGIN_USER"]  = null;
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register(string invite)
        {
            if (GetLoginUser() != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.RegisterLimit = Config.RegisterLimit;
            if (Config.RegisterLimit == 2)
            {
                return Content(string.Format("系统在升级，暂停开放注册！ <a href='{2}'>返回首页</a>  <hr><a href='{1}'>{0}</a> ", Config.SiteName, Config.SiteHost, Util.GetCurrDomainUrl()));
            }
            if (!string.IsNullOrEmpty(invite))
            {
                Model.InviteCode model = new BLL.InviteCode().GetModelByInvite(invite);
                if (model == null)
                {
                    model = new Model.InviteCode();
                }
                ViewBag.InviteCode = model;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(string email, string username, string password, string repassword, string verifyCode, string invite, int? sex, int? chkTerms)
        {
            try
            {
                ViewBag.Password = password;
                ViewBag.Repassword = repassword;
                ViewBag.RegisterLimit = Config.RegisterLimit;

                Model.InviteCode mInvite = null;

                if (Config.RegisterLimit == 2)
                {
                    ModelState.AddModelError("Error", "系统在升级，暂停开放注册！");
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
                if (string.IsNullOrEmpty(email) || !Util.IsEmail(email))
                {
                    ModelState.AddModelError("Email", "请输入正确的邮箱！");
                    return View();
                }
                if (Config.RegisterLimit == 1)
                {
                    if (string.IsNullOrEmpty(invite))
                    {
                        ModelState.AddModelError("Invite", "请输入邀请码！");
                        return View();
                    }
                    BLL.InviteCode bllInvite = new BLL.InviteCode();
                    mInvite = bllInvite.GetModelByInvite(invite);
                    if (mInvite == null)
                    {
                        ModelState.AddModelError("Invite", "邀请码无效！");
                        return View();
                    }
                    //else if (mInvite.Email != email)
                    //{
                    //    ModelState.AddModelError("Invite", "该邀请码已经被其它用户使用！");
                    //    return View();
                    //}
                }
                if (string.IsNullOrEmpty(username))
                {
                    ModelState.AddModelError("Username", "用户名不能为空！");
                    return View();
                }
                if (username.Length < 4 || username.Length > 20)
                {
                    ModelState.AddModelError("Username", "用户名必须4到20个字符之间！");
                    return View();
                }
                if (!Regex.IsMatch(username, @"^[a-z][a-z0-9_]{3,19}$", RegexOptions.IgnoreCase))
                {
                    ModelState.AddModelError("Username", "用户名格式不正确！");
                    return View();
                }
                if (new BLL.User().GetModelByUserName(username) != null)
                {
                    ModelState.AddModelError("Username", "该用户名已经被注册！");
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


                //Md5密码
                string md5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");



                Model.User mUser = new Model.User();
                BLL.User bllUser = new BLL.User();
                if (bllUser.Exists(email))
                {
                    ModelState.AddModelError("Email", "对不起，该邮箱已经注册！");
                    return View();
                }

                mUser.AreaID = 0;
                mUser.Avatar = "";
                mUser.Birth = null;
                mUser.Email = email;
                mUser.Intro = "";
                mUser.LoginCount = 0;
                mUser.LoginIp = "";
                mUser.LoginTime = null;
                mUser.Mobile = "";
                mUser.Username = username;
                mUser.Nickname = username;
                mUser.Password = md5Password;
                mUser.Privacy = 0;
                mUser.RegIp = Helper.Util.GetIP();
                mUser.RegTime = DateTime.Now;
                mUser.Sex = sex;
                mUser.Sign = "此家伙不懒，就是什么也没留下";
                mUser.State = 0;
                mUser.Vip = 0;
                mUser.Credit = 0;
                mUser.Money = 0;
                mUser.Config = "";
                mUser.UserID = bllUser.Add(mUser);

                try
                {
                    if (mInvite != null)
                    {
                        new BLL.InviteCode().Delete(mInvite.ID);
                        if (mInvite.UserID > 0)
                        {
                            BLL.Friend bllFriend = new BLL.Friend();
                            bllFriend.Add(mUser.UserID, (int)mInvite.UserID);    //关注邀请人
                            bllFriend.Add((int)mInvite.UserID, mUser.UserID);    //关注被邀请人
                            Model.User inviteUser = bllUser.GetModel((int)mInvite.UserID);
                            if (inviteUser != null)
                            {
                                //如果是积分
                                if (Config.InviteCredit > 0)
                                {
                                    inviteUser.Credit = inviteUser.Credit + Config.InviteCredit;
                                    bllUser.Update(inviteUser);
                                }
                                SendSysNotice((int)mInvite.UserID, "您邀请的用户【" + mInvite.Email
                                    + "】已经成功注册，注册的昵称为：<a href='" + Url.Action("Blog", "Blog", new { username = username }) + "'>"
                                    + username + "</a>,快去看看吧！");
                                SendSysNotice(mUser.UserID, mUser.Nickname + "，邀请您邀请加入的人：<a href='" + Url.Action("Blog", "Blog", new { username = username }) + "'>"
                                    + username + "</a>,快去看看吧！");
                            }
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Account/Register注册激活码处理异常", ex);
                }

                try
                {
                    //激活码：邮箱#注册时间 转md5
                    string activeCode = FormsAuthentication.HashPasswordForStoringInConfigFile(mUser.Email + "#" + mUser.RegTime, "MD5");
                    string domainUrl = Request.Url.GetLeftPart(UriPartial.Authority);

                    StringBuilder mailContent = new StringBuilder();
                    string strActivateCodeURL = domainUrl + Url.Action("Activate", "Account", new { Email = email, ActivateCode = activeCode });    // "/User/Activate?Email=" + email + "&ActivateCode=" + activeCode;

                    mailContent.Append("<div style=\"font-size:14px; line-height:25px;\">");
                    mailContent.Append("尊敬的" + mUser.Nickname + "：");
                    mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                    mailContent.Append("恭喜您在<b>" + Config.SiteName + "</b>注册成功，你注册帐号是：" 
                        + email + "，请您妥善保管您的密码，如果忘记密码，请<a href=\"" +  domainUrl 
                        + Url.Action("FindPassword", "Account", new{ email = email }) + "\">点击这里找回密码</a>。");
                    mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                    mailContent.Append("您注册的账号需要激活才能正常使用，请尽快激活您的账号。<a href=\""  + strActivateCodeURL 
                        + "\"  target=\"_blank\">点击这里</a>进行激活，或者复制下面链接到浏览器进行激活：");
                    mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                    mailContent.Append("<a href=\"" + strActivateCodeURL + "\"  target=\"_blank\">" + strActivateCodeURL + "</a>");
                    mailContent.Append("<br />");
                    mailContent.Append("<br />");

                    mailContent.Append("<a href=\"" + Config.SiteHost + "\"  target=\"_blank\"><span style=\"font-weight:bold; color:#F00; text-decoration:none;\">" + Config.SiteName + "（" + Config.SiteHost + ")</span></a>");
                    mailContent.Append("<br />");
                    mailContent.Append(DateTime.Now.ToString("yyyy年MM月dd日"));
                    mailContent.Append("<hr />");
                    mailContent.Append("此邮件为自动发送，切勿回复。");
                    mailContent.Append("</div>");

                    Mail.Send(email, Config.SiteName + "注册成功，请及时激活帐号！", mailContent.ToString());
                }
                catch
                {
                    ViewBag.RegisterStatus = 0;
                    ViewBag.Message = string.Format(@"
                    你已经注册成功！但我们无法发送激活邮件到您的邮箱：{0},
                    为了您帐号的安全，请确认您的邮箱[{0}]是否正确！", email);
                    return View("RegisterOk");
                }

                ViewBag.RegisterStatus = 1;
                ViewBag.Message = string.Format(@"
                    恭喜你，你已经注册成功！我们已发送一封激活邮件到您邮箱：{0},
                    为了您帐号的安全，请及时登录邮箱[{0}]进行激活！
                    如果无法收到邮件，请检查邮箱是否正确，或者<a href='{1}'>点击这里重新发送邮件</a> 。"
                    , email, Url.Action("ResendActivate"));
                ViewData["Email"] = email;
                return View("RegisterOk");
            }
            catch (Exception ex)
            {
                Helper.Log.Error("User/Reginster异常：" + ex.Message);
                ModelState.AddModelError("ErrorMsg", "注册异常，请稍后重试！" + ex.Message);
            }

            return View();
        }



        /// <summary>
        /// 重发激活码邮件
        /// </summary>
        /// <returns></returns>
        public ActionResult ResendActivate(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    if (!Util.IsEmail(email))
                    {
                        ViewBag.Message = "您输入的邮件不合法，请重新输入！";
                        return View();
                    }
                    string key = "ResendActivate_" + email.ToLower();
                    int preSendTimeTicks = 0;
                    try
                    {
                        preSendTimeTicks = Convert.ToInt32(HttpContext.Cache[key]);
                    }
                    catch 
                    {
                        preSendTimeTicks = 0;
                    }
                    //两个小时内有效
                    if (DateTime.MinValue.Ticks >= preSendTimeTicks 
                        && preSendTimeTicks <= DateTime.MaxValue.Ticks 
                        && DateTime.Now.AddHours(-2).Ticks <= preSendTimeTicks)
                    {
                        ViewBag.Message = "两个小时切勿重复发送激活邮件，上次发送激活邮件为：" + new DateTime(preSendTimeTicks);
                        return View();
                    }
                    Model.User user = new BLL.User().GetModel(email);
                    if (user == null)
                    {
                        ViewBag.Message = "该邮箱尚未注册！";
                        return View();
                    }
                    //判断是否需要激活，只有0才使用激活
                    if (user.State != 0)
                    {
                        ViewBag.Message = "您的帐号无需激活";
                        return View();
                    }
                    try
                    {
                        //激活码：邮箱#注册时间 转md5
                        string activeCode = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Email + "#" + user.RegTime, "MD5");
                        string strActivateCodeURL = Util.GetCurrDomainUrl() + Url.Action("Activate", "Account", new { Email = email, ActivateCode = activeCode }); 
                        
                        StringBuilder mailContent = new StringBuilder();
                        mailContent.Append("<div style=\"font-size:14px; line-height:25px;\">");
                        mailContent.Append("尊敬的" + user.Nickname + "：");
                        mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                        mailContent.Append("您于" + DateTime.Now.ToString("yyyy年M月d日H时m分s秒申请重新发送激活邮件") 
                            + "，请尽快激活您的账号。<a href=\"" + strActivateCodeURL 
                            + "\"  target=\"_blank\">点击这里</a>进行激活，或者复制下面链接到浏览器进行激活：");
                        mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                        mailContent.Append("<a href=\"" + strActivateCodeURL + "\"  target=\"_blank\">" 
                            + strActivateCodeURL + "</a>");
                        mailContent.Append("<br />");
                        mailContent.Append("<br />");

                        mailContent.Append("如果忘记密码，请<a href=\"" + Util.GetCurrDomainUrl() 
                            + Url.Action("FindPassword", "Account", new { email = email }) + "\">点击这里找回密码</a>。");
                        mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");


                        mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");
                        mailContent.Append("如果您没有申请重发激活邮件，请忽略此邮件，打扰之处，请见谅。");
                        mailContent.Append("<br />");
                        mailContent.Append("<br />");

                        mailContent.Append("<a href=\"" + Config.SiteHost + "\"  target=\"_blank\"><span style=\"font-weight:bold; color:#F00; text-decoration:none;\">" + Config.SiteName + "（" + Config.SiteHost + ")</span></a>");
                        mailContent.Append("<br />");
                        mailContent.Append(DateTime.Now.ToString("yyyy年MM月dd日"));
                        mailContent.Append("<hr />");
                        mailContent.Append("此邮件为自动发送，切勿回复。");
                        mailContent.Append("</div>");

                        Mail.Send(email, "激活" + Config.SiteName + "帐号邮件", mailContent.ToString());
                        //缓存发送时间
                        HttpContext.Cache.Insert(key, DateTime.Now.Ticks, null, DateTime.Now.AddHours(2), TimeSpan.Zero);

                        ViewBag.Message = "激活帐号邮件已经发送，请尽快登录邮箱进行激活！<a class=\"btn btn-danger\" href=\"http://mail." + Util.GetEmailDomain(email) + "\" target=\"_blank\">登录邮箱进行激活</a> ";
                        return View();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("重发激活邮件异常", ex);
                        ViewBag.Message = "重发激活邮件失败，请检查邮箱是否正确，或者稍后重试。";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("重发激活邮件异常", ex);
                ViewBag.Message = "重发激活邮件异常";
                return View();
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
                ViewData["Message"] = "激活失败，连接不合法！";
                return View();
            }

            BLL.User bll = new BLL.User();
            Model.User user = bll.GetModel(email);
            if (user == null)
            {
                ViewData["Message"] = "激活失败，邮箱" + email + "尚未注册！";
                return View();
            }
            if (user.State == 1)
            {
                ViewData["Message"] = "您已经激活，无需再次激活！";
                return View();
            }

            ViewData["Message"] = "激活失败，非法Url！";
            string verifyActiveCode = FormsAuthentication.HashPasswordForStoringInConfigFile(email + "#" + user.RegTime, "MD5");
            if (verifyActiveCode.ToLower() == activateCode.Trim().ToLower())
            {
                user.State = 1;
                bll.Update(user);
                ViewData["Message"] = "恭喜您，激活帐号成功！";
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
            return View();
        }

        /// <summary>
        /// 找回用户密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FindPassword(string email, string verifyCode)
        {
            try
            {
                if (!Util.IsEmail(email))
                {
                    ViewBag.Message = "您输入的邮件不合法，请重新输入！";
                    ModelState.AddModelError("email", "您输入的邮件不合法，请重新输入！");
                    return View();
                }
                if (string.IsNullOrEmpty(verifyCode))
                {
                    ViewBag.Message = "请输入验证码！";
                    ModelState.AddModelError("VerifyCode", "请输入验证码！");
                    return View();
                }
                if (verifyCode.Trim().ToLower() != (Session["VerifyCode"] + "").Trim().ToLower())
                {
                    ViewBag.Message = "验证码不正确！";
                    ModelState.AddModelError("VerifyCode", "验证码不正确！");
                    return View();
                }
                string cacheKey = "FindPassword_" + email.ToLower();
                long preSendTimeTicks = 0;
                try
                {
                    preSendTimeTicks = Convert.ToInt64(HttpContext.Cache[cacheKey]);
                }
                catch
                {
                    preSendTimeTicks = 0;
                }
                //两个小时内有效
                if (preSendTimeTicks >= DateTime.MinValue.Ticks
                    && preSendTimeTicks <= DateTime.MaxValue.Ticks
                    && preSendTimeTicks >= DateTime.Now.AddHours(-2).Ticks)
                {
                    ViewBag.Message = "两个小时内只可以操作一次发送找回密码邮件，您上次发送找回密码时间为：" + new DateTime(preSendTimeTicks) 
                        + "，如果您未收到邮件，请注意确认是否被误认为是垃圾邮件。";
                    return View();
                }
                Model.User user = new BLL.User().GetModel(email);
                if (user == null)
                {
                    ViewBag.Message = "该帐号尚未被注册！";
                    return View();
                }
                //判断是否锁定用户
                if (user.State == -1)
                {
                    ViewBag.Message = "您的帐号已经被冻结或者不存在";
                    return View();
                }
                try
                {
                    //激活码：当前时间戳加密
                    string cacheVal = DateTime.Now.Ticks.ToString();
                    string pwdCode = DESEncrypt.Encrypt(cacheVal, Config.SecrectKey);
                    string strResetPasswordURL = Util.GetCurrDomainUrl()
                        + Url.Action("ResetPassword", "Account", new { Email = email, VerifyCode = pwdCode });

                    StringBuilder mailContent = new StringBuilder();
                    mailContent.Append("<div style=\"font-size:14px; line-height:25px;\">");
                    mailContent.Append("尊敬的" + user.Nickname + "：");
                    mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                    mailContent.Append("您于" + DateTime.Now.ToString("yyyy年M月d日H时m分s秒申请找回密码邮件")
                        + "，请尽快重置您的密码，该邮件两个小时内有效。<a href=\"" + strResetPasswordURL
                        + "\"  target=\"_blank\">点击这里</a>进行重置密码，或者复制下面链接到浏览器进行重置密码：");
                    mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                    mailContent.Append("<a href=\"" + strResetPasswordURL + "\"  target=\"_blank\">"
                        + strResetPasswordURL + "</a>");
                    mailContent.Append("<br />");
                    mailContent.Append("<br />");

                    mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");
                    mailContent.Append("如果您没有申请重置密码邮件，请忽略此邮件，打扰之处，请见谅。");
                    mailContent.Append("<br />");
                    mailContent.Append("<br />");

                    mailContent.Append("<a href=\"" + Config.SiteHost + "\"  target=\"_blank\"><span style=\"font-weight:bold; color:#F00; text-decoration:none;\">" + Config.SiteName + "（" + Config.SiteHost + ")</span></a>");
                    mailContent.Append("<br />");
                    mailContent.Append(DateTime.Now.ToString("yyyy年MM月dd日"));
                    mailContent.Append("<hr />");
                    mailContent.Append("此邮件为自动发送，切勿回复。");
                    mailContent.Append("</div>");

                    Mail.Send(email, "重置" + Config.SiteName + "帐号的密码邮件", mailContent.ToString());
                    //缓存发送时间
                    HttpContext.Cache.Insert(cacheKey, cacheVal, null, DateTime.Now.AddHours(2), TimeSpan.Zero);

                    ViewBag.Message = "重置密码邮件已经发送，请尽快登录邮箱进行进行重置，两个小时内有效！<a class=\"btn btn-danger\" href=\"http://mail." + Util.GetEmailDomain(email) + "\" target=\"_blank\">登录邮箱进行激活</a> ";
                    return View();
                }
                catch (Exception ex)
                {
                    Log.Error("发送重置密码邮件异常", ex);
                    ViewBag.Message = "发送重置密码邮件失败，请检查邮箱是否正确，或者稍后重试。";
                    return View();
                }
            }
            catch (Exception ex)
            {
                Log.Error("发送重置密码邮件异常", ex);
                ViewBag.Message = "发送重置密码邮件异常";
                return View();
            }
        }


        /// <summary>
        /// 用户重置密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ResetPassword(string email, string verifycode)
        {
            try
            {
                if (Session["RESET_PASSWORD_EMAIL"] == null)
                {
                    if (string.IsNullOrEmpty(email) || !Util.IsEmail(email) || string.IsNullOrEmpty(verifycode))
                    {
                        return JumpToTips("重置密码出错", "重置密码失败，连接不合法！");
                    }

                    string cacheKey = "FindPassword_" + email.ToLower();
                    string cacheVal = HttpContext.Cache[cacheKey] + "";
                    if (string.IsNullOrEmpty(cacheVal))
                    {
                        return JumpToTips("重置密码出错", "重置密码连接已失效！");
                    }
                    HttpContext.Cache.Remove(cacheKey); //移除连接
                    //缓存时间戳
                    long cacheTicks = Convert.ToInt64(cacheVal);
                    if (cacheTicks < DateTime.MinValue.Ticks || cacheTicks > DateTime.MaxValue.Ticks
                        || new DateTime(cacheTicks).AddHours(2).Ticks < DateTime.Now.Ticks)
                    {
                        return JumpToTips("重置密码出错", "重置密码连接已失效！");
                    }
                    //Url的时间戳
                    long urlTicks = 0;
                    try
                    {
                        urlTicks = Convert.ToInt64(DESEncrypt.Decrypt(verifycode, Config.SecrectKey));
                    }
                    catch { }
                    //校验Url时间戳
                    if (cacheTicks != urlTicks)
                    {
                        return JumpToTips("重置密码出错", "重置密码连接非法！");
                    }
                    BLL.User bll = new BLL.User();
                    Model.User user = bll.GetModel(email);
                    if (user == null)
                    {
                        return JumpToTips("重置密码出错", "重置密码非法，帐号不存在！");
                    }
                    if (user.State == -1)
                    {
                        return JumpToTips("重置密码出错", "您的帐号已经锁定，无法操作！");
                    }

                    ViewBag.Email = email;
                    //设置临时的保存Session
                    Session["RESET_PASSWORD_EMAIL"] = email;
                }
                else
                {
                    email = Session["RESET_PASSWORD_EMAIL"] + "";
                    if (!Util.IsEmail(email))
                    {
                        return JumpToTips("重置密码出错", "重置密码连接已过期！");
                    }
                    ViewBag.Email = email;
                }
            }
            catch (Exception ex)
            {
                Log.Error("重置密码异常", ex);
                return JumpToTips("重置密码出错", "对不起，重置密码出错，请稍后重试！");
            }
            return View();
        }


        /// <summary>
        /// 用户重置密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetPassword(string email, string password, string repassword)
        {
            try
            {
                if (Session["RESET_PASSWORD_EMAIL"] == null || Session["RESET_PASSWORD_EMAIL"].ToString() != email)
                {
                    return JumpToTips("重置密码失败", "重置密码操作已过期！");
                }
                if (string.IsNullOrEmpty(password) || password.Length < 6 || password.Length > 20)
                {
                    ViewBag.Message = "密码的长度为6至20个字符之间！";
                    return View();
                }
                if (password != repassword)
                {
                    ViewBag.Message = "两次密码不一致！";
                    return View();
                }
                Session["RESET_PASSWORD_EMAIL"] = null;  //置空Session
                BLL.User bllUser = new BLL.User();
                Model.User user = bllUser.GetModel(email);
                if (user == null || user.State == -1)
                {
                    return JumpToTips("修改密码失败！", "用户不存在或者被锁定");
                }
                string md5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                user.Password = md5Password;
                bllUser.Update(user);
                return JumpToAction("重置密码成功", "恭喜，重置密码成功！", "Login");
            }
            catch (Exception ex)
            {
                Log.Error("重置密码异常！", ex);
                return JumpToTips("重置密码失败", "重置密码异常，请重试或者联系客服！");
            }
        }


        /// <summary>
        /// 申请邀请码
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplyInvite()
        {
            return View();
        }

        /// <summary>
        /// 申请邀请码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ApplyInvite(string email, string verifyCode)
        {
            try
            {
                if (!Util.IsEmail(email))
                {
                    ViewBag.Message = "对不起，您输入的邮箱不正确，不能申请邀请码！";
                    ModelState.AddModelError("email", "请输入正确邮箱！");
                    return View();
                }
                if (string.IsNullOrEmpty(verifyCode))
                {
                    ViewBag.Message = "请输入验证码！";
                    ModelState.AddModelError("VerifyCode", "请输入验证码！");
                    return View();
                }
                if (verifyCode.Trim().ToLower() != (Session["VerifyCode"] + "").Trim().ToLower())
                {
                    ViewBag.Message = "验证码不正确！";
                    ModelState.AddModelError("VerifyCode", "验证码不正确！");
                    return View();
                }
                if (new BLL.User().GetModel(email) != null)
                {
                    ViewBag.Message = "您邮箱"+email+"已经注册，无需申请邀请码。";
                    return View();
                }
                BLL.InviteCode bllInvite = new BLL.InviteCode();
                DataSet dsExistList = bllInvite.GetList("email='" + email + "'");
                if (dsExistList != null && dsExistList.Tables.Count > 0
                    && dsExistList.Tables[0].Rows.Count > 0)
                {
                    ViewBag.Message = "您已经申请邀请码，无需重复提交申请，我们会定期发放并发送您的邮箱[" + email + "]通知您，请耐心等待。";
                    return View();
                }
                Model.InviteCode mInvite = new Model.InviteCode();
                mInvite.Email = email;
                mInvite.Invite = "";
                mInvite.UserID = 0;
                mInvite.ID = bllInvite.Add(mInvite);
                if (mInvite.ID > 0)
                {
                    return JumpToTips("已经成功提交申请", "您好，您已经成功提交申请！我们会定期发放并发送邮箱[" + email + "]通知您，请耐心等待。");
                }
                else
                {
                    ViewBag.Message = "您好，未知错误，请稍后重试。";
                    return View();
                }
            }
            catch (Exception ex)
            {
                Log.Error("申请邀请码异常", ex);
                ViewBag.Message = "申请邀请码异常，请稍后重试。";
                return View();
            }
        }


    }
}
