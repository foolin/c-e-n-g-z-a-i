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
        public ActionResult Index(string active)
        {
            try
            {
                Model.User loginUser = GetLoginUser();
                if (loginUser == null)
                {
                    return JumpToLogin();
                }
                DataSet dsArtList = null;   //文章列表
                BLL.Article bllArt = new BLL.Article();
                //取朋友文章
                DataSet dsFriendUserList = null;
                string whereSql = "Privacy=0 And State=1";
                if (active == "follow")
                {
                    dsFriendUserList = new BLL.Friend().GetFriendUserList(loginUser.UserID, Model.FriendRelation.Follow, 0, "");
                }
                else if (active == "fans")
                {
                    dsFriendUserList = new BLL.Friend().GetFriendUserList(loginUser.UserID, Model.FriendRelation.Fans, 0, "");
                }
                else if (active == "friend")
                {
                    dsFriendUserList = new BLL.Friend().GetFriendUserList(loginUser.UserID, Model.FriendRelation.Friend, 0, "");
                }
                if (dsFriendUserList != null && dsFriendUserList.Tables.Count > 0 && dsFriendUserList.Tables[0].Rows.Count > 0)
                {
                    string userIds = loginUser.UserID.ToString();
                    foreach (DataRow row in dsFriendUserList.Tables[0].Rows)
                    {
                        userIds += "," + row["UserID"];
                    }
                    whereSql += " and UserID in (" + userIds + ") ";
                }
                dsArtList = bllArt.GetListByPage(whereSql, "ArtID DESC", mPageSize, mPageIndex, out mTotalCount);
                List<Model.Article> artList = null;
                if (dsArtList != null && dsArtList.Tables.Count > 0)
                {
                    artList = bllArt.DataTableToList(dsArtList.Tables[0]);
                }
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
