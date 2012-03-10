using System;
using System.Data;
using System.Collections.Generic;
using CengZai.Helper;
using CengZai.Model;
namespace CengZai.BLL
{
	/// <summary>
	/// LoverComment
	/// </summary>
	public partial class LoverComment
	{
		private readonly CengZai.DAL.LoverComment dal=new CengZai.DAL.LoverComment();
		public LoverComment()
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
		public bool Exists(int CommentID)
		{
			return dal.Exists(CommentID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CengZai.Model.LoverComment model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.LoverComment model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int CommentID)
		{
			
			return dal.Delete(CommentID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string CommentIDlist )
		{
			return dal.DeleteList(CommentIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CengZai.Model.LoverComment GetModel(int CommentID)
		{
			
			return dal.GetModel(CommentID);
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
		public List<CengZai.Model.LoverComment> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CengZai.Model.LoverComment> DataTableToList(DataTable dt)
		{
			List<CengZai.Model.LoverComment> modelList = new List<CengZai.Model.LoverComment>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CengZai.Model.LoverComment model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CengZai.Model.LoverComment();
					if(dt.Rows[n]["CommentID"]!=null && dt.Rows[n]["CommentID"].ToString()!="")
					{
						model.CommentID=int.Parse(dt.Rows[n]["CommentID"].ToString());
					}
					if(dt.Rows[n]["LoverID"]!=null && dt.Rows[n]["LoverID"].ToString()!="")
					{
						model.LoverID=int.Parse(dt.Rows[n]["LoverID"].ToString());
					}
					if(dt.Rows[n]["Title"]!=null && dt.Rows[n]["Title"].ToString()!="")
					{
					model.Title=dt.Rows[n]["Title"].ToString();
					}
					if(dt.Rows[n]["Content"]!=null && dt.Rows[n]["Content"].ToString()!="")
					{
					model.Content=dt.Rows[n]["Content"].ToString();
					}
					if(dt.Rows[n]["UserID"]!=null && dt.Rows[n]["UserID"].ToString()!="")
					{
						model.UserID=int.Parse(dt.Rows[n]["UserID"].ToString());
					}
					if(dt.Rows[n]["PostTime"]!=null && dt.Rows[n]["PostTime"].ToString()!="")
					{
						model.PostTime=DateTime.Parse(dt.Rows[n]["PostTime"].ToString());
					}
					if(dt.Rows[n]["PostIP"]!=null && dt.Rows[n]["PostIP"].ToString()!="")
					{
					model.PostIP=dt.Rows[n]["PostIP"].ToString();
					}
					if(dt.Rows[n]["ParentID"]!=null && dt.Rows[n]["ParentID"].ToString()!="")
					{
						model.ParentID=int.Parse(dt.Rows[n]["ParentID"].ToString());
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

