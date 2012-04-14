using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace System.Web.Mvc
{
    public static class HtmlExtendForApp
    {
        /// <summary>
        /// 取证书名称
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static string GetCertificateName(this HtmlHelper helper, int? certificate)
        {
            return new CengZai.BLL.Lover().GetCertificateName(certificate);
        }

        /// <summary>
        /// 取状态节点名称
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="flow"></param>
        /// <returns></returns>
        public static string GetLoverFlowName(this HtmlHelper helper, int? flow)
        {
            return new CengZai.BLL.Lover().GetFlowName(flow);
        }


        /// <summary>
        /// 取天数
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetDaysByNow(this HtmlHelper helper, DateTime? date)
        {
            int days = 0;
            try
            {
                days = (DateTime.Now - Convert.ToDateTime(date)).Days;
            }
            catch { }
            return days;
        }


        /// <summary>
        /// 重写绝对路径
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString HttpActionLink(this HtmlHelper helper, string linkText, string actionName, string controllerName)
        {
            return helper.HttpActionLink(linkText, actionName, controllerName, null, null);
        }

        /// <summary>
        /// 重写绝对路径
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString HttpActionLink(this HtmlHelper helper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            if (CengZai.Helper.Config.OpenBlogDomain == 1)
            {
                Dictionary<string, object> dicHtmlAttributes = htmlAttributes as Dictionary<string, object>;
                return helper.ActionLink(linkText, actionName, controllerName, "http", CengZai.Helper.Config.SiteDomain, ""
                    , new RouteValueDictionary(routeValues), dicHtmlAttributes);
            }
            else
            {
                return helper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }
        }
    }
}
