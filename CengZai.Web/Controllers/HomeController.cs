using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai.Web.Common;
using System.Data;
using CengZai.Helper;

namespace CengZai.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        [CheckAuthFilter]
        public ActionResult Index()
        {
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return JumpToLogin();
                }
                bool isReadSystem = false;
                DataSet dsArtList = null;   //文章列表
                BLL.Article bllArt = new BLL.Article();
                //取朋友文章
                DataSet dsFollowUserList = new BLL.Friend().GetFriendUserList(loginUser.UserID, Model.FriendRelation.Follow, 0, "");
                if (dsFollowUserList != null && dsFollowUserList.Tables.Count > 0 && dsFollowUserList.Tables[0].Rows.Count > 0)
                {
                    string userIds = loginUser.UserID.ToString();
                    foreach (DataRow row in dsFollowUserList.Tables[0].Rows)
                    {
                        userIds += "," + row["UserID"];
                    }
                    dsArtList = bllArt.GetListByPage("Privacy=0 And State=1 and UserID in (" + userIds + ")", "ArtID DESC", mPageSize, mPageIndex, out mTotalCount);
                }
                if (dsArtList == null || dsArtList.Tables.Count == 0 || dsArtList.Tables[0].Rows.Count == 0)
                {
                    isReadSystem = true;    //读取系统文章
                    dsArtList = bllArt.GetListByPage("Privacy=0 And State=1", "ArtID DESC", mPageSize, mPageIndex, out mTotalCount);
                }
                List<Model.Article> artList = null;
                if (dsArtList != null && dsArtList.Tables.Count > 0)
                {
                    artList = bllArt.DataTableToList(dsArtList.Tables[0]);
                }
                ViewBag.IsReadSystem = isReadSystem;
                ViewBag.ArtList = artList;
                SetPage();
            }
            catch (Exception ex)
            {
                Log.Error("取好友动态数据异常", ex);
            }
            return View();
        }


    }
}
