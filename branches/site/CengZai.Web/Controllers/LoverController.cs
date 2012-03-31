using System;
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
        [CheckAuthFilter]
        public ActionResult Index()
        {
            try
            {
                Model.User user = GetLoginUser();
                ViewBag.User = user;

                BLL.Lover bllLover = new BLL.Lover();
                //取我的Lover
                ViewBag.MyLover =  bllLover.GetMyLover(user.UserID);
                //取我收到的Receive
                ViewBag.ReceiveLoverList = bllLover.GetReceiveList(user.UserID);
                //取最新列表
                ViewBag.TopLoverList = bllLover.GetModelList(200,"State=1", "ApplyTime ASC,JoinDate ASC");


            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("Lover/Index异常", ex);
                return JumpTo("对不起，出错了", "哎呀，对不起！出错了，请刷新或者稍后访问！", "", 0);
            }
            return View();
        }


        /// <summary>
        /// 申请
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        public ActionResult Apply()
        {
            try
            {
                Model.User user = GetLoginUser();
                ViewBag.User = user;

                BLL.Lover bllLover = new BLL.Lover();
                Model.Lover lover = bllLover.GetMyLover(user.UserID);
                if (lover != null)
                {
                    if (lover.State == 1)
                    {
                        return JumpToAction("对不起，您无权申请！", "您已经领了证书，不可以再申请证书，如果需要申请，请注销原来的证书！", "Certificate", new { LoverID = lover.LoverID });
                    }
                    else
                    {
                        return JumpToAction("对不起，您无权申请！", "您已经有证书在处理中，不可以再申请其它证书，请注销原来的证书！", "Certificate", new { LoverID = lover.LoverID });
                    }
                }
                DataSet dsFriendList = new BLL.Friend().GetFriendUserList(user.UserID, Model.FriendRelation.Follow, 0, "");
                if (dsFriendList != null && dsFriendList.Tables.Count > 0)
                {
                    ViewBag.FriendList = new BLL.User().DataTableToList(dsFriendList.Tables[0]);
                }
                if (user.Sex == 0)
                {
                    return AlertAndBack("您的性别未知，请完善资料再来申请！");
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("Lover/Apply出现异常", ex);
                return JumpToTips("囧！", "啊哟，出现点小小异常，请稍后重试或者联系我们。");
            }
            return View();
        }

        /// <summary>
        /// 申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CheckAuthFilter]
        public ActionResult Apply(int? certificate, int? honeyUserID, string nickname, string avatar, string mobile, DateTime? birth, DateTime? joinDate, string oath)
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
                if (new BLL.User().GetModel((int)honeyUserID) == null)
                {
                    return AjaxReturn("honeyUserID", "唉哟，对方帐号怎么不存在呢？");
                }
                if (new BLL.Lover().GetMyLover((int)honeyUserID) != null)
                {
                    return AjaxReturn("honeyUserID", "唉哟，对方帐号怎么不存在呢？");
                }
                nickname = (nickname + "").Trim();
                if (nickname.Length == 0 || nickname.Length > 20)
                {
                    return AjaxReturn("nickname", "请输入您的名字");
                }
                if (string.IsNullOrEmpty(avatar) ||
                    !System.IO.File.Exists(Server.MapPath(Config.UploadMapPath + "/" + avatar))
                    )
                {
                    return AjaxReturn("avatar", "请上传你们的合照头像");
                }
                if (string.IsNullOrEmpty(mobile) || !System.Text.RegularExpressions.Regex.IsMatch(mobile, @"1[0-9]{10}"))
                {
                    return AjaxReturn("mobile", "请正确填写您的手机号(代替身份证)");
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
                    return AjaxReturn("oath", "你的誓言10个字符都不到？没话跟对方的说？");
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
                lover.Flow = (int)Model.LoverFlow.Apply;
                lover.State = 0;
                lover.UpdateUserID = user.UserID;
                lover.UpdateTime = DateTime.Now;
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
                    return AjaxReturn("success", "申请成功，请耐心等待对方的答案！");
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
        public ActionResult Accept(int? loverID)
        {
            try
            {
                if (loverID == null)
                {
                    return JumpToHome("对不起，操作错误！", "您访问的页面不存在！");
                }

                Model.User user = GetLoginUser();
                ViewBag.User = user;

                Model.Lover lover = new BLL.Lover().GetModel((int)loverID);
                if (lover == null || lover.State != 0)
                {
                    return JumpToHome("对不起，操作错误！", "您访问的页面不存在或已经失效！");
                }
                if (lover.State == 1 || lover.Flow != (int)Model.LoverFlow.Apply)
                {
                    return JumpToHome("对不起，操作错误！", "您访问的申请已经失效！");
                }
                if (lover.BoyUserID != user.UserID && lover.GirlUserID != user.UserID)
                {
                    return JumpToHome("对不起，操作错误！", "您访问无权操作，我们不支持第三者！");
                }
                if (lover.ApplyUserID == user.UserID)
                {
                    if (lover.State >= 0)
                    {
                        return JumpToAction("您无权操作！", "您访问无权操作，这个申请是您提出的！", "Preview", new { LoverID = loverID });
                    }
                    else
                    {
                        return JumpToHome("您无权操作！", "您访问无权操作，这个申请是您提出的！");
                    }
                }
                ViewBag.Lover = lover;
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("Lover/UnAccept出现异常：", ex);
                return JumpToHome("对不起，操作错误！", "您访问的页面出现点异常！");
            }

            return View();
        }


        /// <summary>
        /// 接收
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        [HttpPost]
        public ActionResult Accept(int? loverID, string nickname, string avatar, string mobile, DateTime? birth, DateTime? joinDate, string oath)
        {
            try
            {
                if (loverID == null)
                {
                    return AjaxReturn("error", "您访问的页面不存在！");
                }

                Model.User user = GetLoginUser();
                ViewBag.User = user;

                Model.Lover lover = new BLL.Lover().GetModel((int)loverID);
                if (lover == null || lover.State != 0)
                {
                    return AjaxReturn("error", "您访问的页面不存在！");
                }
                if ((lover.BoyUserID != user.UserID && lover.GirlUserID != user.UserID)
                    || lover.ApplyUserID == user.UserID
                    )
                {
                    return AjaxReturn("error", "您访问无权操作！");
                }
                if (lover.State == 1 || lover.Flow != (int)Model.LoverFlow.Apply)
                {
                    return AjaxReturn("error", "您访问的登记已经失效！");
                }

                if (new BLL.User().GetModel(user.UserID) == null)
                {
                    return AjaxReturn("honeyUserID", "唉哟，对方帐号怎么不存在呢？");
                }
                nickname = (nickname + "").Trim();
                if (nickname.Length == 0 || nickname.Length > 20)
                {
                    return AjaxReturn("nickname", "请输入您的名字");
                }
                if (string.IsNullOrEmpty(avatar) ||
                    !System.IO.File.Exists(Server.MapPath(Config.UploadMapPath + "/" + avatar))
                    )
                {
                    return AjaxReturn("avatar", "请上传你们的合照头像");
                }
                if (string.IsNullOrEmpty(mobile) || !System.Text.RegularExpressions.Regex.IsMatch(mobile, @"1[0-9]{10}"))
                {
                    return AjaxReturn("mobile", "请正确填写您的手机作为身份证号！");
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
                    return AjaxReturn("oath", "誓言十个字符也不到？难道你就没话对对方的说？");
                }
                if (oath.Length > 1000)
                {
                    return AjaxReturn("oath", "誓言这么长超过1000字啊？精简点吧，记得说重点哦...");
                }

                //更新申请者信息
                BLL.User bllUser = new BLL.User();
                user.Username = user.Nickname;
                user.Mobile = mobile;
                user.Birth = birth;
                bllUser.Update(user);

                //更新资料
                lover.Avatar = avatar;
                lover.JoinDate = joinDate;
                lover.Flow = (int)Model.LoverFlow.Accept;
                lover.UpdateUserID = user.UserID;
                lover.UpdateTime = DateTime.Now;
                if (lover.BoyUserID == user.UserID)
                {
                    lover.BoyOath = oath;
                }
                else if (lover.GirlUserID == user.UserID)
                {
                    lover.GirlOath = oath;
                }
                else
                {
                    return AjaxReturn("error", "严重鄙视你！原来你不是男方，也不是女方，传说中的第三者？");
                }
                if (new BLL.Lover().Update(lover))
                {
                    return AjaxReturn("success", "接受成功！请耐心等待系统审核...");
                }
                else
                {
                    return AjaxReturn("error", "申请失败，请检查输入或者稍后重试！");
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("LoverController.Accept()出现异常", ex);
                return AjaxReturn("error", "操作异常，请检查输入或者稍后重试");
            }
        }


        /// <summary>
        /// 拒绝对方
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        public ActionResult UnAccept(int? loverID)
        {
            if (loverID == null)
            {
                return JumpToHome("对不起，操作错误！", "您访问的页面不存在！");
            }
            try
            {
                Model.User user = GetLoginUser();
                ViewBag.User = user;

                Model.Lover lover = new BLL.Lover().GetModel((int)loverID);
                if (lover == null)
                {
                    return JumpToHome("对不起，操作错误！", "您访问的页面不存在！");
                }
                if (lover.BoyUserID != user.UserID && lover.GirlUserID != user.UserID)
                {
                    return JumpToHome("对不起，操作错误！", "您无权限访问和操作！");
                }
                if (lover.ApplyUserID != user.UserID && lover.State == 0 && lover.Flow == (int)Model.LoverFlow.Apply)
                {
                    //如果还生效
                }
                else
                {
                    return JumpToHome("对不起，操作错误！", "您访问页面已失效！");
                }
                ViewBag.Lover = lover;
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("Lover/UnAccept出现异常：", ex);
                return JumpToHome("对不起，操作错误！", "您访问的页面出现点异常！");
            }

            return View();
        }

        [CheckAuthFilter]
        [HttpPost]
        public ActionResult UnAccept(int? loverID, string why)
        {
            try
            {
                if (loverID == null)
                {
                    JumpToHome("对不起，操作错误！", "您访问的操作页面不存在！");
                }

                Model.User user = GetLoginUser();
                ViewBag.User = user;

                BLL.Lover bllLover = new BLL.Lover();
                Model.Lover lover = bllLover.GetModel((int)loverID);
                if (lover == null)
                {
                    return JumpToHome("对不起，操作错误！", "您访问的页面不存在！");
                }
                if (lover.BoyUserID != user.UserID && lover.GirlUserID != user.UserID)
                {
                    return JumpToHome("对不起，操作错误！", "您无权限访问和操作！");
                }
                if (lover.ApplyUserID != user.UserID && lover.State == 0 && lover.Flow == (int)Model.LoverFlow.Apply)
                {
                    //如果还生效
                }
                else
                {
                    return JumpToHome("对不起，操作错误！", "您访问页面已失效！");
                }

                lover.State = -1;   //注销
                lover.Flow = (int)Model.LoverFlow.UnAccept;
                lover.UpdateUserID = user.UserID;
                lover.UpdateTime = DateTime.Now;
                bllLover.Update(lover);
                return JumpToHome("操作成功！", "您好，您的操作已经成功！您拒绝了对方！");
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("Lover/UnAccept出现异常：", ex);
                return JumpToHome("对不起，操作错误！", "您访问的页面出现点异常！");
            }
        }


        /// <summary>
        /// 详细
        /// </summary>
        /// <returns></returns>
        [CheckAuthFilter]
        public ActionResult Preview(int? loverID)
        {
            if (loverID == null)
            {
                return JumpToHome("对不起，操作错误！", "您访问的页面不存在！");
            }
            try
            {
                Model.User user = GetLoginUser();
                ViewBag.User = user;

                Model.Lover lover = new BLL.Lover().GetModel((int)loverID);
                if (lover == null)
                {
                    return JumpToHome("对不起，操作错误！", "您访问的申请不存在！");
                }
                if (lover.BoyUserID != user.UserID && lover.GirlUserID != user.UserID)     
                {
                    return JumpToHome("对不起", "您访问无权访问！");
                }
                ViewBag.Lover = lover;
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("Lover/UnAccept出现异常：", ex);
                return JumpToHome("对不起，操作错误！", "您访问的页面出现点异常！");
            }
            return View();
        }


        /// <summary>
        /// 证书
        /// </summary>
        /// <param name="loverID"></param>
        /// <returns></returns>
        public ActionResult Certificate(int? loverID)
        {
            if (loverID == null)
            {
                return JumpToHome("对不起，操作错误！", "您访问的页面不存在！");
            }
            try
            {
                Model.User user = GetLoginUser();
                ViewBag.User = user;

                Model.Lover lover = new BLL.Lover().GetModel((int)loverID);
                if (lover == null)
                {
                    return JumpToHome("对不起，操作错误！", "您访问的页面不存在！");
                }
                ViewBag.Lover = lover;
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("Lover/UnAccept出现异常：", ex);
                return JumpToHome("对不起，操作错误！", "您访问的页面出现点异常！");
            }
            return View();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="loverID"></param>
        /// <returns></returns>
        [CheckAuthFilter]
        public ActionResult Abolish(int? loverID)
        {
            try
            {
                if (loverID == null)
                {
                    return JumpToHome("对不起，操作错误！", "您访问的操作页面不存在！");
                }

                Model.User user = GetLoginUser();
                ViewBag.User = user;

                Model.Lover lover = new BLL.Lover().GetModel((int)loverID);
                if (lover == null)
                {
                    return JumpToHome("对不起，操作错误！", "您访问的页面不存在或已经失效！");
                }
                if (lover.BoyUserID != user.UserID && lover.GirlUserID != user.UserID)
                {
                    return JumpToHome("对不起，操作错误！", "对不起，该记录不属于您的，您无权操作！");
                }
                ViewBag.Lover = lover;
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("Lover/UnAccept出现异常：", ex);
                return JumpToHome("对不起，操作错误！", "您访问的页面出现点异常！");
            }
            return View();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="loverID"></param>
        /// <returns></returns>
        [CheckAuthFilter]
        [HttpPost]
        public ActionResult Abolish(int? loverID, string why)
        {
            try
            {
                if (loverID == null)
                {
                    JumpToHome("对不起，操作错误！", "您访问的操作页面不存在！");
                }

                Model.User user = GetLoginUser();
                ViewBag.User = user;

                BLL.Lover bllLover = new BLL.Lover();
                Model.Lover lover = bllLover.GetModel((int)loverID);
                if (lover == null)
                {
                    return JumpToHome("对不起，操作错误！", "您访问的页面不存在或已经失效！");
                }
                if (lover.BoyUserID != user.UserID && lover.GirlUserID != user.UserID)
                {
                    return JumpToHome("对不起，操作错误！", "对不起，您无权访问和操作！");
                }

                lover.State = -1;   //注销
                lover.Flow = (int)Model.LoverFlow.Abolish;
                lover.UpdateUserID = user.UserID;
                lover.UpdateTime = DateTime.Now;
                bllLover.Update(lover);
                return JumpToHome("操作成功！", "您好，您的操作已经成功！您已经解除了关系！");
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("Lover/Abolish出现异常：", ex);
                return JumpToHome("对不起，操作错误！", "您访问的页面出现点异常！");
            }
        }

    }


}
