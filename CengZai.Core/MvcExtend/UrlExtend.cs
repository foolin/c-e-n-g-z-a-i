using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace System.Web.Mvc
{
    public static class UrlExtend
    {

        /// <summary>
        /// 取资源文件Url
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static string GetResourceUrl(this UrlHelper helper, string relativePath)
        {
            try
            {
                string domain = (CengZai.Helper.Config.ResourceHost + "").Trim();
                if (string.IsNullOrEmpty(domain))
                {
                    domain = CengZai.Helper.Util.GetCurrDomainUrl();
                }
                if (domain.Length > 0 && domain.Substring(domain.Length - 1) == "/")
                {
                    domain = domain.Substring(0, domain.Length - 1);
                }
                if (!string.IsNullOrEmpty(relativePath))
                {
                    relativePath = relativePath.Trim();
                    if (relativePath.Length > 0 && relativePath.Substring(0, 1) == "/")
                    {
                        relativePath = relativePath.Substring(1);
                    }
                }
                relativePath = domain + "/" + relativePath;
            }
            catch { }
            return relativePath;
        }




        /// <summary>
        /// 取Http路径
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetUploadUrl(this UrlHelper helper, string path)
        {
            string combilePath = CengZai.Helper.Config.UploadHttpPath + "/" + path;
            return Regex.Replace(combilePath, "([^:])//", "$1/");
        }


        /// <summary>
        /// 获取上传图片Url
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string GetAvatarUrl(this UrlHelper helper, string imagePath)
        {
            return CengZai.Helper.Util.GetAvatarUrl(imagePath);
        }


        /// <summary>
        /// 取绝对路径
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string HttpAction(this UrlHelper helper, string actionName, string controllerName)
        {
            return helper.HttpAction(actionName, controllerName, null);
        }

        /// <summary>
        /// 取绝对路径
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string HttpAction(this UrlHelper helper, string actionName, string controllerName, object routeValues)
        {
            RouteValueDictionary rvdRouteValues = new RouteValueDictionary(routeValues);
            if (CengZai.Helper.Config.OpenBlogDomain == 1)
            {
                return helper.Action(actionName, controllerName, rvdRouteValues, "http", CengZai.Helper.Config.SiteDomain);
            }
            else
            {
                return helper.Action(actionName, controllerName, rvdRouteValues);
            }
        }

        /// <summary>
        /// 取某用户博客地址
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static string BlogUrl(this UrlHelper helper, string username)
        {
            if (CengZai.Helper.Config.OpenBlogDomain == 1)
            {
                int port = helper.RequestContext.HttpContext.Request.Url.Port;
                string strPort = "";
                if(port != 80)
                {
                    strPort = ":" + port;
                }
                return "http://" + username + "." + CengZai.Helper.Config.BlogDomain + strPort;
            }
            else
            {
                return CengZai.Helper.Config.SiteHost + helper.Action("Blog", "Blog", new { username=username });
            }
        }

    }
}
