using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CengZai.Helper;
namespace CengZai.DAL
{
	/// <summary>
	/// 数据访问类:Inbox
	/// </summary>
	public partial class Inbox
	{
		public Inbox()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("MsgID", "T_Inbox"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MsgID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Inbox");
			strSql.Append(" where MsgID=@MsgID");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgID", SqlDbType.Int,4)
			};
			parameters[0].Value = MsgID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CengZai.Model.Inbox model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Inbox(");
			strSql.Append("Title,Content,ToUserID,FromUserID,SendTime,IsRead,IsSystem,IsDelete)");
			strSql.Append(" values (");
			strSql.Append("@Title,@Content,@ToUserID,@FromUserID,@SendTime,@IsRead,@IsSystem,@IsDelete)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NVarChar,300),
					new SqlParameter("@ToUserID", SqlDbType.Int,4),
					new SqlParameter("@FromUserID", SqlDbType.Int,4),
					new SqlParameter("@SendTime", SqlDbType.DateTime),
					new SqlParameter("@IsRead", SqlDbType.Int,4),
					new SqlParameter("@IsSystem", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.Content;
			parameters[2].Value = model.ToUserID;
			parameters[3].Value = model.FromUserID;
			parameters[4].Value = model.SendTime;
			parameters[5].Value = model.IsRead;
			parameters[6].Value = model.IsSystem;
			parameters[7].Value = model.IsDelete;

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
		public bool Update(CengZai.Model.Inbox model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Inbox set ");
			strSql.Append("Title=@Title,");
			strSql.Append("Content=@Content,");
			strSql.Append("ToUserID=@ToUserID,");
			strSql.Append("FromUserID=@FromUserID,");
			strSql.Append("SendTime=@SendTime,");
			strSql.Append("IsRead=@IsRead,");
			strSql.Append("IsSystem=@IsSystem,");
			strSql.Append("IsDelete=@IsDelete");
			strSql.Append(" where MsgID=@MsgID");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NVarChar,300),
					new SqlParameter("@ToUserID", SqlDbType.Int,4),
					new SqlParameter("@FromUserID", SqlDbType.Int,4),
					new SqlParameter("@SendTime", SqlDbType.DateTime),
					new SqlParameter("@IsRead", SqlDbType.Int,4),
					new SqlParameter("@IsSystem", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@MsgID", SqlDbType.Int,4)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.Content;
			parameters[2].Value = model.ToUserID;
			parameters[3].Value = model.FromUserID;
			parameters[4].Value = model.SendTime;
			parameters[5].Value = model.IsRead;
			parameters[6].Value = model.IsSystem;
			parameters[7].Value = model.IsDelete;
			parameters[8].Value = model.MsgID;

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
		public bool Delete(int MsgID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Inbox ");
			strSql.Append(" where MsgID=@MsgID");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgID", SqlDbType.Int,4)
			};
			parameters[0].Value = MsgID;

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
		public bool DeleteList(string MsgIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Inbox ");
			strSql.Append(" where MsgID in ("+MsgIDlist + ")  ");
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
		public CengZai.Model.Inbox GetModel(int MsgID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 MsgID,Title,Content,ToUserID,FromUserID,SendTime,IsRead,IsSystem,IsDelete from T_Inbox ");
			strSql.Append(" where MsgID=@MsgID");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgID", SqlDbType.Int,4)
			};
			parameters[0].Value = MsgID;

			CengZai.Model.Inbox model=new CengZai.Model.Inbox();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["MsgID"]!=null && ds.Tables[0].Rows[0]["MsgID"].ToString()!="")
				{
					model.MsgID=int.Parse(ds.Tables[0].Rows[0]["MsgID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Title"]!=null && ds.Tables[0].Rows[0]["Title"].ToString()!="")
				{
					model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Content"]!=null && ds.Tables[0].Rows[0]["Content"].ToString()!="")
				{
					model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ToUserID"]!=null && ds.Tables[0].Rows[0]["ToUserID"].ToString()!="")
				{
					model.ToUserID=int.Parse(ds.Tables[0].Rows[0]["ToUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FromUserID"]!=null && ds.Tables[0].Rows[0]["FromUserID"].ToString()!="")
				{
					model.FromUserID=int.Parse(ds.Tables[0].Rows[0]["FromUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SendTime"]!=null && ds.Tables[0].Rows[0]["SendTime"].ToString()!="")
				{
					model.SendTime=DateTime.Parse(ds.Tables[0].Rows[0]["SendTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsRead"]!=null && ds.Tables[0].Rows[0]["IsRead"].ToString()!="")
				{
					model.IsRead=int.Parse(ds.Tables[0].Rows[0]["IsRead"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSystem"]!=null && ds.Tables[0].Rows[0]["IsSystem"].ToString()!="")
				{
					model.IsSystem=int.Parse(ds.Tables[0].Rows[0]["IsSystem"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"]!=null && ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
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
			strSql.Append("select MsgID,Title,Content,ToUserID,FromUserID,SendTime,IsRead,IsSystem,IsDelete ");
			strSql.Append(" FROM T_Inbox ");
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
			strSql.Append(" MsgID,Title,Content,ToUserID,FromUserID,SendTime,IsRead,IsSystem,IsDelete ");
			strSql.Append(" FROM T_Inbox ");
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
			strSql.Append("select count(1) FROM T_Inbox ");
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
				strSql.Append("order by T.MsgID desc");
			}
			strSql.Append(")AS Row, T.*  from T_Inbox T ");
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
			parameters[0].Value = "T_Inbox";
			parameters[1].Value = "MsgID";
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

