using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CengZai.Helper
{
    public partial class Config
    {
        /// <summary>
        /// 网站名：曾在网
        /// </summary>
        public static string SiteName
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteName"] + "";
            }
        }

        /// <summary>
        /// 网站域名：cengzai.com
        /// </summary>
        public static string SiteDomain
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteDomain"] + "";
            }
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConnString"] + "";
            }
        }


        /// <summary>
        /// 密钥
        /// </summary>
        public static string SecrectKey
        {
            get
            {
                string key = ConfigurationManager.AppSettings["SecrectKey"] + "";
                if (string.IsNullOrEmpty(key))
                {
                    key = "LiufuLing";
                }
                return key;
            }
        }


        /************ 邮箱相关配置 ***********/

        //From
        /// <summary>
        /// 发送邮箱地址
        /// </summary>
        public static string MailFrom
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MailFrom"] + "";
            }
        }

        //MailUserName
        /// <summary>
        /// 邮箱用户名
        /// </summary>
        public static string MailUserName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MailUserName"] + "";
            }
        }


        //MailPassword
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public static string MailPassword
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MailPassword"] + "";
            }
        }

        //MailSmtpServer
        /// <summary>
        /// 邮箱服务地址
        /// </summary>
        public static string MailSmtpServer
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MailSmtpServer"] + "";
            }
        }

        /// <summary>
        /// 邮箱端口号
        /// </summary>
        public static int MailSmtpPort
        {
            get
            {
                int port = 25;
                try
                {
                    port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MailSmtpPort"]);
                }
                catch { }

                return port;
            }
        }


    }
}
