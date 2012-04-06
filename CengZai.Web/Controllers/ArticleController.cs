using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai;
using System.Data;
using System.IO;
using CengZai.Helper;
using CengZai.Web.Common;

namespace CengZai.Web.Controllers
{
    public class ArticleController : BaseController
    {
        //
        // GET: /Article/
        [CheckAuthFilter]
        public ActionResult Index(string active)
        {
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return JumpToLogin();
                }
                string strWhere = "UserID=" + loginUser.UserID;
                if (active == "draft")
                {
                    strWhere += " and State=0 ";
                }
                else if (active == "public")
                {
                    strWhere += " and State=1 and privacy=0 ";
                }
                else if (active == "friend")
                {
                    strWhere += " and State=1 and privacy=1 ";
                }
                else if (active == "privacy")
                {
                    strWhere += " and State=1 and privacy=2 ";
                }
                BLL.Article bll = new BLL.Article();
                DataSet dsList = bll.GetListByPage(strWhere, "PostTime DESC", mPageSize, mPageIndex, out mTotalCount);
                SetPage();  //设置分页
                if (dsList != null && dsList.Tables.Count > 0)
                {
                    ViewBag.ArtList = bll.DataTableToList(dsList.Tables[0]);
                }
                return View();
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("获取用户文章列表异常", ex);
                return JumpToTips("无法获取文章列表", "糟糕！无法获取日志列表，请稍后重试！");
            }
        }

        [CheckAuthFilter]
        public ActionResult PostText(int? artid)
        {
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return JumpToLogin();
                }
                Model.Article mArticle = null;
                if (artid != null)
                {
                    mArticle = new BLL.Article().GetModel((int)artid);
                    if (mArticle == null)
                    {
                        return JumpToHome("操作错误！", "对不起，该文章已被删除或者不存在！");
                    }
                    if (mArticle.UserID != loginUser.UserID)
                    {
                        JumpToHome("操作错误！", "对不起，您无权限操作或者文章不存在！");
                    }
                    ViewBag.Article = mArticle;
                }

                List<Model.Category> categories = new BLL.Category().GetModelList(string.Format("UserID={0}", GetLoginUser().UserID));
                if (categories == null)
                {
                    ViewBag.CategoryList = new List<SelectListItem>(){
                        new SelectListItem(){ Text = "默认分类", Value = "0"}
                    };
                }
                else
                {
                    if (mArticle == null)
                    {
                        ViewBag.CategoryList = new SelectList(categories, "CategoryID", "CategoryName");
                    }
                    else
                    {
                        ViewBag.CategoryList = new SelectList(categories, "CategoryID", "CategoryName", mArticle.CategoryID);
                    }
                }
                //隐私设置
                var privateList = new List<Object>(){
                    new{ Text = "所有人可见", Value="0"},
                    new{ Text = "仅好友可见", Value="1"},
                    new{ Text = "仅自己可见", Value="2"},
                };
                if (mArticle == null)
                {
                    ViewBag.PrivateList = new SelectList(privateList, "Value", "Text");
                }
                else
                {
                    ViewBag.PrivateList = new SelectList(privateList, "Value", "Text", mArticle.Privacy);
                }

                return View();
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("ArticleController.PostText异常", ex);
                return JumpToTips("初始化提交文章表单出错", "亲，初始化提交文章表单出错了，请检查输入或者联系客服！");
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AjaxPostText(int? artid, string title, string content, int? categoryid, int? privacy, int? top, int? draft)
        {
            try
            {
                Model.User loginUser =  GetLoginUser();
                if(loginUser == null)
                {
                    return AjaxReturn("error","您尚未登录或者登录超时！");
                }
                if (string.IsNullOrEmpty(content))
                {
                    return AjaxReturn("error", "内容不能为空！");
                }
                if (!string.IsNullOrEmpty(title) && title.Length > 50)
                {
                    return AjaxReturn("error", "标题不能超过50个字符！");
                }
                //文章处理
                bool isNewArticle = true;
                BLL.Article bll = new BLL.Article();
                Model.Article model = null;
                if (artid != null && artid > 0)
                {
                    model = bll.GetModel((int)artid);
                    if (model != null || model.UserID == loginUser.UserID)
                    {
                        isNewArticle = false;
                    }
                }
                if(isNewArticle)
                {
                    model = new Model.Article();
                }
                
                model.CategoryID = (categoryid == null ? 0 : categoryid);
                model.Title = Server.HtmlEncode(title); //过滤html代码
                model.Content = Helper.Util.GetSafeHtml(content);
                model.IsTop = (top == null ? 0 : top);
                model.PostIP = Helper.Util.GetIP();
                if (isNewArticle || model.PostTime== null)
                {
                    model.PostTime = DateTime.Now;
                }
                model.Privacy = (privacy == null ? 0 : privacy);
                model.ReplyCount = 0;
                model.ReportCount = 0;
                if (draft == 1)
                {
                    model.State = 0;
                }
                else
                {
                    model.State = 1;
                }
                
                model.Type = 0;
                model.UserID = loginUser.UserID;
                model.ViewCount = 0;

                if (isNewArticle)
                {
                    model.ArtID = bll.Add(model);
                }
                else
                {
                    bll.Update(model);
                }

                return AjaxReturn("success", "恭喜，文章保存成功！");
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo(GetLoginUser().Email + "提交文章出现异常：" + ex.Message + ",详细：" + ex.StackTrace);
                return AjaxReturn("error", "对不起，文章保存失败！出现了异常");
            }
        }

        //删除日志
        [CheckAuthFilter]
        [HttpPost]
        public ActionResult Delete(int? artid)
        {
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return JumpToLogin();
                }
                if (artid == null)
                {
                    return JumpBackAndRefresh("删除失败！", "对不起，操作错误！");
                }
                BLL.Article bllArt= new BLL.Article();
                Model.Article mArt = bllArt.GetModel((int)artid);
                if (mArt == null)
                {
                    return JumpBackAndRefresh("删除失败！", "对不起，日志不存在或者已被删除！");
                }
                if (mArt.UserID != loginUser.UserID)
                {
                    return JumpBackAndRefresh("删除失败！", "对不起，你无权限操作或者日志不存在！");
                }
                bllArt.Delete(mArt.ArtID);
                //******* 删除附件 **********//
                //尚未开发
                //******* 删除附件 **********//
                return JumpBackAndRefresh("删除成功", "删除日志成功！");
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("删除文章出现异常", ex);
                return JumpToTips("删除失败！", "删除出现错误，请稍后重试！");
            }
        }



        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ActionResult AjaxCategoryAdd(string categoryName, string categoryDesc, int? categoryID)
        {
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return AjaxReturn("error", "您尚未登录或者登录超时");
                }

                categoryName = (categoryName + "").Trim();
                if (string.IsNullOrEmpty(categoryName) || categoryName.Length > 20)
                {
                    return AjaxReturn("error", "分类为空或者超过20个字符");
                }
                if (Util.CheckBadWord(categoryName))
                {
                    return AjaxReturn("error", "分类含有非法字符");
                }
                if (!string.IsNullOrEmpty(categoryDesc) && categoryDesc.Length > 300)
                {
                    return AjaxReturn("error", "分类描述不可以大于300个字符");
                }
                
                BLL.Category bCategory = new BLL.Category();
                DataSet dsExistList = bCategory.GetList("UserID=" + loginUser.UserID);
                if (dsExistList != null && dsExistList.Tables.Count > 0)
                {
                    DataRow[] rows = dsExistList.Tables[0].Select("CategoryName='"+ categoryName +"'");
                    if (rows.Length > 0)
                    {
                        return AjaxReturn("error", "已经存在该广告分类");
                    }
                }
                Model.Category mCategory = null;
                if (categoryID != null)
                {
                    mCategory = bCategory.GetModel((int)categoryID);
                }
                if (mCategory == null || mCategory.UserID != loginUser.UserID)
                {
                    //如果分类为空或者用户分类权限不同
                    mCategory = new Model.Category();
                }
                mCategory.CategoryDesc = categoryDesc;
                mCategory.CategoryName = categoryName;
                mCategory.UserID = loginUser.UserID;
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                if (mCategory.CategoryID == categoryID)
                {
                    bCategory.Update(mCategory);
                    return AjaxReturn("success", jss.Serialize(mCategory));
                }
                else
                {
                    mCategory.CategoryID = bCategory.Add(mCategory);
                    return AjaxReturn("success", jss.Serialize(mCategory));
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("新建广告分类异常", ex);
                return AjaxReturn("error", "");
            }
        }


        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ActionResult AjaxCategoryDel(int? categoryID)
        {
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return AjaxReturn("error", "您尚未登录或者登录超时");
                }

                if (categoryID == null || categoryID <= 0)
                {
                    return AjaxReturn("error", "参数错误");
                }

                BLL.Category bCategory = new BLL.Category();
                Model.Category mCategory = bCategory.GetModel((int)categoryID);
                if (mCategory == null)
                {
                    return AjaxReturn("error", "分类不存在！");
                }
                if (mCategory.UserID != loginUser.UserID)
                {
                    return AjaxReturn("error", "删除分类不存在！");
                }
                bCategory.Delete(mCategory.CategoryID);
                return AjaxReturn("success", mCategory.CategoryID.ToString());
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo("新建广告分类异常", ex);
                return AjaxReturn("error", "");
            }
        }


    }
}
