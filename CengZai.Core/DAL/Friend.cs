using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CengZai.Helper;
using CengZai.Model;
namespace CengZai.DAL
{
	/// <summary>
	/// 数据访问类:Friend
	/// </summary>
	public partial class Friend
	{
		public Friend()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "T_Friend"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Friend");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CengZai.Model.Friend model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Friend(");
			strSql.Append("UserID,FriendUserID,Type)");
			strSql.Append(" values (");
			strSql.Append("@UserID,@FriendUserID,@Type)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@FriendUserID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.FriendUserID;
			parameters[2].Value = model.Type;

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
		public bool Update(CengZai.Model.Friend model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Friend set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("FriendUserID=@FriendUserID,");
			strSql.Append("Type=@Type");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@FriendUserID", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.FriendUserID;
			parameters[2].Value = model.Type;
			parameters[3].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Friend ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Friend ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public CengZai.Model.Friend GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,UserID,FriendUserID,Type from T_Friend ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			CengZai.Model.Friend model=new CengZai.Model.Friend();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserID"]!=null && ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FriendUserID"]!=null && ds.Tables[0].Rows[0]["FriendUserID"].ToString()!="")
				{
					model.FriendUserID=int.Parse(ds.Tables[0].Rows[0]["FriendUserID"].ToString());
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
			strSql.Append("select ID,UserID,FriendUserID,Type ");
			strSql.Append(" FROM T_Friend ");
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
			strSql.Append(" ID,UserID,FriendUserID,Type ");
			strSql.Append(" FROM T_Friend ");
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
			strSql.Append("select count(1) FROM T_Friend ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from T_Friend T ");
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
			parameters[0].Value = "T_Friend";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method


        /// <summary>
        /// 取朋友的User数据
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="relation"></param>
        /// <param name="fieldOrder"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetFriendUserListByPage(int userID, FriendRelation relation, string fieldOrder, int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            string sqlSelectUserID = string.Empty;
            if (relation == FriendRelation.Black)
            {
                sqlSelectUserID = "select FriendUserID from T_Friend where type="+ (int)FriendType.Black +" and UserID=" + userID;
            }
            else if (relation == FriendRelation.Fans)
            {
                sqlSelectUserID = "select UserID from T_Friend where type=" + (int)FriendType.Follow + " and FriendUserID=" + userID;
            }
            else if (relation == FriendRelation.Follow)
            {
                sqlSelectUserID = "select FriendUserID from T_Friend where type=" + (int)FriendType.Follow + " and UserID=" + userID;
            }
            else if (relation == FriendRelation.Friend)
            {
                sqlSelectUserID = string.Format(@"select FriendUserID from T_Friend  
                       where type={0} and UserID={1} and FriendUserID in (
                            select UserID from T_Friend  where type={0} and FriendUserID={1} /*关注我的人*/
                        )
                    "
                    , (int)FriendType.Follow
                    , userID);
            }
            else if (relation == FriendRelation.FollowOrFans)
            {
                sqlSelectUserID = string.Format(@"
                            select FriendUserID from T_Friend  where type={0} and UserID={1} /*我关注的人*/
                        union all 
                            select UserID from T_Friend  where type={0} and FriendUserID={1} /*关注我的人*/
                    "
                    , (int)FriendType.Follow 
                    ,userID);
            }
            else
            {
                return null;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM T_User ");
            strSql.Append(@" Where UserID IN ( " + sqlSelectUserID + ") and State>=0 ");
            if (string.IsNullOrEmpty(fieldOrder))
            {
                fieldOrder = "Nickname ASC";
            }
            DataSet dsList = null;
            dsList = SqlHelperEx.ExecuteDatasetByPage(Config.ConnString, strSql.ToString(), fieldOrder, pageSize, pageIndex, out totalCount);
            return dsList;
        }



        /// <summary>
        /// 取朋友的User数据
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="relation"></param>
        /// <param name="top"></param>
        /// <param name="fieldOrder"></param>
        /// <returns></returns>
        public DataSet GetFriendUserList(int userID, FriendRelation relation, int top, string fieldOrder)
        {
            string sqlSelectUserID = string.Empty;
            if (relation == FriendRelation.Black)
            {
                sqlSelectUserID = "select FriendUserID from T_Friend where type=" + (int)FriendType.Black + " and UserID=" + userID;
            }
            else if (relation == FriendRelation.Fans)
            {
                sqlSelectUserID = "select UserID from T_Friend where type=" + (int)FriendType.Follow + " and FriendUserID=" + userID;
            }
            else if (relation == FriendRelation.Follow)
            {
                sqlSelectUserID = "select FriendUserID from T_Friend where type=" + (int)FriendType.Follow + " and UserID=" + userID;
            }
            else if (relation == FriendRelation.Friend)
            {
                sqlSelectUserID = string.Format(@"select FriendUserID from T_Friend  
                       where type={0} and UserID={1} and FriendUserID in (
                            select UserID from T_Friend  where type={0} and FriendUserID={1} /*关注我的人*/
                        )
                    "
                    , (int)FriendType.Follow
                    , userID);
            }
            else if (relation == FriendRelation.FollowOrFans)
            {
                sqlSelectUserID = string.Format(@"
                            select FriendUserID from T_Friend  where type={0} and UserID={1} /*我关注的人*/
                        union all 
                            select UserID from T_Friend  where type={0} and FriendUserID={1} /*关注我的人*/
                    "
                    , (int)FriendType.Follow
                    , userID);
            }
            else
            {
                return null;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (top > 0)
            {
                strSql.Append(" top " + top + " ");
            }
            strSql.Append(" * FROM T_User ");
            strSql.Append(@" Where UserID IN ( " + sqlSelectUserID + ") and State>=0 ");
            if (string.IsNullOrEmpty(fieldOrder))
            {
                strSql.Append(" ORDER BY Nickname ASC ");
            }
            else
            {
                strSql.Append(" ORDER BY " + fieldOrder);
            }
            DataSet dsList = null;
            dsList = SqlHelper.ExecuteDataset(Config.ConnString, CommandType.Text, strSql.ToString());
            return dsList;
        }

	}
}

