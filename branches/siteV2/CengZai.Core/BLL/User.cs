using System;
using System.Data;
using System.Collections.Generic;
using CengZai.Helper;
using CengZai.Model;
using System.Threading;
namespace CengZai.BLL
{
	/// <summary>
	/// User
	/// </summary>
	public partial class User
	{
		private readonly CengZai.DAL.User dal=new CengZai.DAL.User();
		public User()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int UserID)
		{
			return dal.Exists(UserID);
		}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string email)
        {
            return dal.Exists(email);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(CengZai.Model.User model)
		{
			int ret =  dal.Add(model);
            //启动现场更新缓存
            Thread thread = new Thread(delegate()
            {
                UpdateCache();
            });
            thread.Start();
            return ret;
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.User model)
		{
			bool ret =  dal.Update(model);
            Thread thread = new Thread(delegate()
            {
                UpdateCache();
            });
            thread.Start();
            return ret;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int UserID)
		{
			bool ret =  dal.Delete(UserID);
            Thread thread = new Thread(delegate()
            {
                UpdateCache();
            });
            thread.Start();
            return ret;
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string UserIDlist )
		{
			bool ret =  dal.DeleteList(UserIDlist );
            Thread thread = new Thread(delegate()
            {
                UpdateCache();
            });
            thread.Start();
            return ret;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CengZai.Model.User GetModel(int UserID)
		{
            return dal.GetModelByUserIDOrEmail(UserID, "");
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CengZai.Model.User GetModel(string email)
        {
            return dal.GetModelByUserIDOrEmail(0, email);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CengZai.Model.User GetModelByUserName(string username)
        {
            return dal.GetModelByUsername(username);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CengZai.Model.User GetModelByOpenId(string openId, LoginType loginType)
        {
            return dal.GetModelByOpenId(openId, loginType);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CengZai.Model.User> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CengZai.Model.User> DataTableToList(DataTable dt)
		{
			List<CengZai.Model.User> modelList = new List<CengZai.Model.User>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int n = 0; n < rowsCount; n++)
				{
					modelList.Add(dal.RowToModel(dt.Rows[n]));
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method


        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetListByCache(bool isUpdateCache)
        {
            DataSet dsList = null;
            string cacheKey = Config.CacheKeyPrefix + "USER_LIST";
            if (!isUpdateCache && System.Web.HttpRuntime.Cache[cacheKey] != null)
            {
                dsList = System.Web.HttpRuntime.Cache[cacheKey] as DataSet;
            }
            else
            {
                dsList = GetList("");
                if (dsList != null)
                {
                    System.Web.HttpRuntime.Cache.Insert(cacheKey, dsList, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
                }
            }
            return dsList;
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <returns></returns>
        public bool UpdateCache()
        {
            DataSet dsList = null;
            try
            {
                dsList = GetListByCache(true);
            }
            catch (Exception ex)
            {
                Log.Error("BLL.User.Update()异常", ex);
            }
            return dsList != null;
        }

        /// <summary>
        /// 取缓存
        /// </summary>
        /// <returns></returns>
        public DataSet GetListByCache()
        {
            return GetListByCache(false);
        }

        /// <summary>
        /// 取用户名
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Model.User GetModelByCache(int userID)
        {
            Model.User model = null;
            DataSet dsUserList = new CengZai.BLL.User().GetListByCache();
            if (dsUserList != null)
            {
                DataRow[] rows = dsUserList.Tables[0].Select("UserID=" + userID);
                if (rows.Length > 0)
                {
                    model = dal.RowToModel(rows[0]);
                }
            }
            //如果不存在用户，则取最新的且更新缓存
            if (model == null)
            {
                dsUserList = new CengZai.BLL.User().GetListByCache(true);
                if (dsUserList != null)
                {
                    DataRow[] rows = dsUserList.Tables[0].Select("UserID=" + userID);
                    if (rows.Length > 0)
                    {
                        model = dal.RowToModel(rows[0]);
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 取用户名
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Model.User GetModelByCache(string username)
        {
            Model.User model = null;
            DataSet dsUserList = new CengZai.BLL.User().GetListByCache();
            if (dsUserList != null)
            {
                DataRow[] rows = dsUserList.Tables[0].Select("Username='" + username + "'");
                if (rows.Length > 0)
                {
                    model = dal.RowToModel(rows[0]);
                }
            }
            //如果不存在用户，则取最新的且更新缓存
            if (model == null)
            {
                dsUserList = new CengZai.BLL.User().GetListByCache(true);
                if (dsUserList != null)
                {
                    DataRow[] rows = dsUserList.Tables[0].Select("Username='" + username + "'");
                    if (rows.Length > 0)
                    {
                        model = dal.RowToModel(rows[0]);
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="top"></param>
        /// <param name="keyword"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public DataSet GetListBySearch(int top, string keyword, string filedOrder)
        {
            return dal.GetListBySearch(top, keyword, filedOrder);
        }



        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="fieldOrder"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetListByPage(string strWhere, string fieldOrder, int pageSize, int pageIndex, out int totalCount)
        {
            return dal.GetListByPage(strWhere, fieldOrder, pageSize, pageIndex, out totalCount);
        }

        /// <summary>
        /// 取朋友关系
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="type">-1=黑名单, 0=我关注的人,1=关注我的人</param>
        /// <param name="fieldOrder"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetFriendListByPage(bool isWhereFriendIDField, int whereUserID, int type, string fieldOrder, int pageSize, int pageIndex, out int totalCount)
        {
            return dal.GetFriendListByPage(isWhereFriendIDField, whereUserID, type, fieldOrder, pageSize, pageIndex, out totalCount);
        }

        /// <summary>
        /// 取性别
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        public string GetSexName(int? sex)
        {
            if (sex == 2)
            {
                return "女生";
            }
            else if(sex == 1)
            {
                return "男生";
            }
            else
            {
                return "保密";
            }
        }


        /// <summary>
        /// 取性别
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        public static string GetLoginTypeName(LoginType loginType)
        {
            if (loginType ==  LoginType.QQ)
            {
                return "腾讯QQ";
            }
            else if (loginType == LoginType.Sina)
            {
                return "新浪微博";
            }
            else if (loginType == LoginType.Renren)
            {
                return "人人网";
            }
            else if (loginType == LoginType.Douban)
            {
                return "豆瓣网";
            }
            else if (loginType == LoginType.System)
            {
                return Config.SiteName;
            }
            else
            {
                return "未知";
            }
        }
	}
}

