using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CengZai.Helper;
namespace CengZai.DAL
{
	/// <summary>
	/// 数据访问类:Like
	/// </summary>
	public partial class Like
	{
		public Like()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("LikeID", "T_Like"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int LikeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Like");
			strSql.Append(" where LikeID=@LikeID");
			SqlParameter[] parameters = {
					new SqlParameter("@LikeID", SqlDbType.Int,4)
			};
			parameters[0].Value = LikeID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CengZai.Model.Like model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Like(");
			strSql.Append("LikeUserID,LikeArtID,UserID,Type)");
			strSql.Append(" values (");
			strSql.Append("@LikeUserID,@LikeArtID,@UserID,@Type)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@LikeUserID", SqlDbType.Int,4),
					new SqlParameter("@LikeArtID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)};
			parameters[0].Value = model.LikeUserID;
			parameters[1].Value = model.LikeArtID;
			parameters[2].Value = model.UserID;
			parameters[3].Value = model.Type;

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
		public bool Update(CengZai.Model.Like model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Like set ");
			strSql.Append("LikeUserID=@LikeUserID,");
			strSql.Append("LikeArtID=@LikeArtID,");
			strSql.Append("UserID=@UserID,");
			strSql.Append("Type=@Type");
			strSql.Append(" where LikeID=@LikeID");
			SqlParameter[] parameters = {
					new SqlParameter("@LikeUserID", SqlDbType.Int,4),
					new SqlParameter("@LikeArtID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@LikeID", SqlDbType.Int,4)};
			parameters[0].Value = model.LikeUserID;
			parameters[1].Value = model.LikeArtID;
			parameters[2].Value = model.UserID;
			parameters[3].Value = model.Type;
			parameters[4].Value = model.LikeID;

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
		public bool Delete(int LikeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Like ");
			strSql.Append(" where LikeID=@LikeID");
			SqlParameter[] parameters = {
					new SqlParameter("@LikeID", SqlDbType.Int,4)
			};
			parameters[0].Value = LikeID;

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
		public bool DeleteList(string LikeIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Like ");
			strSql.Append(" where LikeID in ("+LikeIDlist + ")  ");
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
		public CengZai.Model.Like GetModel(int LikeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 LikeID,LikeUserID,LikeArtID,UserID,Type from T_Like ");
			strSql.Append(" where LikeID=@LikeID");
			SqlParameter[] parameters = {
					new SqlParameter("@LikeID", SqlDbType.Int,4)
			};
			parameters[0].Value = LikeID;

			CengZai.Model.Like model=new CengZai.Model.Like();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["LikeID"]!=null && ds.Tables[0].Rows[0]["LikeID"].ToString()!="")
				{
					model.LikeID=int.Parse(ds.Tables[0].Rows[0]["LikeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LikeUserID"]!=null && ds.Tables[0].Rows[0]["LikeUserID"].ToString()!="")
				{
					model.LikeUserID=int.Parse(ds.Tables[0].Rows[0]["LikeUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LikeArtID"]!=null && ds.Tables[0].Rows[0]["LikeArtID"].ToString()!="")
				{
					model.LikeArtID=int.Parse(ds.Tables[0].Rows[0]["LikeArtID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserID"]!=null && ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Type"]!=null && ds.Tables[0].Rows[0]["Type"].ToString()!="")
				{
					model.Type=int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
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
			strSql.Append("select LikeID,LikeUserID,LikeArtID,UserID,Type ");
			strSql.Append(" FROM T_Like ");
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
			strSql.Append(" LikeID,LikeUserID,LikeArtID,UserID,Type ");
			strSql.Append(" FROM T_Like ");
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
			strSql.Append("select count(1) FROM T_Like ");
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
				strSql.Append("order by T.LikeID desc");
			}
			strSql.Append(")AS Row, T.*  from T_Like T ");
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
			parameters[0].Value = "T_Like";
			parameters[1].Value = "LikeID";
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

