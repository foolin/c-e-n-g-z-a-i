using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CengZai.Helper;
namespace CengZai.DAL
{
	/// <summary>
	/// 数据访问类:Article
	/// </summary>
	public partial class Article
	{
		public Article()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ArtID", "T_Article"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ArtID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Article");
			strSql.Append(" where ArtID=@ArtID");
			SqlParameter[] parameters = {
					new SqlParameter("@ArtID", SqlDbType.Int,4)
			};
			parameters[0].Value = ArtID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CengZai.Model.Article model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Article(");
			strSql.Append("CategoryID,Title,Content,Type,IsTop,UserID,PostTime,PostIP,ViewCount,ReplyCount,ReportCount,Privacy,State)");
			strSql.Append(" values (");
			strSql.Append("@CategoryID,@Title,@Content,@Type,@IsTop,@UserID,@PostTime,@PostIP,@ViewCount,@ReplyCount,@ReportCount,@Privacy,@State)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NVarChar,4000),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@PostTime", SqlDbType.DateTime),
					new SqlParameter("@PostIP", SqlDbType.NVarChar,50),
					new SqlParameter("@ViewCount", SqlDbType.Int,4),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@ReportCount", SqlDbType.Int,4),
					new SqlParameter("@Privacy", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.Int,4)};
			parameters[0].Value = model.CategoryID;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.Content;
			parameters[3].Value = model.Type;
			parameters[4].Value = model.IsTop;
			parameters[5].Value = model.UserID;
			parameters[6].Value = model.PostTime;
			parameters[7].Value = model.PostIP;
			parameters[8].Value = model.ViewCount;
			parameters[9].Value = model.ReplyCount;
			parameters[10].Value = model.ReportCount;
			parameters[11].Value = model.Privacy;
			parameters[12].Value = model.State;

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
		public bool Update(CengZai.Model.Article model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Article set ");
			strSql.Append("CategoryID=@CategoryID,");
			strSql.Append("Title=@Title,");
			strSql.Append("Content=@Content,");
			strSql.Append("Type=@Type,");
			strSql.Append("IsTop=@IsTop,");
			strSql.Append("UserID=@UserID,");
			strSql.Append("PostTime=@PostTime,");
			strSql.Append("PostIP=@PostIP,");
			strSql.Append("ViewCount=@ViewCount,");
			strSql.Append("ReplyCount=@ReplyCount,");
			strSql.Append("ReportCount=@ReportCount,");
			strSql.Append("Privacy=@Privacy,");
			strSql.Append("State=@State");
			strSql.Append(" where ArtID=@ArtID");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NVarChar,4000),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@PostTime", SqlDbType.DateTime),
					new SqlParameter("@PostIP", SqlDbType.NVarChar,50),
					new SqlParameter("@ViewCount", SqlDbType.Int,4),
					new SqlParameter("@ReplyCount", SqlDbType.Int,4),
					new SqlParameter("@ReportCount", SqlDbType.Int,4),
					new SqlParameter("@Privacy", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@ArtID", SqlDbType.Int,4)};
			parameters[0].Value = model.CategoryID;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.Content;
			parameters[3].Value = model.Type;
			parameters[4].Value = model.IsTop;
			parameters[5].Value = model.UserID;
			parameters[6].Value = model.PostTime;
			parameters[7].Value = model.PostIP;
			parameters[8].Value = model.ViewCount;
			parameters[9].Value = model.ReplyCount;
			parameters[10].Value = model.ReportCount;
			parameters[11].Value = model.Privacy;
			parameters[12].Value = model.State;
			parameters[13].Value = model.ArtID;

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
		public bool Delete(int ArtID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Article ");
			strSql.Append(" where ArtID=@ArtID");
			SqlParameter[] parameters = {
					new SqlParameter("@ArtID", SqlDbType.Int,4)
			};
			parameters[0].Value = ArtID;

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
		public bool DeleteList(string ArtIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Article ");
			strSql.Append(" where ArtID in ("+ArtIDlist + ")  ");
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
		public CengZai.Model.Article GetModel(int ArtID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ArtID,CategoryID,Title,Content,Type,IsTop,UserID,PostTime,PostIP,ViewCount,ReplyCount,ReportCount,Privacy,State from T_Article ");
			strSql.Append(" where ArtID=@ArtID");
			SqlParameter[] parameters = {
					new SqlParameter("@ArtID", SqlDbType.Int,4)
			};
			parameters[0].Value = ArtID;

			CengZai.Model.Article model=new CengZai.Model.Article();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ArtID"]!=null && ds.Tables[0].Rows[0]["ArtID"].ToString()!="")
				{
					model.ArtID=int.Parse(ds.Tables[0].Rows[0]["ArtID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CategoryID"]!=null && ds.Tables[0].Rows[0]["CategoryID"].ToString()!="")
				{
					model.CategoryID=int.Parse(ds.Tables[0].Rows[0]["CategoryID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Title"]!=null && ds.Tables[0].Rows[0]["Title"].ToString()!="")
				{
					model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Content"]!=null && ds.Tables[0].Rows[0]["Content"].ToString()!="")
				{
					model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Type"]!=null && ds.Tables[0].Rows[0]["Type"].ToString()!="")
				{
					model.Type=int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsTop"]!=null && ds.Tables[0].Rows[0]["IsTop"].ToString()!="")
				{
					model.IsTop=int.Parse(ds.Tables[0].Rows[0]["IsTop"].ToString());
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
				if(ds.Tables[0].Rows[0]["ViewCount"]!=null && ds.Tables[0].Rows[0]["ViewCount"].ToString()!="")
				{
					model.ViewCount=int.Parse(ds.Tables[0].Rows[0]["ViewCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReplyCount"]!=null && ds.Tables[0].Rows[0]["ReplyCount"].ToString()!="")
				{
					model.ReplyCount=int.Parse(ds.Tables[0].Rows[0]["ReplyCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReportCount"]!=null && ds.Tables[0].Rows[0]["ReportCount"].ToString()!="")
				{
					model.ReportCount=int.Parse(ds.Tables[0].Rows[0]["ReportCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Privacy"]!=null && ds.Tables[0].Rows[0]["Privacy"].ToString()!="")
				{
					model.Privacy=int.Parse(ds.Tables[0].Rows[0]["Privacy"].ToString());
				}
				if(ds.Tables[0].Rows[0]["State"]!=null && ds.Tables[0].Rows[0]["State"].ToString()!="")
				{
					model.State=int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
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
			strSql.Append("select ArtID,CategoryID,Title,Content,Type,IsTop,UserID,PostTime,PostIP,ViewCount,ReplyCount,ReportCount,Privacy,State ");
			strSql.Append(" FROM T_Article ");
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
			strSql.Append(" ArtID,CategoryID,Title,Content,Type,IsTop,UserID,PostTime,PostIP,ViewCount,ReplyCount,ReportCount,Privacy,State ");
			strSql.Append(" FROM T_Article ");
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
			strSql.Append("select count(1) FROM T_Article ");
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
				strSql.Append("order by T.ArtID desc");
			}
			strSql.Append(")AS Row, T.*  from T_Article T ");
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
			parameters[0].Value = "T_Article";
			parameters[1].Value = "ArtID";
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

