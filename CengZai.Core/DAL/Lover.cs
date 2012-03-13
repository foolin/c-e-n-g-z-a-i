﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CengZai.Helper;
namespace CengZai.DAL
{
	/// <summary>
	/// 数据访问类:Lover
	/// </summary>
	public partial class Lover
	{
		public Lover()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("LoverID", "T_Lover"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int LoverID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Lover");
			strSql.Append(" where LoverID=@LoverID");
			SqlParameter[] parameters = {
					new SqlParameter("@LoverID", SqlDbType.Int,4)
			};
			parameters[0].Value = LoverID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CengZai.Model.Lover model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Lover(");
			strSql.Append("Avatar,BoyUserID,GirlUserID,BoyOath,GirlOath,Certificate,JoinDate,ApplyUserID,ApplyTime,Flow,State)");
			strSql.Append(" values (");
            strSql.Append("@Avatar,@BoyUserID,@GirlUserID,@BoyOath,@GirlOath,@Certificate,@JoinDate,@ApplyUserID,@ApplyTime,@Flow,@State)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Avatar", SqlDbType.NVarChar,500),
					new SqlParameter("@BoyUserID", SqlDbType.Int,4),
					new SqlParameter("@GirlUserID", SqlDbType.Int,4),
					new SqlParameter("@BoyOath", SqlDbType.NVarChar,500),
					new SqlParameter("@GirlOath", SqlDbType.NVarChar,500),
					new SqlParameter("@Certificate", SqlDbType.Int,4),
					new SqlParameter("@JoinDate", SqlDbType.DateTime),
					new SqlParameter("@ApplyUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplyTime", SqlDbType.DateTime),
					new SqlParameter("@Flow", SqlDbType.Int,4),
                    new SqlParameter("@State", SqlDbType.Int,4)};
			parameters[0].Value = model.Avatar;
			parameters[1].Value = model.BoyUserID;
			parameters[2].Value = model.GirlUserID;
			parameters[3].Value = model.BoyOath;
			parameters[4].Value = model.GirlOath;
			parameters[5].Value = model.Certificate;
			parameters[6].Value = model.JoinDate;
			parameters[7].Value = model.ApplyUserID;
			parameters[8].Value = model.ApplyTime;
			parameters[9].Value = model.Flow;
            parameters[10].Value = model.State;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.Lover model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Lover set ");
			strSql.Append("Avatar=@Avatar,");
			strSql.Append("BoyUserID=@BoyUserID,");
			strSql.Append("GirlUserID=@GirlUserID,");
			strSql.Append("BoyOath=@BoyOath,");
			strSql.Append("GirlOath=@GirlOath,");
			strSql.Append("Certificate=@Certificate,");
			strSql.Append("JoinDate=@JoinDate,");
			strSql.Append("ApplyUserID=@ApplyUserID,");
			strSql.Append("ApplyTime=@ApplyTime,");
			strSql.Append("Flow=@Flow");
			strSql.Append(" where LoverID=@LoverID");
			SqlParameter[] parameters = {
					new SqlParameter("@Avatar", SqlDbType.NVarChar,500),
					new SqlParameter("@BoyUserID", SqlDbType.Int,4),
					new SqlParameter("@GirlUserID", SqlDbType.Int,4),
					new SqlParameter("@BoyOath", SqlDbType.NVarChar,500),
					new SqlParameter("@GirlOath", SqlDbType.NVarChar,500),
					new SqlParameter("@Certificate", SqlDbType.Int,4),
					new SqlParameter("@JoinDate", SqlDbType.DateTime),
					new SqlParameter("@ApplyUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplyTime", SqlDbType.DateTime),
					new SqlParameter("@Flow", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.Int,4),
                    new SqlParameter("@LoverID", SqlDbType.Int,4)};
			parameters[0].Value = model.Avatar;
			parameters[1].Value = model.BoyUserID;
			parameters[2].Value = model.GirlUserID;
			parameters[3].Value = model.BoyOath;
			parameters[4].Value = model.GirlOath;
			parameters[5].Value = model.Certificate;
			parameters[6].Value = model.JoinDate;
			parameters[7].Value = model.ApplyUserID;
			parameters[8].Value = model.ApplyTime;
			parameters[9].Value = model.Flow;
            parameters[10].Value = model.State;
			parameters[11].Value = model.LoverID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int LoverID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Lover ");
			strSql.Append(" where LoverID=@LoverID");
			SqlParameter[] parameters = {
					new SqlParameter("@LoverID", SqlDbType.Int,4)
			};
			parameters[0].Value = LoverID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string LoverIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Lover ");
			strSql.Append(" where LoverID in ("+LoverIDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CengZai.Model.Lover GetModel(int LoverID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 LoverID,Avatar,BoyUserID,GirlUserID,BoyOath,GirlOath,Certificate,JoinDate,ApplyUserID,ApplyTime,Flow,State from T_Lover ");
			strSql.Append(" where LoverID=@LoverID");
			SqlParameter[] parameters = {
					new SqlParameter("@LoverID", SqlDbType.Int,4)
			};
			parameters[0].Value = LoverID;

			CengZai.Model.Lover model=new CengZai.Model.Lover();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["LoverID"]!=null && ds.Tables[0].Rows[0]["LoverID"].ToString()!="")
				{
					model.LoverID=int.Parse(ds.Tables[0].Rows[0]["LoverID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Avatar"]!=null && ds.Tables[0].Rows[0]["Avatar"].ToString()!="")
				{
					model.Avatar=ds.Tables[0].Rows[0]["Avatar"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BoyUserID"]!=null && ds.Tables[0].Rows[0]["BoyUserID"].ToString()!="")
				{
					model.BoyUserID=int.Parse(ds.Tables[0].Rows[0]["BoyUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GirlUserID"]!=null && ds.Tables[0].Rows[0]["GirlUserID"].ToString()!="")
				{
					model.GirlUserID=int.Parse(ds.Tables[0].Rows[0]["GirlUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BoyOath"]!=null && ds.Tables[0].Rows[0]["BoyOath"].ToString()!="")
				{
					model.BoyOath=ds.Tables[0].Rows[0]["BoyOath"].ToString();
				}
				if(ds.Tables[0].Rows[0]["GirlOath"]!=null && ds.Tables[0].Rows[0]["GirlOath"].ToString()!="")
				{
					model.GirlOath=ds.Tables[0].Rows[0]["GirlOath"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Certificate"]!=null && ds.Tables[0].Rows[0]["Certificate"].ToString()!="")
				{
					model.Certificate=int.Parse(ds.Tables[0].Rows[0]["Certificate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["JoinDate"]!=null && ds.Tables[0].Rows[0]["JoinDate"].ToString()!="")
				{
					model.JoinDate=DateTime.Parse(ds.Tables[0].Rows[0]["JoinDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ApplyUserID"]!=null && ds.Tables[0].Rows[0]["ApplyUserID"].ToString()!="")
				{
					model.ApplyUserID=int.Parse(ds.Tables[0].Rows[0]["ApplyUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ApplyTime"]!=null && ds.Tables[0].Rows[0]["ApplyTime"].ToString()!="")
				{
					model.ApplyTime=DateTime.Parse(ds.Tables[0].Rows[0]["ApplyTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Flow"]!=null && ds.Tables[0].Rows[0]["Flow"].ToString()!="")
				{
					model.Flow=int.Parse(ds.Tables[0].Rows[0]["Flow"].ToString());
				}
                if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select LoverID,Avatar,BoyUserID,GirlUserID,BoyOath,GirlOath,Certificate,JoinDate,ApplyUserID,ApplyTime,Flow,State ");
			strSql.Append(" FROM T_Lover ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" LoverID,Avatar,BoyUserID,GirlUserID,BoyOath,GirlOath,Certificate,JoinDate,ApplyUserID,ApplyTime,Flow,State ");
			strSql.Append(" FROM T_Lover ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM T_Lover ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.LoverID desc");
			}
			strSql.Append(")AS Row, T.*  from T_Lover T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "T_Lover";
			parameters[1].Value = "LoverID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

