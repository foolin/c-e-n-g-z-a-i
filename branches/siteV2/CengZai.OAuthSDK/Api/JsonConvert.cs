using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace CengZai.OAuthSDK.Api
{

    /// <summary>
    /// JsonConvert，序列化Object数据
    /// 
    /// Author:Foolin
    /// Date: 2011.11.14
    /// </summary>
    public class JsonConvert
    {
        /// <summary>
        /// 序列化成Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T t)
        {
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ds.WriteObject(ms, t);

            string strReturn = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return strReturn;
        }


        /// <summary>
        /// 反序列化成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string strJson) where T : class
        {
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(strJson));

            return ds.ReadObject(ms) as T;
        }


        /// <summary>
        /// 序列化，主要针对web，会解析对象属性
        /// </summary>
        /// <param name="obj"></param>
        public static string JavascriptSerialize(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }


        /// <summary>
        /// 反序列化，主要针对web，会解析对象属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T JavascriptDeserialize<T>(string strJson) where T : class
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(strJson);
        }

    }


}
