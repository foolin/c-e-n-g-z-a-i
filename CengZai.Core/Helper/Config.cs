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

        /*********** 相关开关配置 ************/
        /// <summary>
        /// 登录限制：
        /// 0=非锁定注册用户可登录，
        /// 1=仅激活用户可登录，
        /// 2=所有用户禁止登录，
        /// -1=不作限制，所有用户可以登录（包括锁定）
        /// </summary>
        public static int LoginLimit
        {
            get
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(ConfigurationManager.AppSettings["LoginLimit"]);
                }
                catch { }
                return val;
            }
        }


        /// <summary>
        /// 注册限制：
        /// 0=开放所有人注册，
        /// 1=仅有邀请码用户可以注册，
        /// 2=禁止注册，
        /// </summary>
        public static int RegisterLimit
        {
            get
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(ConfigurationManager.AppSettings["RegisterLimit"]);
                }
                catch { }
                return val;
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
