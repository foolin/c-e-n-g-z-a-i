﻿using System;
using System.Data;
using System.Collections.Generic;
using CengZai.Model;
namespace CengZai.BLL
{
	/// <summary>
	/// Lover
	/// </summary>
	public partial class Lover
	{
		private readonly CengZai.DAL.Lover dal=new CengZai.DAL.Lover();
		public Lover()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int LoverID)
		{
			return dal.Exists(LoverID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(CengZai.Model.Lover model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.Lover model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int LoverID)
		{
			
			return dal.Delete(LoverID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string LoverIDlist )
		{
			return dal.DeleteList(LoverIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CengZai.Model.Lover GetModel(int LoverID)
		{
			
			return dal.GetModel(LoverID);
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
		public List<CengZai.Model.Lover> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CengZai.Model.Lover> DataTableToList(DataTable dt)
		{
			List<CengZai.Model.Lover> modelList = new List<CengZai.Model.Lover>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CengZai.Model.Lover model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CengZai.Model.Lover();
					if(dt.Rows[n]["LoverID"]!=null && dt.Rows[n]["LoverID"].ToString()!="")
					{
						model.LoverID=int.Parse(dt.Rows[n]["LoverID"].ToString());
					}
					if(dt.Rows[n]["Avatar"]!=null && dt.Rows[n]["Avatar"].ToString()!="")
					{
					model.Avatar=dt.Rows[n]["Avatar"].ToString();
					}
					if(dt.Rows[n]["BoyUserID"]!=null && dt.Rows[n]["BoyUserID"].ToString()!="")
					{
						model.BoyUserID=int.Parse(dt.Rows[n]["BoyUserID"].ToString());
					}
					if(dt.Rows[n]["GirlUserID"]!=null && dt.Rows[n]["GirlUserID"].ToString()!="")
					{
						model.GirlUserID=int.Parse(dt.Rows[n]["GirlUserID"].ToString());
					}
					if(dt.Rows[n]["BoyOath"]!=null && dt.Rows[n]["BoyOath"].ToString()!="")
					{
					model.BoyOath=dt.Rows[n]["BoyOath"].ToString();
					}
					if(dt.Rows[n]["GirlOath"]!=null && dt.Rows[n]["GirlOath"].ToString()!="")
					{
					model.GirlOath=dt.Rows[n]["GirlOath"].ToString();
					}
					if(dt.Rows[n]["LoveState"]!=null && dt.Rows[n]["LoveState"].ToString()!="")
					{
						model.LoveState=int.Parse(dt.Rows[n]["LoveState"].ToString());
					}
					if(dt.Rows[n]["MetDate"]!=null && dt.Rows[n]["MetDate"].ToString()!="")
					{
						model.MetDate=DateTime.Parse(dt.Rows[n]["MetDate"].ToString());
					}
					if(dt.Rows[n]["LoveDate"]!=null && dt.Rows[n]["LoveDate"].ToString()!="")
					{
						model.LoveDate=DateTime.Parse(dt.Rows[n]["LoveDate"].ToString());
					}
					if(dt.Rows[n]["MarryDate"]!=null && dt.Rows[n]["MarryDate"].ToString()!="")
					{
						model.MarryDate=DateTime.Parse(dt.Rows[n]["MarryDate"].ToString());
					}
					if(dt.Rows[n]["ApplyUserID"]!=null && dt.Rows[n]["ApplyUserID"].ToString()!="")
					{
						model.ApplyUserID=int.Parse(dt.Rows[n]["ApplyUserID"].ToString());
					}
					if(dt.Rows[n]["ApplyTime"]!=null && dt.Rows[n]["ApplyTime"].ToString()!="")
					{
						model.ApplyTime=DateTime.Parse(dt.Rows[n]["ApplyTime"].ToString());
					}
					if(dt.Rows[n]["IsConfirm"]!=null && dt.Rows[n]["IsConfirm"].ToString()!="")
					{
						model.IsConfirm=int.Parse(dt.Rows[n]["IsConfirm"].ToString());
					}
					if(dt.Rows[n]["ConfirmUserID"]!=null && dt.Rows[n]["ConfirmUserID"].ToString()!="")
					{
						model.ConfirmUserID=int.Parse(dt.Rows[n]["ConfirmUserID"].ToString());
					}
					if(dt.Rows[n]["ConfirmTime"]!=null && dt.Rows[n]["ConfirmTime"].ToString()!="")
					{
						model.ConfirmTime=DateTime.Parse(dt.Rows[n]["ConfirmTime"].ToString());
					}
					if(dt.Rows[n]["CurrState"]!=null && dt.Rows[n]["CurrState"].ToString()!="")
					{
						model.CurrState=int.Parse(dt.Rows[n]["CurrState"].ToString());
					}
					if(dt.Rows[n]["CurrUserID"]!=null && dt.Rows[n]["CurrUserID"].ToString()!="")
					{
						model.CurrUserID=int.Parse(dt.Rows[n]["CurrUserID"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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

