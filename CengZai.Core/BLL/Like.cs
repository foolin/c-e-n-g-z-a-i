using System;
using System.Data;
using System.Collections.Generic;
using CengZai.Helper;
using CengZai.Model;
namespace CengZai.BLL
{
	/// <summary>
	/// Like
	/// </summary>
	public partial class Like
	{
		private readonly CengZai.DAL.Like dal=new CengZai.DAL.Like();
		public Like()
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
		public bool Exists(int LikeID)
		{
			return dal.Exists(LikeID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(CengZai.Model.Like model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.Like model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int LikeID)
		{
			
			return dal.Delete(LikeID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string LikeIDlist )
		{
			return dal.DeleteList(LikeIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CengZai.Model.Like GetModel(int LikeID)
		{
			
			return dal.GetModel(LikeID);
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
		public List<CengZai.Model.Like> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CengZai.Model.Like> DataTableToList(DataTable dt)
		{
			List<CengZai.Model.Like> modelList = new List<CengZai.Model.Like>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CengZai.Model.Like model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CengZai.Model.Like();
					if(dt.Rows[n]["LikeID"]!=null && dt.Rows[n]["LikeID"].ToString()!="")
					{
						model.LikeID=int.Parse(dt.Rows[n]["LikeID"].ToString());
					}
					if(dt.Rows[n]["LikeUserID"]!=null && dt.Rows[n]["LikeUserID"].ToString()!="")
					{
						model.LikeUserID=int.Parse(dt.Rows[n]["LikeUserID"].ToString());
					}
					if(dt.Rows[n]["LikeArtID"]!=null && dt.Rows[n]["LikeArtID"].ToString()!="")
					{
						model.LikeArtID=int.Parse(dt.Rows[n]["LikeArtID"].ToString());
					}
					if(dt.Rows[n]["UserID"]!=null && dt.Rows[n]["UserID"].ToString()!="")
					{
						model.UserID=int.Parse(dt.Rows[n]["UserID"].ToString());
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
	}
}

