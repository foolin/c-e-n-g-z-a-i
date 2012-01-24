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
			strSql.Append("Email,Password,Nickname,Sign,Intro,Birth,Sex,AreaID,Mobile,LoginIp,LoginTime,LoginCount,RegIp,RegTime,State,Private,Domain)");
			strSql.Append(" values (");
			strSql.Append("@Email,@Password,@Nickname,@Sign,@Intro,@Birth,@Sex,@AreaID,@Mobile,@LoginIp,@LoginTime,@LoginCount,@RegIp,@RegTime,@State,@Private,@Domain)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@Nickname", SqlDbType.NVarChar,50),
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
					new SqlParameter("@Private", SqlDbType.Int,4),
					new SqlParameter("@Domain", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Email;
			parameters[1].Value = model.Password;
			parameters[2].Value = model.Nickname;
			parameters[3].Value = model.Sign;
			parameters[4].Value = model.Intro;
			parameters[5].Value = model.Birth;
			parameters[6].Value = model.Sex;
			parameters[7].Value = model.AreaID;
			parameters[8].Value = model.Mobile;
			parameters[9].Value = model.LoginIp;
			parameters[10].Value = model.LoginTime;
			parameters[11].Value = model.LoginCount;
			parameters[12].Value = model.RegIp;
			parameters[13].Value = model.RegTime;
			parameters[14].Value = model.State;
			parameters[15].Value = model.Private;
			parameters[16].Value = model.Domain;

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
			strSql.Append("Nickname=@Nickname,");
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
			strSql.Append("Private=@Private,");
			strSql.Append("Domain=@Domain");
			strSql.Append(" where UserID=@UserID");
			SqlParameter[] parameters = {
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@Nickname", SqlDbType.NVarChar,50),
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
					new SqlParameter("@Private", SqlDbType.Int,4),
					new SqlParameter("@Domain", SqlDbType.NVarChar,50),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
			parameters[0].Value = model.Email;
			parameters[1].Value = model.Password;
			parameters[2].Value = model.Nickname;
			parameters[3].Value = model.Sign;
			parameters[4].Value = model.Intro;
			parameters[5].Value = model.Birth;
			parameters[6].Value = model.Sex;
			parameters[7].Value = model.AreaID;
			parameters[8].Value = model.Mobile;
			parameters[9].Value = model.LoginIp;
			parameters[10].Value = model.LoginTime;
			parameters[11].Value = model.LoginCount;
			parameters[12].Value = model.RegIp;
			parameters[13].Value = model.RegTime;
			parameters[14].Value = model.State;
			parameters[15].Value = model.Private;
			parameters[16].Value = model.Domain;
			parameters[17].Value = model.UserID;

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
			strSql.Append("select  top 1 UserID,Email,Password,Nickname,Sign,Intro,Birth,Sex,AreaID,Mobile,LoginIp,LoginTime,LoginCount,RegIp,RegTime,State,Private,Domain from T_User ");
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
				if(ds.Tables[0].Rows[0]["UserID"]!=null && ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Email"]!=null && ds.Tables[0].Rows[0]["Email"].ToString()!="")
				{
					model.Email=ds.Tables[0].Rows[0]["Email"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Password"]!=null && ds.Tables[0].Rows[0]["Password"].ToString()!="")
				{
					model.Password=ds.Tables[0].Rows[0]["Password"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Nickname"]!=null && ds.Tables[0].Rows[0]["Nickname"].ToString()!="")
				{
					model.Nickname=ds.Tables[0].Rows[0]["Nickname"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Sign"]!=null && ds.Tables[0].Rows[0]["Sign"].ToString()!="")
				{
					model.Sign=ds.Tables[0].Rows[0]["Sign"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Intro"]!=null && ds.Tables[0].Rows[0]["Intro"].ToString()!="")
				{
					model.Intro=ds.Tables[0].Rows[0]["Intro"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Birth"]!=null && ds.Tables[0].Rows[0]["Birth"].ToString()!="")
				{
					model.Birth=DateTime.Parse(ds.Tables[0].Rows[0]["Birth"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sex"]!=null && ds.Tables[0].Rows[0]["Sex"].ToString()!="")
				{
					model.Sex=int.Parse(ds.Tables[0].Rows[0]["Sex"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AreaID"]!=null && ds.Tables[0].Rows[0]["AreaID"].ToString()!="")
				{
					model.AreaID=int.Parse(ds.Tables[0].Rows[0]["AreaID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Mobile"]!=null && ds.Tables[0].Rows[0]["Mobile"].ToString()!="")
				{
					model.Mobile=ds.Tables[0].Rows[0]["Mobile"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LoginIp"]!=null && ds.Tables[0].Rows[0]["LoginIp"].ToString()!="")
				{
					model.LoginIp=ds.Tables[0].Rows[0]["LoginIp"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LoginTime"]!=null && ds.Tables[0].Rows[0]["LoginTime"].ToString()!="")
				{
					model.LoginTime=DateTime.Parse(ds.Tables[0].Rows[0]["LoginTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LoginCount"]!=null && ds.Tables[0].Rows[0]["LoginCount"].ToString()!="")
				{
					model.LoginCount=int.Parse(ds.Tables[0].Rows[0]["LoginCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegIp"]!=null && ds.Tables[0].Rows[0]["RegIp"].ToString()!="")
				{
					model.RegIp=ds.Tables[0].Rows[0]["RegIp"].ToString();
				}
				if(ds.Tables[0].Rows[0]["RegTime"]!=null && ds.Tables[0].Rows[0]["RegTime"].ToString()!="")
				{
					model.RegTime=DateTime.Parse(ds.Tables[0].Rows[0]["RegTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["State"]!=null && ds.Tables[0].Rows[0]["State"].ToString()!="")
				{
					model.State=int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Private"]!=null && ds.Tables[0].Rows[0]["Private"].ToString()!="")
				{
					model.Private=int.Parse(ds.Tables[0].Rows[0]["Private"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Domain"]!=null && ds.Tables[0].Rows[0]["Domain"].ToString()!="")
				{
					model.Domain=ds.Tables[0].Rows[0]["Domain"].ToString();
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
			strSql.Append("select UserID,Email,Password,Nickname,Sign,Intro,Birth,Sex,AreaID,Mobile,LoginIp,LoginTime,LoginCount,RegIp,RegTime,State,Private,Domain ");
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
			strSql.Append(" UserID,Email,Password,Nickname,Sign,Intro,Birth,Sex,AreaID,Mobile,LoginIp,LoginTime,LoginCount,RegIp,RegTime,State,Private,Domain ");
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
	}
}

