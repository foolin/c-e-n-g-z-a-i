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

        public static void AddErrorInfo(string info)
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


        public static void AddDebugInfo(string info)
        {
            string logFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\DebugLog\\Error.txt";
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
