using System;
using System.Data;
using System.Collections.Generic;
using CengZai.Helper;
using CengZai.Model;
namespace CengZai.BLL
{
	/// <summary>
	/// Article
	/// </summary>
	public partial class Article
	{
		private readonly CengZai.DAL.Article dal=new CengZai.DAL.Article();
		public Article()
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
		public bool Exists(int ArtID)
		{
			return dal.Exists(ArtID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(CengZai.Model.Article model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.Article model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ArtID)
		{
			
			return dal.Delete(ArtID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ArtIDlist )
		{
			return dal.DeleteList(ArtIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CengZai.Model.Article GetModel(int ArtID)
		{
			
			return dal.GetModel(ArtID);
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
		public List<CengZai.Model.Article> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CengZai.Model.Article> DataTableToList(DataTable dt)
		{
			List<CengZai.Model.Article> modelList = new List<CengZai.Model.Article>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CengZai.Model.Article model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CengZai.Model.Article();
					if(dt.Rows[n]["ArtID"]!=null && dt.Rows[n]["ArtID"].ToString()!="")
					{
						model.ArtID=int.Parse(dt.Rows[n]["ArtID"].ToString());
					}
					if(dt.Rows[n]["CategoryID"]!=null && dt.Rows[n]["CategoryID"].ToString()!="")
					{
						model.CategoryID=int.Parse(dt.Rows[n]["CategoryID"].ToString());
					}
					if(dt.Rows[n]["Title"]!=null && dt.Rows[n]["Title"].ToString()!="")
					{
					model.Title=dt.Rows[n]["Title"].ToString();
					}
					if(dt.Rows[n]["Content"]!=null && dt.Rows[n]["Content"].ToString()!="")
					{
					model.Content=dt.Rows[n]["Content"].ToString();
					}
					if(dt.Rows[n]["Type"]!=null && dt.Rows[n]["Type"].ToString()!="")
					{
						model.Type=int.Parse(dt.Rows[n]["Type"].ToString());
					}
					if(dt.Rows[n]["IsTop"]!=null && dt.Rows[n]["IsTop"].ToString()!="")
					{
						model.IsTop=int.Parse(dt.Rows[n]["IsTop"].ToString());
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
					if(dt.Rows[n]["ViewCount"]!=null && dt.Rows[n]["ViewCount"].ToString()!="")
					{
						model.ViewCount=int.Parse(dt.Rows[n]["ViewCount"].ToString());
					}
					if(dt.Rows[n]["ReplyCount"]!=null && dt.Rows[n]["ReplyCount"].ToString()!="")
					{
						model.ReplyCount=int.Parse(dt.Rows[n]["ReplyCount"].ToString());
					}
					if(dt.Rows[n]["ReportCount"]!=null && dt.Rows[n]["ReportCount"].ToString()!="")
					{
						model.ReportCount=int.Parse(dt.Rows[n]["ReportCount"].ToString());
					}
					if(dt.Rows[n]["Privacy"]!=null && dt.Rows[n]["Privacy"].ToString()!="")
					{
						model.Privacy=int.Parse(dt.Rows[n]["Privacy"].ToString());
					}
					if(dt.Rows[n]["State"]!=null && dt.Rows[n]["State"].ToString()!="")
					{
						model.State=int.Parse(dt.Rows[n]["State"].ToString());
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

