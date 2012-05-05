using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CengZai.Helper;
namespace CengZai.DAL
{
	/// <summary>
	/// 数据访问类:MessageReply
	/// </summary>
	public partial class MessageReply
	{
		public MessageReply()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ReplyID", "T_MessageReply"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ReplyID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_MessageReply");
			strSql.Append(" where ReplyID=@ReplyID");
			SqlParameter[] parameters = {
					new SqlParameter("@ReplyID", SqlDbType.Int,4)
			};
			parameters[0].Value = ReplyID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CengZai.Model.MessageReply model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_MessageReply(");
			strSql.Append("MsgID,Content,ReplyUserID,ReplyTime)");
			strSql.Append(" values (");
			strSql.Append("@MsgID,@Content,@ReplyUserID,@ReplyTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgID", SqlDbType.Int,4),
					new SqlParameter("@Content", SqlDbType.NVarChar,500),
					new SqlParameter("@ReplyUserID", SqlDbType.Int,4),
					new SqlParameter("@ReplyTime", SqlDbType.DateTime)};
			parameters[0].Value = model.MsgID;
			parameters[1].Value = model.Content;
			parameters[2].Value = model.ReplyUserID;
			parameters[3].Value = model.ReplyTime;

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
		public bool Update(CengZai.Model.MessageReply model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_MessageReply set ");
			strSql.Append("MsgID=@MsgID,");
			strSql.Append("Content=@Content,");
			strSql.Append("ReplyUserID=@ReplyUserID,");
			strSql.Append("ReplyTime=@ReplyTime");
			strSql.Append(" where ReplyID=@ReplyID");
			SqlParameter[] parameters = {
					new SqlParameter("@MsgID", SqlDbType.Int,4),
					new SqlParameter("@Content", SqlDbType.NVarChar,500),
					new SqlParameter("@ReplyUserID", SqlDbType.Int,4),
					new SqlParameter("@ReplyTime", SqlDbType.DateTime),
					new SqlParameter("@ReplyID", SqlDbType.Int,4)};
			parameters[0].Value = model.MsgID;
			parameters[1].Value = model.Content;
			parameters[2].Value = model.ReplyUserID;
			parameters[3].Value = model.ReplyTime;
			parameters[4].Value = model.ReplyID;

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
		public bool Delete(int ReplyID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_MessageReply ");
			strSql.Append(" where ReplyID=@ReplyID");
			SqlParameter[] parameters = {
					new SqlParameter("@ReplyID", SqlDbType.Int,4)
			};
			parameters[0].Value = ReplyID;

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
		public bool DeleteList(string ReplyIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_MessageReply ");
			strSql.Append(" where ReplyID in ("+ReplyIDlist + ")  ");
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
		public CengZai.Model.MessageReply GetModel(int ReplyID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ReplyID,MsgID,Content,ReplyUserID,ReplyTime from T_MessageReply ");
			strSql.Append(" where ReplyID=@ReplyID");
			SqlParameter[] parameters = {
					new SqlParameter("@ReplyID", SqlDbType.Int,4)
			};
			parameters[0].Value = ReplyID;

			CengZai.Model.MessageReply model=new CengZai.Model.MessageReply();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReplyID"]!=null && ds.Tables[0].Rows[0]["ReplyID"].ToString()!="")
				{
					model.ReplyID=int.Parse(ds.Tables[0].Rows[0]["ReplyID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MsgID"]!=null && ds.Tables[0].Rows[0]["MsgID"].ToString()!="")
				{
					model.MsgID=int.Parse(ds.Tables[0].Rows[0]["MsgID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Content"]!=null && ds.Tables[0].Rows[0]["Content"].ToString()!="")
				{
					model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ReplyUserID"]!=null && ds.Tables[0].Rows[0]["ReplyUserID"].ToString()!="")
				{
					model.ReplyUserID=int.Parse(ds.Tables[0].Rows[0]["ReplyUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReplyTime"]!=null && ds.Tables[0].Rows[0]["ReplyTime"].ToString()!="")
				{
					model.ReplyTime=DateTime.Parse(ds.Tables[0].Rows[0]["ReplyTime"].ToString());
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
			strSql.Append("select ReplyID,MsgID,Content,ReplyUserID,ReplyTime ");
			strSql.Append(" FROM T_MessageReply ");
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
			strSql.Append(" ReplyID,MsgID,Content,ReplyUserID,ReplyTime ");
			strSql.Append(" FROM T_MessageReply ");
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
			strSql.Append("select count(1) FROM T_MessageReply ");
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
				strSql.Append("order by T.ReplyID desc");
			}
			strSql.Append(")AS Row, T.*  from T_MessageReply T ");
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
			parameters[0].Value = "T_MessageReply";
			parameters[1].Value = "ReplyID";
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

