using System;
using System.Data;
using System.Collections.Generic;
using CengZai.Helper;
using CengZai.Model;
using System.Text;
namespace CengZai.BLL
{
	/// <summary>
	/// Lover
	/// </summary>
	public partial class Lover
	{
		private readonly CengZai.DAL.Lover dal=new CengZai.DAL.Lover();
		public Lover()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int LoverID)
		{
			return dal.Exists(LoverID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(CengZai.Model.Lover model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CengZai.Model.Lover model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int LoverID)
		{
			
			return dal.Delete(LoverID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string LoverIDlist )
		{
			return dal.DeleteList(LoverIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CengZai.Model.Lover GetModel(int LoverID)
		{
			
			return dal.GetModel(LoverID);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CengZai.Model.Lover> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CengZai.Model.Lover> DataTableToList(DataTable dt)
		{
			List<CengZai.Model.Lover> modelList = new List<CengZai.Model.Lover>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CengZai.Model.Lover model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CengZai.Model.Lover();
					if(dt.Rows[n]["LoverID"]!=null && dt.Rows[n]["LoverID"].ToString()!="")
					{
						model.LoverID=int.Parse(dt.Rows[n]["LoverID"].ToString());
					}
					if(dt.Rows[n]["Avatar"]!=null && dt.Rows[n]["Avatar"].ToString()!="")
					{
					model.Avatar=dt.Rows[n]["Avatar"].ToString();
					}
					if(dt.Rows[n]["BoyUserID"]!=null && dt.Rows[n]["BoyUserID"].ToString()!="")
					{
						model.BoyUserID=int.Parse(dt.Rows[n]["BoyUserID"].ToString());
					}
					if(dt.Rows[n]["GirlUserID"]!=null && dt.Rows[n]["GirlUserID"].ToString()!="")
					{
						model.GirlUserID=int.Parse(dt.Rows[n]["GirlUserID"].ToString());
					}
					if(dt.Rows[n]["BoyOath"]!=null && dt.Rows[n]["BoyOath"].ToString()!="")
					{
					model.BoyOath=dt.Rows[n]["BoyOath"].ToString();
					}
					if(dt.Rows[n]["GirlOath"]!=null && dt.Rows[n]["GirlOath"].ToString()!="")
					{
					model.GirlOath=dt.Rows[n]["GirlOath"].ToString();
					}
					if(dt.Rows[n]["Certificate"]!=null && dt.Rows[n]["Certificate"].ToString()!="")
					{
						model.Certificate=int.Parse(dt.Rows[n]["Certificate"].ToString());
					}
					if(dt.Rows[n]["JoinDate"]!=null && dt.Rows[n]["JoinDate"].ToString()!="")
					{
						model.JoinDate=DateTime.Parse(dt.Rows[n]["JoinDate"].ToString());
					}
					if(dt.Rows[n]["ApplyUserID"]!=null && dt.Rows[n]["ApplyUserID"].ToString()!="")
					{
						model.ApplyUserID=int.Parse(dt.Rows[n]["ApplyUserID"].ToString());
					}
					if(dt.Rows[n]["ApplyTime"]!=null && dt.Rows[n]["ApplyTime"].ToString()!="")
					{
						model.ApplyTime=DateTime.Parse(dt.Rows[n]["ApplyTime"].ToString());
					}
					if(dt.Rows[n]["Flow"]!=null && dt.Rows[n]["Flow"].ToString()!="")
					{
						model.Flow=int.Parse(dt.Rows[n]["Flow"].ToString());
					}
                    if (dt.Rows[n]["State"] != null && dt.Rows[n]["State"].ToString() != "")
                    {
                        model.State = int.Parse(dt.Rows[n]["State"].ToString());
                    }
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method

        /// <summary>
        /// 获取用户的爱巢
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="isUse">是否正在使用：false=已经失效，true=正在使用</param>
        /// <returns></returns>
        public List<Model.Lover> GetLoverList(int userID, bool isUse)
        {
            List<Model.Lover> list = null;
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("(BoyUserID={0} OR GirlUserID={0})", userID);
            if (isUse)
            {
                strWhere.AppendFormat(" And State in (0,1)");
            }
            DataSet dsList = GetList(strWhere.ToString());
            if (dsList != null && dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                list = DataTableToList(dsList.Tables[0]);
            }
            return list;
        }

        /// <summary>
        /// 获取我的爱人
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Model.Lover GetLover(int userID)
        {
            Model.Lover myLover = null;
            List<Model.Lover> myList = GetLoverList(userID, true);   //找出未失效的单
            if (myList.Count > 0)
            {
                //判断是否有申请权限：
                //1.作为申请者：如果已经有申请了
                //2.作为接受者：如果已经有接受
                myLover = myList.Find(delegate(Model.Lover m)
                 {
                     //申请者
                     if (m.ApplyUserID == userID)
                     {  
                         return true;
                     }
                     //被接收者
                     if (m.ApplyUserID != userID
                         && (m.Flow == (int)Model.LoverFlow.Accept
                             || m.Flow == (int)Model.LoverFlow.Award
                             || m.Flow == (int)Model.LoverFlow.UnAward
                         )
                         )
                     {
                         return true;
                     }
                     return false;
                 });
            }

            return myLover;
        }

        
	}
}

