using System;
using System.Data;
using System.Collections.Generic;
using CengZai.Helper;
using CengZai.Model;
namespace CengZai.BLL
{
	/// <summary>
	/// Attachment
	/// </summary>
	public partial class Attachment
	{
		private readonly CengZai.DAL.Attachment dal=new CengZai.DAL.Attachment();
		public Attachment()
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
		public bool Exists(int AttachID)
		{
			return dal.Exists(AttachID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(CengZai.Model.Attachment model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.Attachment model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int AttachID)
		{
			
			return dal.Delete(AttachID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string AttachIDlist )
		{
			return dal.DeleteList(AttachIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CengZai.Model.Attachment GetModel(int AttachID)
		{
			
			return dal.GetModel(AttachID);
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
		public List<CengZai.Model.Attachment> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CengZai.Model.Attachment> DataTableToList(DataTable dt)
		{
			List<CengZai.Model.Attachment> modelList = new List<CengZai.Model.Attachment>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CengZai.Model.Attachment model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CengZai.Model.Attachment();
					if(dt.Rows[n]["AttachID"]!=null && dt.Rows[n]["AttachID"].ToString()!="")
					{
						model.AttachID=int.Parse(dt.Rows[n]["AttachID"].ToString());
					}
					if(dt.Rows[n]["Source"]!=null && dt.Rows[n]["Source"].ToString()!="")
					{
						model.Source=int.Parse(dt.Rows[n]["Source"].ToString());
					}
					if(dt.Rows[n]["SourceID"]!=null && dt.Rows[n]["SourceID"].ToString()!="")
					{
						model.SourceID=int.Parse(dt.Rows[n]["SourceID"].ToString());
					}
					if(dt.Rows[n]["FilePath"]!=null && dt.Rows[n]["FilePath"].ToString()!="")
					{
					model.FilePath=dt.Rows[n]["FilePath"].ToString();
					}
					if(dt.Rows[n]["FileExt"]!=null && dt.Rows[n]["FileExt"].ToString()!="")
					{
					model.FileExt=dt.Rows[n]["FileExt"].ToString();
					}
					if(dt.Rows[n]["FieSize"]!=null && dt.Rows[n]["FieSize"].ToString()!="")
					{
						model.FieSize=int.Parse(dt.Rows[n]["FieSize"].ToString());
					}
					if(dt.Rows[n]["FileData"]!=null && dt.Rows[n]["FileData"].ToString()!="")
					{
					model.FileData=dt.Rows[n]["FileData"].ToString();
					}
					if(dt.Rows[n]["UserID"]!=null && dt.Rows[n]["UserID"].ToString()!="")
					{
						model.UserID=int.Parse(dt.Rows[n]["UserID"].ToString());
					}
					if(dt.Rows[n]["UploadTime"]!=null && dt.Rows[n]["UploadTime"].ToString()!="")
					{
						model.UploadTime=DateTime.Parse(dt.Rows[n]["UploadTime"].ToString());
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

