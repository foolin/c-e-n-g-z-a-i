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
                    model = dal.ToModel(dt.Rows[n]);
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
        /// 获得数据列表
        /// </summary>
        public List<CengZai.Model.Lover> GetModelList(int top, string strWhere, string fieldOrder)
        {
            DataSet ds = dal.GetList(top, strWhere, fieldOrder);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 取发送给我的单
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Model.Lover> GetReceiveList(int userID)
        {
            List<Model.Lover> list = null;
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("(BoyUserID={0} OR GirlUserID={0}) And State=0 And ApplyUserID<>{0} And Flow IN ({1})", userID, (int)LoverFlow.Apply);
            DataSet dsList = GetList(0, strWhere.ToString(), "State DESC");
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
        public Model.Lover GetMyLover(int userID)
        {
            Model.Lover myLover = null;
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("(BoyUserID={0} OR GirlUserID={0})", userID);
            strWhere.AppendFormat(" And State in (0,1)");
            DataSet dsList = GetList(0, strWhere.ToString(), "State DESC");
            if(dsList == null || dsList.Tables.Count == 0 || dsList.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            List<Model.Lover> myList = DataTableToList(dsList.Tables[0]);
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
                        && ( m.Flow == (int)Model.LoverFlow.Accept   //已经接受
                            || m.Flow == (int)Model.LoverFlow.Award    //已审核并颁发
                            || m.Flow == (int)Model.LoverFlow.UnAward  //未审核颁发
                        )
                        )
                    {
                        return true;
                    }
                    return false;
                });

            return myLover;
        }


        /// <summary>
        /// 获取已经正式颁布证书的关系
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Model.Lover GetAwardLover(int userID)
        {
            Model.Lover myLover = null;
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("(BoyUserID={0} OR GirlUserID={0})", userID);
            strWhere.AppendFormat(" And State = 1 And Flow={0}", (int)LoverFlow.Award);
            DataSet dsList = GetList(0, strWhere.ToString(), "State DESC");
            if (dsList == null || dsList.Tables.Count == 0 || dsList.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            myLover = dal.ToModel(dsList.Tables[0].Rows[0]);
            return myLover;
        }

        /// <summary>
        /// 取证件类型名字
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public string GetCertificateName(int? certificate)
        {
            if (certificate == (int)CengZai.Model.LoverCertificate.Love)
            {
                return "恋爱证";
            }
            if (certificate == (int)CengZai.Model.LoverCertificate.Marry)
            {
                return "结婚证";
            }

            return "";
        }

        /// <summary>
        /// 取两人的关系
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public string GetLoverRelation(int? certificate)
        {
            if (certificate == (int)CengZai.Model.LoverCertificate.Love)
            {
                return "恋爱";
            }
            if (certificate == (int)CengZai.Model.LoverCertificate.Marry)
            {
                return "结婚";
            }

            return "";
        }


        /// <summary>
        /// 取状态名
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public string GetFlowName(int? flow)
        {
            if (flow == (int)CengZai.Model.LoverFlow.Accept)
            {
                return "等待审核";
            }
            if (flow == (int)CengZai.Model.LoverFlow.Apply)
            {
                return "等待接受";
            }
            if (flow == (int)CengZai.Model.LoverFlow.Award)
            {
                return "审核通过";
            }
            if (flow == (int)CengZai.Model.LoverFlow.UnAccept)
            {
                return "已经拒绝";
            }
            if (flow == (int)CengZai.Model.LoverFlow.UnApply)
            {
                return "取消申请";
            }
            if (flow == (int)CengZai.Model.LoverFlow.UnAward)
            {
                return "系统退回";
            }
            if (flow == (int)CengZai.Model.LoverFlow.Abolish)
            {
                return "已注销";
            }
            return "未知";
        }
	}
}

