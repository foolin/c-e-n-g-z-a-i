using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CengZai.Helper;
namespace CengZai.DAL
{
	/// <summary>
	/// 数据访问类:Category
	/// </summary>
	public partial class Category
	{
		public Category()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CategoryID", "T_Category"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CategoryID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Category");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CengZai.Model.Category model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Category(");
			strSql.Append("CategoryName,CategoryDesc)");
			strSql.Append(" values (");
			strSql.Append("@CategoryName,@CategoryDesc)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,50),
					new SqlParameter("@CategoryDesc", SqlDbType.NVarChar,300)};
			parameters[0].Value = model.CategoryName;
			parameters[1].Value = model.CategoryDesc;

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
		public bool Update(CengZai.Model.Category model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Category set ");
			strSql.Append("CategoryName=@CategoryName,");
			strSql.Append("CategoryDesc=@CategoryDesc");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,50),
					new SqlParameter("@CategoryDesc", SqlDbType.NVarChar,300),
					new SqlParameter("@CategoryID", SqlDbType.Int,4)};
			parameters[0].Value = model.CategoryName;
			parameters[1].Value = model.CategoryDesc;
			parameters[2].Value = model.CategoryID;

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
		public bool Delete(int CategoryID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Category ");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryID;

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
		public bool DeleteList(string CategoryIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Category ");
			strSql.Append(" where CategoryID in ("+CategoryIDlist + ")  ");
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
		public CengZai.Model.Category GetModel(int CategoryID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CategoryID,CategoryName,CategoryDesc from T_Category ");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryID;

			CengZai.Model.Category model=new CengZai.Model.Category();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CategoryID"]!=null && ds.Tables[0].Rows[0]["CategoryID"].ToString()!="")
				{
					model.CategoryID=int.Parse(ds.Tables[0].Rows[0]["CategoryID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CategoryName"]!=null && ds.Tables[0].Rows[0]["CategoryName"].ToString()!="")
				{
					model.CategoryName=ds.Tables[0].Rows[0]["CategoryName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CategoryDesc"]!=null && ds.Tables[0].Rows[0]["CategoryDesc"].ToString()!="")
				{
					model.CategoryDesc=ds.Tables[0].Rows[0]["CategoryDesc"].ToString();
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
			strSql.Append("select CategoryID,CategoryName,CategoryDesc ");
			strSql.Append(" FROM T_Category ");
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
			strSql.Append(" CategoryID,CategoryName,CategoryDesc ");
			strSql.Append(" FROM T_Category ");
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
			strSql.Append("select count(1) FROM T_Category ");
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
				strSql.Append("order by T.CategoryID desc");
			}
			strSql.Append(")AS Row, T.*  from T_Category T ");
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
			parameters[0].Value = "T_Category";
			parameters[1].Value = "CategoryID";
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

