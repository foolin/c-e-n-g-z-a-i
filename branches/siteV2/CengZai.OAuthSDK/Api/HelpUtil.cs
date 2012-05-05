using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace CengZai.OAuthSDK.Api
{
    public class HelpUtil
    {
        /// <summary>
        /// 字符串转参数数组
        /// </summary>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        public static NameValueCollection StringToParameters(string strParameters)
        {
            if (string.IsNullOrEmpty(strParameters))
            {
                return null;
            }
            NameValueCollection nvc = null;
            string[] arrKeyVal = strParameters.Split(new char[]{'&'}, StringSplitOptions.RemoveEmptyEntries);
            if (arrKeyVal.Length > 0)
            {
                nvc = new NameValueCollection();
                foreach (string kv in arrKeyVal)
                {
                    string[] arrKv = kv.Split(new char[] { '=' });
                    if (arrKv.Length >= 2)
                    {
                        nvc.Add(arrKv[0], arrKv[1]);
                    }
                }
            }
            return nvc;
        }


        /// <summary>
        /// 参数转字符串
        /// </summary>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        public static string ParametersToString(NameValueCollection nvcParameters)
        {
            if (nvcParameters == null || nvcParameters.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder strParameters = new StringBuilder();
            foreach (string key in nvcParameters.Keys)
            {
                if (strParameters.Length > 0)
                {
                    strParameters.Append("&");
                }
                strParameters.Append(key + "=" + nvcParameters[key]);
            }
            return strParameters.ToString();
        }


    }
}
