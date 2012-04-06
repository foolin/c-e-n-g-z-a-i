using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

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
                string domain = (CengZai.Helper.Config.ResourceDomain + "").Trim();
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
    }
}
