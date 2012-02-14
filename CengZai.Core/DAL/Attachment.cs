using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CengZai.Helper;
namespace CengZai.DAL
{
	/// <summary>
	/// 数据访问类:Attachment
	/// </summary>
	public partial class Attachment
	{
		public Attachment()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("AttachID", "T_Attachment"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AttachID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Attachment");
			strSql.Append(" where AttachID=@AttachID");
			SqlParameter[] parameters = {
					new SqlParameter("@AttachID", SqlDbType.Int,4)
			};
			parameters[0].Value = AttachID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CengZai.Model.Attachment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Attachment(");
			strSql.Append("ArtID,Source)");
			strSql.Append(" values (");
			strSql.Append("@ArtID,@Source)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ArtID", SqlDbType.Int,4),
					new SqlParameter("@Source", SqlDbType.NVarChar,1000)};
			parameters[0].Value = model.ArtID;
			parameters[1].Value = model.Source;

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
		public bool Update(CengZai.Model.Attachment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Attachment set ");
			strSql.Append("ArtID=@ArtID,");
			strSql.Append("Source=@Source");
			strSql.Append(" where AttachID=@AttachID");
			SqlParameter[] parameters = {
					new SqlParameter("@ArtID", SqlDbType.Int,4),
					new SqlParameter("@Source", SqlDbType.NVarChar,1000),
					new SqlParameter("@AttachID", SqlDbType.Int,4)};
			parameters[0].Value = model.ArtID;
			parameters[1].Value = model.Source;
			parameters[2].Value = model.AttachID;

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
		public bool Delete(int AttachID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Attachment ");
			strSql.Append(" where AttachID=@AttachID");
			SqlParameter[] parameters = {
					new SqlParameter("@AttachID", SqlDbType.Int,4)
			};
			parameters[0].Value = AttachID;

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
		public bool DeleteList(string AttachIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Attachment ");
			strSql.Append(" where AttachID in ("+AttachIDlist + ")  ");
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
		public CengZai.Model.Attachment GetModel(int AttachID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 AttachID,ArtID,Source from T_Attachment ");
			strSql.Append(" where AttachID=@AttachID");
			SqlParameter[] parameters = {
					new SqlParameter("@AttachID", SqlDbType.Int,4)
			};
			parameters[0].Value = AttachID;

			CengZai.Model.Attachment model=new CengZai.Model.Attachment();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["AttachID"]!=null && ds.Tables[0].Rows[0]["AttachID"].ToString()!="")
				{
					model.AttachID=int.Parse(ds.Tables[0].Rows[0]["AttachID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ArtID"]!=null && ds.Tables[0].Rows[0]["ArtID"].ToString()!="")
				{
					model.ArtID=int.Parse(ds.Tables[0].Rows[0]["ArtID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Source"]!=null && ds.Tables[0].Rows[0]["Source"].ToString()!="")
				{
					model.Source=ds.Tables[0].Rows[0]["Source"].ToString();
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
			strSql.Append("select AttachID,ArtID,Source ");
			strSql.Append(" FROM T_Attachment ");
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
			strSql.Append(" AttachID,ArtID,Source ");
			strSql.Append(" FROM T_Attachment ");
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
			strSql.Append("select count(1) FROM T_Attachment ");
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
				strSql.Append("order by T.AttachID desc");
			}
			strSql.Append(")AS Row, T.*  from T_Attachment T ");
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
			parameters[0].Value = "T_Attachment";
			parameters[1].Value = "AttachID";
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

