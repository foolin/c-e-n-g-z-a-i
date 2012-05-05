using System;
using System.Data;
using System.Collections.Generic;
using CengZai.Helper;
using CengZai.Model;
namespace CengZai.BLL
{
	/// <summary>
	/// Category
	/// </summary>
	public partial class Category
	{
		private readonly CengZai.DAL.Category dal=new CengZai.DAL.Category();
		public Category()
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
		public bool Exists(int CategoryID)
		{
			return dal.Exists(CategoryID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(CengZai.Model.Category model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.Category model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int CategoryID)
		{
			
			return dal.Delete(CategoryID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string CategoryIDlist )
		{
			return dal.DeleteList(CategoryIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CengZai.Model.Category GetModel(int CategoryID)
		{
			
			return dal.GetModel(CategoryID);
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
		public List<CengZai.Model.Category> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CengZai.Model.Category> DataTableToList(DataTable dt)
		{
			List<CengZai.Model.Category> modelList = new List<CengZai.Model.Category>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CengZai.Model.Category model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CengZai.Model.Category();
					if(dt.Rows[n]["CategoryID"]!=null && dt.Rows[n]["CategoryID"].ToString()!="")
					{
						model.CategoryID=int.Parse(dt.Rows[n]["CategoryID"].ToString());
					}
					if(dt.Rows[n]["CategoryName"]!=null && dt.Rows[n]["CategoryName"].ToString()!="")
					{
					model.CategoryName=dt.Rows[n]["CategoryName"].ToString();
					}
					if(dt.Rows[n]["CategoryDesc"]!=null && dt.Rows[n]["CategoryDesc"].ToString()!="")
					{
					model.CategoryDesc=dt.Rows[n]["CategoryDesc"].ToString();
					}
					if(dt.Rows[n]["UserID"]!=null && dt.Rows[n]["UserID"].ToString()!="")
					{
						model.UserID=int.Parse(dt.Rows[n]["UserID"].ToString());
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

