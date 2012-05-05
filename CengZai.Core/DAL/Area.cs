using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CengZai.Helper;
namespace CengZai.DAL
{
	/// <summary>
	/// 数据访问类:Area
	/// </summary>
	public partial class Area
	{
		public Area()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("AreaID", "T_Area"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AreaID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Area");
			strSql.Append(" where AreaID=@AreaID");
			SqlParameter[] parameters = {
					new SqlParameter("@AreaID", SqlDbType.Int,4)
			};
			parameters[0].Value = AreaID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CengZai.Model.Area model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Area(");
			strSql.Append("ParentID,EnName,AreaName,RegionNO)");
			strSql.Append(" values (");
			strSql.Append("@ParentID,@EnName,@AreaName,@RegionNO)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@EnName", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@RegionNO", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ParentID;
			parameters[1].Value = model.EnName;
			parameters[2].Value = model.AreaName;
			parameters[3].Value = model.RegionNO;

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
		public bool Update(CengZai.Model.Area model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Area set ");
			strSql.Append("ParentID=@ParentID,");
			strSql.Append("EnName=@EnName,");
			strSql.Append("AreaName=@AreaName,");
			strSql.Append("RegionNO=@RegionNO");
			strSql.Append(" where AreaID=@AreaID");
			SqlParameter[] parameters = {
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@EnName", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@RegionNO", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaID", SqlDbType.Int,4)};
			parameters[0].Value = model.ParentID;
			parameters[1].Value = model.EnName;
			parameters[2].Value = model.AreaName;
			parameters[3].Value = model.RegionNO;
			parameters[4].Value = model.AreaID;

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
		public bool Delete(int AreaID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Area ");
			strSql.Append(" where AreaID=@AreaID");
			SqlParameter[] parameters = {
					new SqlParameter("@AreaID", SqlDbType.Int,4)
			};
			parameters[0].Value = AreaID;

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
		public bool DeleteList(string AreaIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Area ");
			strSql.Append(" where AreaID in ("+AreaIDlist + ")  ");
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
		public CengZai.Model.Area GetModel(int AreaID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 AreaID,ParentID,EnName,AreaName,RegionNO from T_Area ");
			strSql.Append(" where AreaID=@AreaID");
			SqlParameter[] parameters = {
					new SqlParameter("@AreaID", SqlDbType.Int,4)
			};
			parameters[0].Value = AreaID;

			CengZai.Model.Area model=new CengZai.Model.Area();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["AreaID"]!=null && ds.Tables[0].Rows[0]["AreaID"].ToString()!="")
				{
					model.AreaID=int.Parse(ds.Tables[0].Rows[0]["AreaID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParentID"]!=null && ds.Tables[0].Rows[0]["ParentID"].ToString()!="")
				{
					model.ParentID=int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EnName"]!=null && ds.Tables[0].Rows[0]["EnName"].ToString()!="")
				{
					model.EnName=ds.Tables[0].Rows[0]["EnName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AreaName"]!=null && ds.Tables[0].Rows[0]["AreaName"].ToString()!="")
				{
					model.AreaName=ds.Tables[0].Rows[0]["AreaName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["RegionNO"]!=null && ds.Tables[0].Rows[0]["RegionNO"].ToString()!="")
				{
					model.RegionNO=ds.Tables[0].Rows[0]["RegionNO"].ToString();
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
			strSql.Append("select AreaID,ParentID,EnName,AreaName,RegionNO ");
			strSql.Append(" FROM T_Area ");
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
			strSql.Append(" AreaID,ParentID,EnName,AreaName,RegionNO ");
			strSql.Append(" FROM T_Area ");
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
			strSql.Append("select count(1) FROM T_Area ");
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
				strSql.Append("order by T.AreaID desc");
			}
			strSql.Append(")AS Row, T.*  from T_Area T ");
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
			parameters[0].Value = "T_Area";
			parameters[1].Value = "AreaID";
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

