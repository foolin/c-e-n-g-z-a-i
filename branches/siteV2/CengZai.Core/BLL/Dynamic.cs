using System;
using System.Data;
using System.Collections.Generic;
using CengZai.Helper;
using CengZai.Model;
namespace CengZai.BLL
{
	/// <summary>
	/// Dynamic
	/// </summary>
	public partial class Dynamic
	{
		private readonly CengZai.DAL.Dynamic dal=new CengZai.DAL.Dynamic();
		public Dynamic()
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
		public bool Exists(int DynID)
		{
			return dal.Exists(DynID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(CengZai.Model.Dynamic model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.Dynamic model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int DynID)
		{
			
			return dal.Delete(DynID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string DynIDlist )
		{
			return dal.DeleteList(DynIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CengZai.Model.Dynamic GetModel(int DynID)
		{
			
			return dal.GetModel(DynID);
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
		public List<CengZai.Model.Dynamic> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CengZai.Model.Dynamic> DataTableToList(DataTable dt)
		{
			List<CengZai.Model.Dynamic> modelList = new List<CengZai.Model.Dynamic>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CengZai.Model.Dynamic model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CengZai.Model.Dynamic();
					if(dt.Rows[n]["DynID"]!=null && dt.Rows[n]["DynID"].ToString()!="")
					{
						model.DynID=int.Parse(dt.Rows[n]["DynID"].ToString());
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

