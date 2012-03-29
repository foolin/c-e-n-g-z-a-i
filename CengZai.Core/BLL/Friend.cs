using System;
using System.Data;
using System.Collections.Generic;
using CengZai.Helper;
using CengZai.Model;
namespace CengZai.BLL
{
	/// <summary>
	/// Friend
	/// </summary>
	public partial class Friend
	{
		private readonly CengZai.DAL.Friend dal=new CengZai.DAL.Friend();
		public Friend()
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
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(CengZai.Model.Friend model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.Friend model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CengZai.Model.Friend GetModel(int ID)
		{
			
			return dal.GetModel(ID);
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
		public List<CengZai.Model.Friend> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CengZai.Model.Friend> DataTableToList(DataTable dt)
		{
			List<CengZai.Model.Friend> modelList = new List<CengZai.Model.Friend>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CengZai.Model.Friend model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CengZai.Model.Friend();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["UserID"]!=null && dt.Rows[n]["UserID"].ToString()!="")
					{
						model.UserID=int.Parse(dt.Rows[n]["UserID"].ToString());
					}
					if(dt.Rows[n]["FriendUserID"]!=null && dt.Rows[n]["FriendUserID"].ToString()!="")
					{
						model.FriendUserID=int.Parse(dt.Rows[n]["FriendUserID"].ToString());
					}
					if(dt.Rows[n]["Type"]!=null && dt.Rows[n]["Type"].ToString()!="")
					{
						model.Type=int.Parse(dt.Rows[n]["Type"].ToString());
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


        /// <summary>
        /// 添加关注
        /// </summary>
        /// <param name="userID">我的ID</param>
        /// <param name="friendUserID">对方ID</param>
        public bool Add(int userID, int friendUserID)
        {
            if (userID == friendUserID)
            {
                return false;
            }
            Model.Friend model = GetModel(userID, friendUserID);
            if (model != null)
            {
                return true;
            }
            model = new Model.Friend();
            model.UserID = userID;
            model.FriendUserID = friendUserID;
            model.Type = 0;
            return Add(model) > 0;
        }

        /// <summary>
        /// 取关注
        /// </summary>
        /// <param name="userID">我的ID</param>
        /// <param name="friendUserID">对方ID</param>
        public Model.Friend GetModel(int userID, int friendUserID)
        {
            List<Model.Friend> list = GetModelList("UserID=" + userID + " and FriendUserID=" + friendUserID);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            return list[0];
        }


        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="userID">我的ID</param>
        /// <param name="friendUserID">对方ID</param>
        /// <param name="type">0=关注，1=朋友，-1=黑名单</param>
        /// <returns></returns>
        public bool Update(int userID, int friendUserID, int type)
        {
            Model.Friend model = GetModel(userID, friendUserID);
            if (model == null)
            {
                return false;
            }
            model.Type = type;
            return Update(model);
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="friendUserID"></param>
        /// <returns></returns>
        public bool Delete(int userID, int friendUserID)
        {
            Model.Friend model = GetModel(userID, friendUserID);
            if (model == null)
            {
                return true;
            }
            return Delete(model.ID);
        }



        /// <summary>
        /// 返回用户数据列表
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="relation"></param>
        /// <param name="fieldOrder"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetFriendUserListByPage(int userID, FriendRelation relation, string fieldOrder, int pageSize, int pageIndex, out int totalCount)
        {
            return dal.GetFriendUserListByPage(userID, relation, fieldOrder, pageSize, pageIndex, out totalCount);
        }

	}
}

