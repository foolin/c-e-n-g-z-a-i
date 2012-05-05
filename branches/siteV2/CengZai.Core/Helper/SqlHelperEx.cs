using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CengZai.Helper
{
    public class SqlHelperEx
    {
        /// <summary>
        /// 执行分页存储过程
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="strOrder"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataSet ExecuteDatasetByPage(string connectionString, string strSql, string fieldOrder, int pageSize, int pageIndex, out int totalCount)
        {
            DataSet dsList = null;
            SqlParameter records = new SqlParameter("@TotalCount", SqlDbType.Int, 32);
            records.Direction = ParameterDirection.Output;

            SqlParameter[] sqlParams = new SqlParameter[]{
                new SqlParameter("@Sql", SqlDbType.NVarChar, 4000),
                new SqlParameter("@Sort", SqlDbType.NVarChar, 500),
                new SqlParameter("@PageSize", SqlDbType.Int, 32),
                new SqlParameter("@PageIndex", SqlDbType.Int, 32),
                records
            };
            sqlParams[0].Value = strSql;
            sqlParams[1].Value = fieldOrder;
            sqlParams[2].Value = pageSize;
            sqlParams[3].Value = pageIndex;
            sqlParams[4].Direction = ParameterDirection.Output;

            dsList = SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, "SP_Page", sqlParams);
            totalCount = (int)records.Value;

            return dsList;
        }
    }
}
