using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CengZai.Helper
{
    public class Util
    {
        /// <summary>
        /// 取IP
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string user_IP = "";
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                user_IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                user_IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return user_IP;
        }


        /// <summary>
        /// 取当前域名Url
        /// </summary>
        /// <returns></returns>
        public static string GetCurrDomainUrl()
        {
            HttpRequest Request = HttpContext.Current.Request;
            string domain = "";
            string urlAuthority = Request.Url.GetLeftPart(UriPartial.Authority);
            if (Request.ApplicationPath == null || Request.ApplicationPath == "/")
            {
                //直接安装在Web站点   
                domain = urlAuthority;
            }
            else
            {
                //安装在虚拟子目录下   
                domain = urlAuthority + Request.ApplicationPath;
            }
            if (domain.Length > 1 && domain.Substring(domain.Length - 1, 1) == "/")
            {
                domain = domain.Substring(0, domain.Length - 1);//去掉最后的/
            }
            return domain;
        }
    }
}
