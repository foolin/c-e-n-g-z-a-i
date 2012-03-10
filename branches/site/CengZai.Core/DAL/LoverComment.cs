using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CengZai.Helper;
namespace CengZai.DAL
{
	/// <summary>
	/// 数据访问类:LoverComment
	/// </summary>
	public partial class LoverComment
	{
		public LoverComment()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CommentID", "T_LoverComment"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CommentID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_LoverComment");
			strSql.Append(" where CommentID=@CommentID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CommentID", SqlDbType.Int,4)			};
			parameters[0].Value = CommentID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CengZai.Model.LoverComment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_LoverComment(");
			strSql.Append("CommentID,LoverID,Title,Content,UserID,PostTime,PostIP,ParentID)");
			strSql.Append(" values (");
			strSql.Append("@CommentID,@LoverID,@Title,@Content,@UserID,@PostTime,@PostIP,@ParentID)");
			SqlParameter[] parameters = {
					new SqlParameter("@CommentID", SqlDbType.Int,4),
					new SqlParameter("@LoverID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NVarChar,300),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@PostTime", SqlDbType.DateTime),
					new SqlParameter("@PostIP", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentID", SqlDbType.Int,4)};
			parameters[0].Value = model.CommentID;
			parameters[1].Value = model.LoverID;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.Content;
			parameters[4].Value = model.UserID;
			parameters[5].Value = model.PostTime;
			parameters[6].Value = model.PostIP;
			parameters[7].Value = model.ParentID;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.LoverComment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_LoverComment set ");
			strSql.Append("LoverID=@LoverID,");
			strSql.Append("Title=@Title,");
			strSql.Append("Content=@Content,");
			strSql.Append("UserID=@UserID,");
			strSql.Append("PostTime=@PostTime,");
			strSql.Append("PostIP=@PostIP,");
			strSql.Append("ParentID=@ParentID");
			strSql.Append(" where CommentID=@CommentID ");
			SqlParameter[] parameters = {
					new SqlParameter("@LoverID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NVarChar,300),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@PostTime", SqlDbType.DateTime),
					new SqlParameter("@PostIP", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@CommentID", SqlDbType.Int,4)};
			parameters[0].Value = model.LoverID;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.Content;
			parameters[3].Value = model.UserID;
			parameters[4].Value = model.PostTime;
			parameters[5].Value = model.PostIP;
			parameters[6].Value = model.ParentID;
			parameters[7].Value = model.CommentID;

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
		public bool Delete(int CommentID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_LoverComment ");
			strSql.Append(" where CommentID=@CommentID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CommentID", SqlDbType.Int,4)			};
			parameters[0].Value = CommentID;

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
		public bool DeleteList(string CommentIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_LoverComment ");
			strSql.Append(" where CommentID in ("+CommentIDlist + ")  ");
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
		public CengZai.Model.LoverComment GetModel(int CommentID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CommentID,LoverID,Title,Content,UserID,PostTime,PostIP,ParentID from T_LoverComment ");
			strSql.Append(" where CommentID=@CommentID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CommentID", SqlDbType.Int,4)			};
			parameters[0].Value = CommentID;

			CengZai.Model.LoverComment model=new CengZai.Model.LoverComment();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CommentID"]!=null && ds.Tables[0].Rows[0]["CommentID"].ToString()!="")
				{
					model.CommentID=int.Parse(ds.Tables[0].Rows[0]["CommentID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LoverID"]!=null && ds.Tables[0].Rows[0]["LoverID"].ToString()!="")
				{
					model.LoverID=int.Parse(ds.Tables[0].Rows[0]["LoverID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Title"]!=null && ds.Tables[0].Rows[0]["Title"].ToString()!="")
				{
					model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
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
				if(ds.Tables[0].Rows[0]["PostIP"]!=null && ds.Tables[0].Rows[0]["PostIP"].ToString()!="")
				{
					model.PostIP=ds.Tables[0].Rows[0]["PostIP"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ParentID"]!=null && ds.Tables[0].Rows[0]["ParentID"].ToString()!="")
				{
					model.ParentID=int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
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
			strSql.Append("select CommentID,LoverID,Title,Content,UserID,PostTime,PostIP,ParentID ");
			strSql.Append(" FROM T_LoverComment ");
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
			strSql.Append(" CommentID,LoverID,Title,Content,UserID,PostTime,PostIP,ParentID ");
			strSql.Append(" FROM T_LoverComment ");
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
			strSql.Append("select count(1) FROM T_LoverComment ");
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
				strSql.Append("order by T.CommentID desc");
			}
			strSql.Append(")AS Row, T.*  from T_LoverComment T ");
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
			parameters[0].Value = "T_LoverComment";
			parameters[1].Value = "CommentID";
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

