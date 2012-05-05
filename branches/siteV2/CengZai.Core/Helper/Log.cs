using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.IO;

/// <summary>
///Log 的摘要说明
/// </summary>
namespace CengZai.Helper
{
    public class Log
    {
        private static readonly log4net.ILog logger = LogManager.GetLogger(typeof(Log));
        private static readonly log4net.ILog debuglogger = LogManager.GetLogger("DebugLogger");

        /// <summary>
        /// 打印错误信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public static void Error(string info, Exception ex)
        {
            Error(info + "\n" + ex.Message + "\n" + ex.StackTrace);
        }

        /// <summary>
        /// 打印错误信息
        /// </summary>
        /// <param name="info"></param>
        public static void Error(string info)
        {
            string logFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\Error.txt";
            try
            {
                logger.Error(info);
            }
            catch
            {
                try
                {
                    using (StreamWriter swError = new StreamWriter(logFilePath, true))
                    {
                        swError.WriteLine(DateTime.Now.ToLongTimeString() + ":" + info + "");
                    }
                }
                catch
                {
                    return;
                }
            }
        }


        /// <summary>
        /// 打印调试信息
        /// </summary>
        /// <param name="info"></param>
        public static void Info(string info)
        {
            string logFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\DebugLog.txt";
            try
            {
                debuglogger.Error(info);
            }
            catch
            {
                try
                {
                    using (StreamWriter swError = new StreamWriter(logFilePath, true))
                    {
                        swError.WriteLine(DateTime.Now.ToLongTimeString() + ":" + info + "");
                    }
                }
                catch
                {
                    return;
                }
            }
        }


    }
}
