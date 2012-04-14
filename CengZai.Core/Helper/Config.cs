using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web;

namespace CengZai.Helper
{
    public partial class Config
    {


        /// <summary>
        /// 站名
        /// </summary>
        public static string SiteName
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteName"] + "";
            }
        }

        /// <summary>
        /// 网站域名
        /// </summary>
        public static string SiteHost
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteHost"] + "";
            }
        }

        /// <summary>
        /// 网站标语
        /// </summary>
        public static string SiteSlogan
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteSlogan"] + "";
            }
        }

        /// <summary>
        /// 网站标语
        /// </summary>
        public static string SiteKeyword
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteKeyword"] + "";
            }
        }

        /// <summary>
        /// 网站标语
        /// </summary>
        public static string SiteDescription
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteDescription"] + "";
            }
        }


        /// <summary>
        /// js/css文件资源文件主机地址
        /// </summary>
        public static string ResourceHost
        {
            get
            {
                return ConfigurationManager.AppSettings["ResourceHost"] + "";
            }
        }



        /// <summary>
        /// 站点主域名
        /// </summary>
        public static string SiteDomain
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteDomain"] + "";
            }
        }

        /// <summary>
        /// 博客主域名
        /// </summary>
        public static string BlogDomain
        {
            get
            {
                return ConfigurationManager.AppSettings["BlogDomain"] + "";
            }
        }

        /// <summary>
        /// 特殊用户名博客名称，多用“|”进行分割
        /// </summary>
        public static string VipBlogName
        {
            get
            {
                return ConfigurationManager.AppSettings["VipBlogName"] + "";
            }
        }

        /// <summary>
        /// 是否打开二级域名
        /// </summary>
        public static int OpenBlogDomain
        {
            get
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(ConfigurationManager.AppSettings["OpenBlogDomain"]);
                }
                catch { }
                return val;
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
        /// 禁止用户注册的域名/用户名
        /// </summary>
        public static string ProtectUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["ProtectUsername"] + "";
            }
        }

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


        /// <summary>
        /// 情侣登记限制：
        /// 0=自动审核通过，
        /// 1=人工审核通过，
        /// </summary>
        public static int LoverLimit
        {
            get
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(ConfigurationManager.AppSettings["LoverLimit"]);
                }
                catch { }
                return val;
            }
        }


        /// <summary>
        /// 头像宽度，单位像素
        /// </summary>
        public static int AvatarWidth
        {
            get
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(ConfigurationManager.AppSettings["AvatarWidth"]);
                }
                catch { }
                if (val <= 0) val = 150;
                return val;
            }
        }


        /// <summary>
        /// 头像高度，单位像素
        /// </summary>
        public static int AvatarHeight
        {
            get
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(ConfigurationManager.AppSettings["AvatarHeight"]);
                }
                catch { }
                if (val <= 0) val = 150;
                return val;
            }
        }

        /// <summary>
        /// 证书头像宽度
        /// </summary>
        public static int CertificateAvatarWidth
        {
            get
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(ConfigurationManager.AppSettings["CertificateAvatarWidth"]);
                }
                catch { }
                if (val <= 0) val = 180;
                return val;
            }
        }


        /// <summary>
        /// 证书头像高度，单位像素
        /// </summary>
        public static int CertificateAvatarHeight
        {
            get
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(ConfigurationManager.AppSettings["CertificateAvatarHeight"]);
                }
                catch { }
                if (val <= 0) val = 120;
                return val;
            }
        }

        /// <summary>
        /// 上传图片最大宽，单位像素
        /// </summary>
        public static int UploadImageMaxWidth
        {
            get
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(ConfigurationManager.AppSettings["UploadImageMaxWidth"]);
                }
                catch { }
                return val;
            }
        }


        /// <summary>
        /// 上传图最大高，单位像素
        /// </summary>
        public static int UploadImageMaxHeight
        {
            get
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(ConfigurationManager.AppSettings["UploadImageMaxHeight"]);
                }
                catch { }
                return val;
            }
        }

        /// <summary>
        /// 邀请朋友，奖励（正）或者扣除（负数）多少个积分
        /// </summary>
        public static int InviteCredit
        {
            get
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(ConfigurationManager.AppSettings["InviteCredit"]);
                }
                catch { }
                return val;
            }
        }


        /// <summary>
        /// 缓存键前缀~
        /// </summary>
        public static string CacheKeyPrefix
        {
            get
            {
                return ConfigurationManager.AppSettings["CacheKeyPrefix"] + "";
            }
        }


        /// <summary>
        /// 上传文件路径~
        /// </summary>
        public static string UploadMapPath
        {
            get
            {
                string path = ConfigurationManager.AppSettings["UploadMapPath"] + "";
                try
                {
                    Util.EnsureDir(Util.MapPath(path));
                }
                catch (Exception ex)
                {
                    Log.Error("创建上传文件目录MapUploadPath：" + path + "出错，异常：" + ex.Message);
                }

                return path;
            }
        }


        /// <summary>
        /// 显示文件路径~
        /// </summary>
        public static string UploadHttpPath
        {
            get
            {
                return ConfigurationManager.AppSettings["UploadHttpPath"] + "";
            }
        }


        /// <summary>
        /// 分页条数
        /// </summary>
        public static int PageSize
        {
            get
            {
                int pageSize = 0;
                try
                {
                    pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"] + "");
                }
                catch (Exception ex)
                {
                    Log.Error("Config.PageSize异常", ex);
                }
                if (pageSize <= 0)
                {
                    pageSize = 20;
                }
                return pageSize;
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
