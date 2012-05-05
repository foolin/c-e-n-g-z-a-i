using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CengZai.Helper;
namespace CengZai.DAL
{
	/// <summary>
	/// 数据访问类:Dynamic
	/// </summary>
	public partial class Dynamic
	{
		public Dynamic()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("DynID", "T_Dynamic"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int DynID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Dynamic");
			strSql.Append(" where DynID=@DynID");
			SqlParameter[] parameters = {
					new SqlParameter("@DynID", SqlDbType.Int,4)
			};
			parameters[0].Value = DynID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CengZai.Model.Dynamic model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Dynamic(");
			strSql.Append("Content,UserID,PostTime)");
			strSql.Append(" values (");
			strSql.Append("@Content,@UserID,@PostTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Content", SqlDbType.NVarChar,300),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@PostTime", SqlDbType.DateTime)};
			parameters[0].Value = model.Content;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.PostTime;

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
		public bool Update(CengZai.Model.Dynamic model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Dynamic set ");
			strSql.Append("Content=@Content,");
			strSql.Append("UserID=@UserID,");
			strSql.Append("PostTime=@PostTime");
			strSql.Append(" where DynID=@DynID");
			SqlParameter[] parameters = {
					new SqlParameter("@Content", SqlDbType.NVarChar,300),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@PostTime", SqlDbType.DateTime),
					new SqlParameter("@DynID", SqlDbType.Int,4)};
			parameters[0].Value = model.Content;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.PostTime;
			parameters[3].Value = model.DynID;

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
		public bool Delete(int DynID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Dynamic ");
			strSql.Append(" where DynID=@DynID");
			SqlParameter[] parameters = {
					new SqlParameter("@DynID", SqlDbType.Int,4)
			};
			parameters[0].Value = DynID;

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
		public bool DeleteList(string DynIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Dynamic ");
			strSql.Append(" where DynID in ("+DynIDlist + ")  ");
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
		public CengZai.Model.Dynamic GetModel(int DynID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 DynID,Content,UserID,PostTime from T_Dynamic ");
			strSql.Append(" where DynID=@DynID");
			SqlParameter[] parameters = {
					new SqlParameter("@DynID", SqlDbType.Int,4)
			};
			parameters[0].Value = DynID;

			CengZai.Model.Dynamic model=new CengZai.Model.Dynamic();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["DynID"]!=null && ds.Tables[0].Rows[0]["DynID"].ToString()!="")
				{
					model.DynID=int.Parse(ds.Tables[0].Rows[0]["DynID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Content"]!=null && ds.Tables[0].Rows[0]["Content"].ToString()!="")
				{
					model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				}
				if(ds.Tables[0].Rows[0]["UserID"]!=null && ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PostTime"]!=null && ds.Tables[0].Rows[0]["PostTime"].ToString()!="")
				{
					model.PostTime=DateTime.Parse(ds.Tables[0].Rows[0]["PostTime"].ToString());
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
			strSql.Append("select DynID,Content,UserID,PostTime ");
			strSql.Append(" FROM T_Dynamic ");
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
			strSql.Append(" DynID,Content,UserID,PostTime ");
			strSql.Append(" FROM T_Dynamic ");
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
			strSql.Append("select count(1) FROM T_Dynamic ");
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
				strSql.Append("order by T.DynID desc");
			}
			strSql.Append(")AS Row, T.*  from T_Dynamic T ");
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
			parameters[0].Value = "T_Dynamic";
			parameters[1].Value = "DynID";
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

