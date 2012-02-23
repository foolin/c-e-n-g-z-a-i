using System;
using System.Data;
using System.Collections.Generic;
using CengZai.Helper;
using CengZai.Model;
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
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.User model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int UserID)
		{
			
			return dal.Delete(UserID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string UserIDlist )
		{
			return dal.DeleteList(UserIDlist );
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
				CengZai.Model.User model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CengZai.Model.User();
                    if (dt.Rows[n]["UserID"] != null && dt.Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(dt.Rows[n]["UserID"].ToString());
                    }
                    if (dt.Rows[n]["Email"] != null && dt.Rows[n]["Email"].ToString() != "")
                    {
                        model.Email = dt.Rows[n]["Email"].ToString();
                    }
                    if (dt.Rows[n]["Password"] != null && dt.Rows[n]["Password"].ToString() != "")
                    {
                        model.Password = dt.Rows[n]["Password"].ToString();
                    }
                    if (dt.Rows[n]["Username"] != null && dt.Rows[n]["Username"].ToString() != "")
                    {
                        model.Username = dt.Rows[n]["Username"].ToString();
                    }
                    if (dt.Rows[n]["Nickname"] != null && dt.Rows[n]["Nickname"].ToString() != "")
                    {
                        model.Nickname = dt.Rows[n]["Nickname"].ToString();
                    }
                    if (dt.Rows[n]["Love"] != null && dt.Rows[n]["Love"].ToString() != "")
                    {
                        model.Love = int.Parse(dt.Rows[n]["Love"].ToString());
                    }
                    if (dt.Rows[n]["Sign"] != null && dt.Rows[n]["Sign"].ToString() != "")
                    {
                        model.Sign = dt.Rows[n]["Sign"].ToString();
                    }
                    if (dt.Rows[n]["Intro"] != null && dt.Rows[n]["Intro"].ToString() != "")
                    {
                        model.Intro = dt.Rows[n]["Intro"].ToString();
                    }
                    if (dt.Rows[n]["Birth"] != null && dt.Rows[n]["Birth"].ToString() != "")
                    {
                        model.Birth = DateTime.Parse(dt.Rows[n]["Birth"].ToString());
                    }
                    if (dt.Rows[n]["Sex"] != null && dt.Rows[n]["Sex"].ToString() != "")
                    {
                        model.Sex = int.Parse(dt.Rows[n]["Sex"].ToString());
                    }
                    if (dt.Rows[n]["AreaID"] != null && dt.Rows[n]["AreaID"].ToString() != "")
                    {
                        model.AreaID = int.Parse(dt.Rows[n]["AreaID"].ToString());
                    }
                    if (dt.Rows[n]["Mobile"] != null && dt.Rows[n]["Mobile"].ToString() != "")
                    {
                        model.Mobile = dt.Rows[n]["Mobile"].ToString();
                    }
                    if (dt.Rows[n]["LoginIp"] != null && dt.Rows[n]["LoginIp"].ToString() != "")
                    {
                        model.LoginIp = dt.Rows[n]["LoginIp"].ToString();
                    }
                    if (dt.Rows[n]["LoginTime"] != null && dt.Rows[n]["LoginTime"].ToString() != "")
                    {
                        model.LoginTime = DateTime.Parse(dt.Rows[n]["LoginTime"].ToString());
                    }
                    if (dt.Rows[n]["LoginCount"] != null && dt.Rows[n]["LoginCount"].ToString() != "")
                    {
                        model.LoginCount = int.Parse(dt.Rows[n]["LoginCount"].ToString());
                    }
                    if (dt.Rows[n]["RegIp"] != null && dt.Rows[n]["RegIp"].ToString() != "")
                    {
                        model.RegIp = dt.Rows[n]["RegIp"].ToString();
                    }
                    if (dt.Rows[n]["RegTime"] != null && dt.Rows[n]["RegTime"].ToString() != "")
                    {
                        model.RegTime = DateTime.Parse(dt.Rows[n]["RegTime"].ToString());
                    }
                    if (dt.Rows[n]["State"] != null && dt.Rows[n]["State"].ToString() != "")
                    {
                        model.State = int.Parse(dt.Rows[n]["State"].ToString());
                    }
                    if (dt.Rows[n]["Privacy"] != null && dt.Rows[n]["Privacy"].ToString() != "")
                    {
                        model.Privacy = int.Parse(dt.Rows[n]["Privacy"].ToString());
                    }
                    if (dt.Rows[n]["Credit"] != null && dt.Rows[n]["Credit"].ToString() != "")
                    {
                        model.Credit = int.Parse(dt.Rows[n]["Credit"].ToString());
                    }
                    if (dt.Rows[n]["Vip"] != null && dt.Rows[n]["Vip"].ToString() != "")
                    {
                        model.Vip = int.Parse(dt.Rows[n]["Vip"].ToString());
                    }
                    if (dt.Rows[n]["Money"] != null && dt.Rows[n]["Money"].ToString() != "")
                    {
                        model.Money = int.Parse(dt.Rows[n]["Money"].ToString());
                    }
                    if (dt.Rows[n]["Config"] != null && dt.Rows[n]["Config"].ToString() != "")
                    {
                        model.Config = dt.Rows[n]["Config"].ToString();
                    }
					modelList.Add(model);
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
	}
}

