using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CengZai.Helper;
namespace CengZai.DAL
{
	/// <summary>
	/// 数据访问类:User
	/// </summary>
	public partial class User
	{
		public User()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("UserID", "T_User"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int UserID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_User");
			strSql.Append(" where UserID=@UserID");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
			};
			parameters[0].Value = UserID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_User");
            strSql.Append(" where Email=@Email");
            SqlParameter[] parameters = {
					new SqlParameter("@Email", SqlDbType.NVarChar, 50)
			};
            parameters[0].Value = email;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CengZai.Model.User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_User(");
            strSql.Append("Email,Password,Username,Nickname,Love,Sign,Intro,Birth,Sex,AreaID,Mobile,LoginIp,LoginTime,LoginCount,RegIp,RegTime,State,Privacy,Credit,Vip,Money,Config,Avatar)");
            strSql.Append(" values (");
            strSql.Append("@Email,@Password,@Username,@Nickname,@Love,@Sign,@Intro,@Birth,@Sex,@AreaID,@Mobile,@LoginIp,@LoginTime,@LoginCount,@RegIp,@RegTime,@State,@Privacy,@Credit,@Vip,@Money,@Config,@Avatar)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@Username", SqlDbType.NVarChar,50),
					new SqlParameter("@Nickname", SqlDbType.NVarChar,50),
					new SqlParameter("@Love", SqlDbType.Int,4),
					new SqlParameter("@Sign", SqlDbType.NVarChar,50),
					new SqlParameter("@Intro", SqlDbType.NVarChar,500),
					new SqlParameter("@Birth", SqlDbType.DateTime),
					new SqlParameter("@Sex", SqlDbType.Int,4),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@Mobile", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginIp", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginTime", SqlDbType.DateTime),
					new SqlParameter("@LoginCount", SqlDbType.Int,4),
					new SqlParameter("@RegIp", SqlDbType.NVarChar,50),
					new SqlParameter("@RegTime", SqlDbType.DateTime),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Privacy", SqlDbType.Int,4),
					new SqlParameter("@Credit", SqlDbType.Int,4),
					new SqlParameter("@Vip", SqlDbType.Int,4),
					new SqlParameter("@Money", SqlDbType.Int,4),
					new SqlParameter("@Config", SqlDbType.NVarChar,2000),
                    new SqlParameter("@Avatar", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Email;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.Username;
            parameters[3].Value = model.Nickname;
            parameters[4].Value = model.Love;
            parameters[5].Value = model.Sign;
            parameters[6].Value = model.Intro;
            parameters[7].Value = model.Birth;
            parameters[8].Value = model.Sex;
            parameters[9].Value = model.AreaID;
            parameters[10].Value = model.Mobile;
            parameters[11].Value = model.LoginIp;
            parameters[12].Value = model.LoginTime;
            parameters[13].Value = model.LoginCount;
            parameters[14].Value = model.RegIp;
            parameters[15].Value = model.RegTime;
            parameters[16].Value = model.State;
            parameters[17].Value = model.Privacy;
            parameters[18].Value = model.Credit;
            parameters[19].Value = model.Vip;
            parameters[20].Value = model.Money;
            parameters[21].Value = model.Config;
            parameters[22].Value = model.Avatar;

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
		public bool Update(CengZai.Model.User model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update T_User set ");
            strSql.Append("Email=@Email,");
            strSql.Append("Password=@Password,");
            strSql.Append("Username=@Username,");
            strSql.Append("Nickname=@Nickname,");
            strSql.Append("Love=@Love,");
            strSql.Append("Sign=@Sign,");
            strSql.Append("Intro=@Intro,");
            strSql.Append("Birth=@Birth,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("AreaID=@AreaID,");
            strSql.Append("Mobile=@Mobile,");
            strSql.Append("LoginIp=@LoginIp,");
            strSql.Append("LoginTime=@LoginTime,");
            strSql.Append("LoginCount=@LoginCount,");
            strSql.Append("RegIp=@RegIp,");
            strSql.Append("RegTime=@RegTime,");
            strSql.Append("State=@State,");
            strSql.Append("Privacy=@Privacy,");
            strSql.Append("Credit=@Credit,");
            strSql.Append("Vip=@Vip,");
            strSql.Append("Money=@Money,");
            strSql.Append("Config=@Config,");
            strSql.Append("Avatar=@Avatar");
            strSql.Append(" where UserID=@UserID");
            SqlParameter[] parameters = {
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@Username", SqlDbType.NVarChar,50),
					new SqlParameter("@Nickname", SqlDbType.NVarChar,50),
					new SqlParameter("@Love", SqlDbType.Int,4),
					new SqlParameter("@Sign", SqlDbType.NVarChar,50),
					new SqlParameter("@Intro", SqlDbType.NVarChar,500),
					new SqlParameter("@Birth", SqlDbType.DateTime),
					new SqlParameter("@Sex", SqlDbType.Int,4),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@Mobile", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginIp", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginTime", SqlDbType.DateTime),
					new SqlParameter("@LoginCount", SqlDbType.Int,4),
					new SqlParameter("@RegIp", SqlDbType.NVarChar,50),
					new SqlParameter("@RegTime", SqlDbType.DateTime),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Privacy", SqlDbType.Int,4),
					new SqlParameter("@Credit", SqlDbType.Int,4),
					new SqlParameter("@Vip", SqlDbType.Int,4),
					new SqlParameter("@Money", SqlDbType.Int,4),
					new SqlParameter("@Config", SqlDbType.NVarChar,2000),
					new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@Avatar", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Email;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.Username;
            parameters[3].Value = model.Nickname;
            parameters[4].Value = model.Love;
            parameters[5].Value = model.Sign;
            parameters[6].Value = model.Intro;
            parameters[7].Value = model.Birth;
            parameters[8].Value = model.Sex;
            parameters[9].Value = model.AreaID;
            parameters[10].Value = model.Mobile;
            parameters[11].Value = model.LoginIp;
            parameters[12].Value = model.LoginTime;
            parameters[13].Value = model.LoginCount;
            parameters[14].Value = model.RegIp;
            parameters[15].Value = model.RegTime;
            parameters[16].Value = model.State;
            parameters[17].Value = model.Privacy;
            parameters[18].Value = model.Credit;
            parameters[19].Value = model.Vip;
            parameters[20].Value = model.Money;
            parameters[21].Value = model.Config;
            parameters[22].Value = model.UserID;
            parameters[23].Value = model.Avatar;

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
		public bool Delete(int UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_User ");
			strSql.Append(" where UserID=@UserID");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
			};
			parameters[0].Value = UserID;

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
		public bool DeleteList(string UserIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_User ");
			strSql.Append(" where UserID in ("+UserIDlist + ")  ");
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
		public CengZai.Model.User GetModelByUserIDOrEmail(int UserID,string email)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 * from T_User ");
            SqlParameter[] parameters = null;
            if (UserID > 0)
            {
                strSql.Append(" where UserID=@UserID");
                parameters = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int,4)
			    };
                parameters[0].Value = UserID;
            }
            else
            {
                strSql.Append(" where Email=@Email");
                parameters = new SqlParameter[] {
					new SqlParameter("@Email", SqlDbType.NVarChar,50)
			    };
                parameters[0].Value = email;
            }

			CengZai.Model.User model=new CengZai.Model.User();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
                return RowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CengZai.Model.User GetModelByUsername(string username)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from T_User ");
            
            strSql.Append(" where Username=@Username");
            SqlParameter[] parameters = new SqlParameter[] {
				new SqlParameter("@Username", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = username;

            CengZai.Model.User model = new CengZai.Model.User();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return RowToModel(ds.Tables[0].Rows[0]);
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
			strSql.Append("select * ");
			strSql.Append(" FROM T_User ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM T_User ");
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
			strSql.Append("select count(1) FROM T_User ");
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
				strSql.Append("order by T.UserID desc");
			}
			strSql.Append(")AS Row, T.*  from T_User T ");
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
			parameters[0].Value = "T_User";
			parameters[1].Value = "UserID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method


        /// <summary>
        /// 转Model
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public Model.User RowToModel(DataRow row)
        {
            Model.User model = new Model.User();
            if (row["UserID"] != null && row["UserID"].ToString() != "")
            {
                model.UserID = int.Parse(row["UserID"].ToString());
            }
            if (row["Email"] != null && row["Email"].ToString() != "")
            {
                model.Email = row["Email"].ToString();
            }
            if (row["Password"] != null && row["Password"].ToString() != "")
            {
                model.Password = row["Password"].ToString();
            }
            if (row["Username"] != null && row["Username"].ToString() != "")
            {
                model.Username = row["Username"].ToString();
            }
            if (row["Nickname"] != null && row["Nickname"].ToString() != "")
            {
                model.Nickname = row["Nickname"].ToString();
            }
            if (row["Love"] != null && row["Love"].ToString() != "")
            {
                model.Love = int.Parse(row["Love"].ToString());
            }
            if (row["Sign"] != null && row["Sign"].ToString() != "")
            {
                model.Sign = row["Sign"].ToString();
            }
            if (row["Intro"] != null && row["Intro"].ToString() != "")
            {
                model.Intro = row["Intro"].ToString();
            }
            if (row["Birth"] != null && row["Birth"].ToString() != "")
            {
                model.Birth = DateTime.Parse(row["Birth"].ToString());
            }
            if (row["Sex"] != null && row["Sex"].ToString() != "")
            {
                model.Sex = int.Parse(row["Sex"].ToString());
            }
            if (row["AreaID"] != null && row["AreaID"].ToString() != "")
            {
                model.AreaID = int.Parse(row["AreaID"].ToString());
            }
            if (row["Mobile"] != null && row["Mobile"].ToString() != "")
            {
                model.Mobile = row["Mobile"].ToString();
            }
            if (row["LoginIp"] != null && row["LoginIp"].ToString() != "")
            {
                model.LoginIp = row["LoginIp"].ToString();
            }
            if (row["LoginTime"] != null && row["LoginTime"].ToString() != "")
            {
                model.LoginTime = DateTime.Parse(row["LoginTime"].ToString());
            }
            if (row["LoginCount"] != null && row["LoginCount"].ToString() != "")
            {
                model.LoginCount = int.Parse(row["LoginCount"].ToString());
            }
            if (row["RegIp"] != null && row["RegIp"].ToString() != "")
            {
                model.RegIp = row["RegIp"].ToString();
            }
            if (row["RegTime"] != null && row["RegTime"].ToString() != "")
            {
                model.RegTime = DateTime.Parse(row["RegTime"].ToString());
            }
            if (row["State"] != null && row["State"].ToString() != "")
            {
                model.State = int.Parse(row["State"].ToString());
            }
            if (row["Privacy"] != null && row["Privacy"].ToString() != "")
            {
                model.Privacy = int.Parse(row["Privacy"].ToString());
            }
            if (row["Credit"] != null && row["Credit"].ToString() != "")
            {
                model.Credit = int.Parse(row["Credit"].ToString());
            }
            if (row["Vip"] != null && row["Vip"].ToString() != "")
            {
                model.Vip = int.Parse(row["Vip"].ToString());
            }
            if (row["Money"] != null && row["Money"].ToString() != "")
            {
                model.Money = int.Parse(row["Money"].ToString());
            }
            if (row["Config"] != null && row["Config"].ToString() != "")
            {
                model.Config = row["Config"].ToString();
            }
            if (row["Avatar"] != null && row["Avatar"].ToString() != "")
            {
                model.Avatar = row["Avatar"].ToString();
            }
            return model;
        }



        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListBySearch(int Top, string keyword, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM T_User ");
            strSql.Append(" WHERE (Username like @Keyword or Email like @Keyword or Nickname like @Keyword) and State>=0 ");
            strSql.Append(" order by " + filedOrder);
            SqlParameter[] sqlParams = new SqlParameter[]{
                new SqlParameter("@Keyword",  SqlDbType.NVarChar, 50)
            };
            sqlParams[0].Value = "%" +keyword + "%";
            return DbHelperSQL.Query(strSql.ToString(), sqlParams);
        }



        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="fieldOrder"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetListByPage(string strWhere, string fieldOrder, int pageSize, int pageIndex, out int totalCount)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM T_User ");

            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" Where " + strWhere);
            }

            if (string.IsNullOrEmpty(fieldOrder))
            {
                fieldOrder = "UserID DESC";
            }

            DataSet dsList = null;
            dsList = SqlHelperEx.ExecuteDatasetByPage(Config.ConnString, strSql.ToString(), fieldOrder, pageSize, pageIndex, out totalCount);
            return dsList;
        }


        /// <summary>
        /// 取朋友关系
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="type">-1=黑名单, 0=我关注的人,1=关注我的人</param>
        /// <param name="fieldOrder"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetFriendListByPage(bool isWhereFriendIDField, int whereUserID, int type, string fieldOrder, int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            string sqlSelectUserID = string.Empty;
            if (isWhereFriendIDField)
            {
                sqlSelectUserID = "select UserID from T_Friend where type=" + type + " and friendUserID=" + whereUserID;
            }
            else
            {
                sqlSelectUserID = "select FriendUserID from T_Friend where type=" + type + " and userid=" + whereUserID;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM T_User ");
            strSql.Append(@" Where UserID IN ( " + sqlSelectUserID + ") ");
            if (string.IsNullOrEmpty(fieldOrder))
            {
                fieldOrder = "Nickname ASC";
            }
            DataSet dsList = null;
            dsList = SqlHelperEx.ExecuteDatasetByPage(Config.ConnString, strSql.ToString(), fieldOrder, pageSize, pageIndex, out totalCount);
            return dsList;
        }

	}
}

