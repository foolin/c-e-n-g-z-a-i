using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Model;
using CengZai.Helper;
using CengZai.Web.Common;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Text;
using CengZai.OAuthSDK.QQ;
using CengZai.OAuthSDK.Sina;

namespace CengZai.Web.Controllers
{
    public class OAuthController : BaseController
    {
        // QQ登录页面 
        [HttpGet]
        public ActionResult QQLogin()
        {
            Model.User loginUser = GetLoginUser();
            if (loginUser == null)
            {
                var qqApi = new QQApi();
                string state = Guid.NewGuid().ToString().Replace("-", "");
                Session["requeststate"] = state;
                string scope = "get_user_info,add_share,list_album,upload_pic,check_page_fans,add_t,add_pic_t,del_t,get_repost_list,get_info,get_other_info,get_fanslist,get_idolist,add_idol,del_idol,add_one_blog,add_topic,get_tenpay_addr";
                var authenticationUrl = qqApi.GetAuthorizationUrl(state, scope);
                return new RedirectResult(authenticationUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        // 回调页面 
        public ActionResult QQCallback()
        {
            if (Request.Params["code"] != null)
            {
                var qqApi = new QQApi();
                var code = Request.Params["code"];
                var state = Request.Params["state"];
                string requestState = Session["requeststate"].ToString();
                if (state == requestState)
                {
                    qqApi.GetAccessToken(code);
                    var currentUser = qqApi.GetUserInfo();
                    string openId = qqApi.Token.Uid;
                    if (currentUser == null || string.IsNullOrEmpty(openId))
                    {
                        return JumpToTips("登录失败！", "授权错误！请稍后重试！");
                    }
                    BLL.User bllUser = new BLL.User();
                    Model.User mUser = bllUser.GetModelByOpenId(openId, Model.LoginType.QQ);
                    if (mUser == null)
                    {
                        mUser = new Model.User();
                        mUser.AreaID = 0;
                        mUser.Avatar = currentUser.figureurl_2;
                        mUser.Birth = null;
                        mUser.Email = "";
                        mUser.Intro = "";
                        mUser.LoginCount = 0;
                        mUser.LoginIp = "";
                        mUser.LoginTime = null;
                        mUser.Mobile = "";
                        mUser.Username = "";
                        mUser.Nickname = currentUser.nickname;
                        mUser.Password = "";
                        mUser.Privacy = 0;
                        mUser.RegIp = Helper.Util.GetIP();
                        mUser.RegTime = DateTime.Now;
                        mUser.Sex = (currentUser.gender == "男" ? 1 : 2);
                        mUser.Sign = "此家伙不懒，就是什么也没留下";
                        mUser.State = 0;
                        mUser.Vip = 0;
                        mUser.Credit = 5;   //初次登录赠送5个积分
                        mUser.Money = 0;
                        mUser.Config = new UserConfig();
                        //用户尚未注册
                        mUser.LoginType = Model.LoginType.QQ;
                        mUser.AccessToken = qqApi.Token.AccessToken;
                        mUser.OpenId = openId;
                        mUser.AuthTime = DateTime.Now;
                        mUser.UserID = bllUser.Add(mUser);
                    }
                    else
                    {
                        switch (Config.LoginLimit)
                        {
                            //禁止所有用户登录
                            case 2:
                                return JumpToTips("Error", "系统正在维护，暂停登录，请稍后再试！");
                            //break;

                            //只有激活用户可以登录
                            case 1:
                                if (mUser.State == 0)
                                {
                                    return JumpToTips("Error", "您的帐号尚未激活，请先登录邮箱激活帐号！");
                                }
                                else if (mUser.State == -1)
                                {
                                    return JumpToTips("Error", "您的帐号被冻结，暂时无法登录！");
                                }
                                break;

                            //全部用户可以登录，包括锁定用户
                            case -1:
                                //全部用户可以登录
                                break;

                            //非锁定用户可以登录
                            case 0:
                            default:
                                if (mUser.State == -1)
                                {
                                    return JumpToTips("Error", "您的帐号被冻结，暂时无法登录！");
                                }
                                break;
                        }
                        
                    }
                    //更新登录用户信息，写入Cookies和Session登录
                    UpdateLoginUserInfo(mUser);
                    UpdateLoginUserCookie(mUser, false);
                    UpdateLoginUserSession(mUser);

                    if (string.IsNullOrEmpty(mUser.Email) || string.IsNullOrEmpty(mUser.Username))
                    {
                        return RedirectToAction("Register");
                    }
                    else
                    {
                        return JumpToHome("登录成功！", "正在为您跳转到首页...");
                    }
                }
            }
            return JumpToTips("登录失败！", "未知错误！");
        }

        //新浪登录
        public ActionResult SinaLogin()
        {
            Model.User loginUser = GetLoginUser();
            if (loginUser == null)
            {
                SinaApi sinaApi = new SinaApi();
                string state = Guid.NewGuid().ToString().Replace("-", "");
                Session["requeststate"] = state;
                var authenticationUrl = sinaApi.GetAuthorizationUrl(state);
                return new RedirectResult(authenticationUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //新浪登录
        public ActionResult SinaCallback()
        {
            try
            {
                SinaApi sinaApi = new SinaApi();
                var state = Request.Params["state"];
                string requestState = Session["requeststate"].ToString();
                if (state == requestState)
                {
                    sinaApi.GetAccessToken(Request["code"]);
                    SinaUser sinaUser = sinaApi.GetUserInfo();
                    if (sinaUser == null)
                    {
                        return JumpToTips("读取用户信息失败", "对不起，读取您在新浪微博的帐号失败");
                    }
                    string accesstoken = sinaApi.Token.AccessToken;
                    string openId = sinaApi.Token.Uid;
                    if (sinaUser == null || string.IsNullOrEmpty(openId))
                    {
                        return JumpToTips("登录失败！", "授权错误！请稍后重试！");
                    }
                    BLL.User bllUser = new BLL.User();
                    Model.User mUser = bllUser.GetModelByOpenId(openId, Model.LoginType.Sina);
                    if (mUser == null)
                    {
                        mUser = new Model.User();
                        mUser.AreaID = 0;
                        mUser.Avatar = sinaUser.profile_image_url;
                        mUser.Birth = null;
                        mUser.Email = "";
                        mUser.Intro = sinaUser.description;
                        mUser.LoginCount = 0;
                        mUser.LoginIp = "";
                        mUser.LoginTime = null;
                        mUser.Mobile = "";
                        mUser.Username = "";
                        mUser.Nickname = sinaUser.name;
                        mUser.Password = "";
                        mUser.Privacy = 0;
                        mUser.RegIp = Helper.Util.GetIP();
                        mUser.RegTime = DateTime.Now;
                        mUser.Sex = (sinaUser.gender == "m" ? 1 : 2);
                        mUser.Sign = "此家伙不懒，就是什么也没留下";
                        mUser.State = 0;
                        mUser.Vip = 0;
                        mUser.Credit = 5;   //初次登录赠送5个积分
                        mUser.Money = 0;
                        mUser.Config = new UserConfig();
                        //用户尚未注册
                        mUser.LoginType = Model.LoginType.Sina;
                        mUser.AccessToken = accesstoken;
                        mUser.OpenId = openId;
                        mUser.AuthTime = DateTime.Now;
                        mUser.UserID = bllUser.Add(mUser);
                    }
                    else
                    {
                        switch (Config.LoginLimit)
                        {
                            //禁止所有用户登录
                            case 2:
                                return JumpToTips("Error", "系统正在维护，暂停登录，请稍后再试！");
                            //break;

                            //只有激活用户可以登录
                            case 1:
                                if (mUser.State == 0)
                                {
                                    return JumpToTips("Error", "您的帐号尚未激活，请先登录邮箱激活帐号！");
                                }
                                else if (mUser.State == -1)
                                {
                                    return JumpToTips("Error", "您的帐号被冻结，暂时无法登录！");
                                }
                                break;

                            //全部用户可以登录，包括锁定用户
                            case -1:
                                //全部用户可以登录
                                break;

                            //非锁定用户可以登录
                            case 0:
                            default:
                                if (mUser.State == -1)
                                {
                                    return JumpToTips("Error", "您的帐号被冻结，暂时无法登录！");
                                }
                                break;
                        }
                    }
                    //更新登录用户信息
                    UpdateLoginUserInfo(mUser);
                    //写入Cookies和Session登录
                    UpdateLoginUserCookie(mUser, false);
                    UpdateLoginUserSession(mUser);

                    if (string.IsNullOrEmpty(mUser.Email) || string.IsNullOrEmpty(mUser.Username))
                    {
                        return RedirectToAction("Register");
                    }
                    else
                    {
                        return JumpToHome("登录成功！", "正在为您跳转到首页...");
                    }
                }
                return JumpToTips("校验失败", "请重试！");
            }
            catch (Exception ex)
            {
                Log.Error("新浪微博登录出错！", ex);
                return JumpToTips("登录出错！", "对不起，使用新浪微博帐号登录出错！");
            }
        }

        //完善用户信息
        [CheckAuthFilter]
        public ActionResult Register()
        {
            ViewBag.RegisterLimit = Config.RegisterLimit;
            Model.User loginUser = GetLoginUser();
            if (loginUser == null)
            {
                return JumpToTips("您尚未登录", "对不起，您尚未登录或者连接错误");
            }
            if (!string.IsNullOrEmpty(loginUser.Email) && !string.IsNullOrEmpty(loginUser.Username))
            {
                return JumpToHome("登录成功！", "正在为您跳转到首页...");
            }
            return View();
        }

        //完善用户信息
        [CheckAuthFilter]
        [HttpPost]
        public ActionResult Register(string email, string username, string nickname, string invite)
        {
            try
            {
                BLL.User bllUser = new BLL.User();
                Model.User loginUser = GetLoginUser();
                if(loginUser == null)
                {
                    return JumpToTips("您尚未登录","对不起，您尚未登录或者连接错误");
                }
                if (!string.IsNullOrEmpty(loginUser.Email) && !string.IsNullOrEmpty(loginUser.Username))
                {
                    return JumpToHome("登录成功！", "正在为您跳转到首页...");
                }

                ViewBag.RegisterLimit = Config.RegisterLimit;
                Model.InviteCode mInvite = null;

                if (Config.RegisterLimit == 2)
                {
                    ModelState.AddModelError("Error", "系统在升级，暂停开放注册！");
                    return View();
                }
                if (string.IsNullOrEmpty(email) || !Util.IsEmail(email))
                {
                    ModelState.AddModelError("Email", "请输入正确的邮箱！");
                    return View();
                }
                if (bllUser.Exists(email))
                {
                    ModelState.AddModelError("Email", "对不起，该邮箱已经注册！");
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
                if (username.Length < 5 || username.Length > 20)
                {
                    ModelState.AddModelError("Username", "用户名必须5到20个字符之间！");
                    return View();
                }
                if (!Regex.IsMatch(username, @"^[a-z][a-z0-9_]{4,19}$", RegexOptions.IgnoreCase))
                {
                    ModelState.AddModelError("Username", "用户名格式不正确！");
                    return View();
                }
                string protectedUserName = "," + Config.ProtectUsername + ",";
                if (!string.IsNullOrEmpty(protectedUserName) && protectedUserName.IndexOf("," + username + ",") != -1)
                {
                    ModelState.AddModelError("Username", "该用户名已经被注册或者保护！");
                    return View();
                }
                if (new BLL.User().GetModelByUserName(username) != null)
                {
                    ModelState.AddModelError("Username", "该用户名已经被注册！");
                    return View();
                }
                if (!string.IsNullOrEmpty(nickname) && nickname.Length > 20)
                {
                    nickname = nickname.Substring(0, 20);
                }
                if (string.IsNullOrEmpty(nickname))
                {
                    nickname = username;
                }
                loginUser.Email = email;
                loginUser.Username = username;
                loginUser.Nickname = nickname;
                bllUser.Update(loginUser);

                //发送激活邮件
                SendActivateEmail(loginUser);

                try
                {
                    if (loginUser.LoginType == LoginType.QQ)
                    {
                        QQApi qqApi = new QQApi();
                        qqApi.SetToken(loginUser.AccessToken, loginUser.OpenId, 0);
                        //qzone.AddWeibo("我刚注册了" + Config.SiteName + "，快来看看吧", Url.BlogUrl(loginUser.Username), Config.SiteSlogan, Config.SiteDescription);
                        qqApi.AddShare("我刚注册了" + Config.SiteName + "，快来看看吧！"
                            , Url.BlogUrl(loginUser.Username)
                            , Config.SiteSlogan
                            , Config.SiteDescription);
                    }
                    else if (loginUser.LoginType == LoginType.Sina)
                    {
                        SinaApi sinaApi = new SinaApi();
                        sinaApi.SetToken(loginUser.AccessToken, loginUser.OpenId, 0);
                        string content = "我刚注册了" + Config.SiteName + ":" + Url.BlogUrl(loginUser.Username) + "，快来看看吧！" + Config.SiteSlogan + "//" + Config.SiteDescription;
                        if (content.Length > 140)
                        {
                            content = content.Substring(0, 140);
                        }
                        string url = Url.BlogUrl(loginUser.Username);
                        sinaApi.AddWeibo(content);
                    }
                }
                catch { }
                

                return RedirectToAction("FeedFriendsForFirstLogin", "Friend");
            }
            catch (Exception ex)
            {
                Log.Error("使用第三方帐号登录，完善信息出现异常", ex);
                return JumpToTips("完善信息异常", "完善信息异常，请稍后重试！");
            }
        }


        private bool SendActivateEmail(Model.User mUser)
        {
            try
            {
                //激活码：邮箱#注册时间 转md5
                string activeCode = FormsAuthentication.HashPasswordForStoringInConfigFile(mUser.Email + "#" + mUser.RegTime, "MD5");
                string domainUrl = Request.Url.GetLeftPart(UriPartial.Authority);

                StringBuilder mailContent = new StringBuilder();
                string strActivateCodeURL = domainUrl + Url.Action("Activate", "Account", new { Email = mUser.Email, ActivateCode = activeCode });    // "/User/Activate?Email=" + email + "&ActivateCode=" + activeCode;

                mailContent.Append("<div style=\"font-size:14px; line-height:25px;\">");
                mailContent.Append("尊敬的" + mUser.Nickname + "：");
                mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                mailContent.Append("恭喜您在<b>" + Config.SiteName + "</b>注册成功，您使用的是"+ BLL.User.GetLoginTypeName(mUser.LoginType) +"帐号进行登录。");
                mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                mailContent.Append("您注册的账号需要激活才能正常使用，请尽快激活您的账号。<a href=\"" + strActivateCodeURL
                    + "\"  target=\"_blank\">点击这里</a>进行激活，或者复制下面链接到浏览器进行激活：");
                mailContent.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;");

                mailContent.Append("<a href=\"" + strActivateCodeURL + "\"  target=\"_blank\">" + strActivateCodeURL + "</a>");
                mailContent.Append("<br />");
                mailContent.Append("<br />");

                mailContent.Append("<a href=\"" + Config.SiteHost + "\"  target=\"_blank\"><span style=\"font-weight:bold; color:#F00; text-decoration:none;\">" + Config.SiteName + "（" + Config.SiteDomain + ")</span></a>");
                mailContent.Append("<br />");
                mailContent.Append(DateTime.Now.ToString("yyyy年MM月dd日"));
                mailContent.Append("<hr />");
                mailContent.Append("此邮件为自动发送，切勿回复。");
                mailContent.Append("</div>");

                Mail.Send(mUser.Email, Config.SiteName + "注册成功，请及时激活帐号！", mailContent.ToString());
                return true;
            }
            catch
            {
                return false;
            }

        }


        public ActionResult TestLogin()
        {
            /********** qq测试 *********/
            //var api = new QQApi();
            //string state = Guid.NewGuid().ToString().Replace("-", "");
            //Session["requeststate"] = state;
            //string scope = "get_user_info,add_share,list_album,upload_pic,check_page_fans,add_t,add_pic_t,del_t,get_repost_list,get_info,get_other_info,get_fanslist,get_idolist,add_idol,del_idol,add_one_blog,add_topic,get_tenpay_addr";
            //string url = api.GetAuthorizationUrl(state, scope);
            //return Redirect(url);
            
            /********* 新浪微博测试 *********/
            var api = new SinaApi();
            string state = Guid.NewGuid().ToString().Replace("-", "");
            Session["requeststate"] = state;
            string url = api.GetAuthorizationUrl(state);
            return Redirect(url);
        }


        public ActionResult TestCallBack()
        {
            //if (Request.Params["code"] != null)
            //{
            //    var verifier = Request.Params["code"];
            //    var state = Request.Params["state"];
            //    string requestState = Session["requeststate"].ToString();
            //    if (state == requestState)
            //    {
            //        QQApi api = new QQApi();
            //        api.GetAccessToken(Request.Params["code"]);
            //        QQUser qqUser = api.GetUserInfo();
            //        //api.AddShare("测试分享" + DateTime.Now.ToString()
            //        //    , "http://foolin.cengzai.com"
            //        //    , "我的来源说明"
            //        //    , "这是评论哈哈哈哈"
            //        //    , "这里是内容摘要"
            //        //    , "http://www.cengzai.com/resource/img/logo_topbar.png"
            //        //    , "1"
            //        //    , "4"
            //        //    , ""
            //        //    , ""
            //        //    );

            //        byte[] fileBytes = System.IO.File.ReadAllBytes("E:\\test.jpg");
            //        api.AddWeibo("测试微博，直接二进制发送微博" + DateTime.Now.ToString(), fileBytes, "202.12.1.12", "23", "53", "0");
            //    }
            //}


            if (Request.Params["code"] != null)
            {
                var verifier = Request.Params["code"];
                var state = Request.Params["state"];
                string requestState = Session["requeststate"].ToString();
                if (state == requestState)
                {
                    SinaApi api = new SinaApi();
                    api.GetAccessToken(Request.Params["code"]);
                    //SinaUser qqUser = api.GetUserInfo();
                    byte[] fileBytes = System.IO.File.ReadAllBytes("E:\\test.jpg");
                    api.AddWeibo("测试微博，直接二进制发送微博" + Guid.NewGuid().ToString());
                    api.AddWeibo("测试微博，直接二进制发送微博" + Guid.NewGuid().ToString(), fileBytes, 23, 53, "");
                    api.AddWeibo("测试微博，直接二进制发送微博" + Guid.NewGuid().ToString(), "E:\\test2.jpg", 23, 53, "");
                    api.AddWeiboTestFiles("测试批量文件发微博，直接二进制发送微博" + Guid.NewGuid().ToString(), "E:\\test2.jpg");
                }
            }
            return JumpToTips("",  "测试结束");
        }

    }
}
