using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace System.Web.Mvc
{

    public static class HtmlExtend
    {
        /// <summary>
        /// 扩展Html
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="index"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, int pageSize, int totalCount, string pageTag, int perSideNum)
        {
            var queryString = helper.ViewContext.HttpContext.Request.QueryString;
            
            pageTag = string.IsNullOrEmpty(pageTag) ? "page" : pageTag;
            int pageIndex = 1; //当前页
            int.TryParse(queryString[pageTag], out pageIndex); //与相应的QueryString绑定
            int pageCount = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            StringBuilder str = new StringBuilder();
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 20 : pageSize;

            RouteData routeData = helper.ViewContext.RouteData;
            //你可能还要获取action
            string url = helper.ViewContext.HttpContext.Request.Url.PathAndQuery;
            url = System.Text.RegularExpressions.Regex.Replace(url, ("&{0,1}" + pageTag + "=\\d*").ToString(), ""
                , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //string action = routeData.Values["action"].ToString();
            //string controller = routeData.Values["controller"].ToString();
            StringBuilder html = new StringBuilder();
            html.Append("<ul>");
            //首页、上一页
            if (pageIndex <= 1)
            {
                html.AppendFormat("<li class=\"active\"><a href=\"#\">|<</a></li>");
                html.AppendFormat("<li class=\"active\"><a href=\"#\"><</a></li>");
            }
            else
            {
                html.AppendFormat("<li>{0}</li>", ParsePageLink(url, pageTag, "|<", 1));
                html.AppendFormat("<li>{0}</li>", ParsePageLink(url, pageTag, "<", pageIndex - 1));
            }
            int margin = pageIndex - (perSideNum + 1);
            if (margin > 0)
            {
                margin = 0;
            }
            if (perSideNum >= 0)
            {
                //当前页的前面数字
                if (pageIndex > 1)
                {
                    for (int i = pageIndex - perSideNum; i < pageIndex; i++)
                    {
                        if (i > 0)
                        {
                            html.AppendFormat("<li>{0}</li>", ParsePageLink(url, pageTag, i.ToString(), i));
                        }
                    }
                }
                //当前页
                html.AppendFormat("<li class=\"active\"><a href=\"#\">{0}</a></li>", pageIndex);
                //当前页后面数字
                if (pageIndex < pageCount)
                {
                    for (int j = pageIndex + 1; (j <= pageCount) && j <= (pageIndex + perSideNum - margin); j++)
                    {
                        html.AppendFormat("<li>{0}</li>", ParsePageLink(url, pageTag, j.ToString(), j));
                    }
                }
            }
            //下一页、尾页
            if (pageIndex >= pageCount)
            {
                html.AppendFormat("<li class=\"active\"><a href=\"#\">></a></li>");
                html.AppendFormat("<li class=\"active\"><a href=\"#\">>|</a></li>");
            }
            else
            {
                html.AppendFormat("<li>{0}</li>", ParsePageLink(url, pageTag, ">", pageIndex + 1));
                html.AppendFormat("<li>{0}</li>", ParsePageLink(url, pageTag, ">|", pageCount));
            }
            //前后加<ul>
            
            html.Append("</ul>");

            //<ul> 
            //  <li class="disabled"><a href="#">&laquo;</a></li> 
            //  <li class="active"><a href="#">1</a></li> 
            //  <li><a href="#">2</a></li> 
            //  <li><a href="#">3</a></li> 
            //  <li><a href="#">4</a></li> 
            //  <li><a href="#">&raquo;</a></li> 
            //</ul> 
            return MvcHtmlString.Create(html.ToString());
        }


        /// <summary>
        /// 规格化网址
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageTitle"></param>
        /// <returns></returns>
        private static string ParsePageLink(string url, string pageTag, string pageName, int pageNum)
        {
            //判断是否有参数
            if (url.IndexOf('?') != -1)
            {
                return (" <a href=\"" + url + "&" + pageTag + "=" + pageNum + "\">" + pageName + "</a> ").Replace("?&", "?");
            }

            return " <a href=\"" + url + "?" + pageTag + "=" + pageNum + "\">" + pageName + "</a> ";
        }


        /// <summary>
        /// 创建分页链接
        /// </summary>
        /// <param name="helper">HtmlHelper类</param>
        /// <param name="startPage">开始页 (多数情况下是 1)</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="totalPages">总页数</param>
        /// <param name="pagesToShow">前后显示的页数</param>
        public static MvcHtmlString Pagers(this HtmlHelper helper, int startPage,
            int currentPage, int totalPages, int pagesToShow)
        {
            RouteData routeData = helper.ViewContext.RouteData;
            //你可能还要获取action
            //routeData.Values["action"].ToString();
            string controller = routeData.Values["controller"].ToString();
            StringBuilder html = new StringBuilder();
            //创建从第一页到最后一页的列表
            html = Enumerable.Range(startPage, totalPages)
            .Where(i => (currentPage - pagesToShow) < i & i < (currentPage + pagesToShow))
            .Aggregate(new StringBuilder(@"<div class=""pagination""><span>共" + totalPages + "页</span>"), (seed, page) =>
            {
                //当前页
                if (page == currentPage)
                    seed.AppendFormat("<span>{0}</span>", page);
                else
                {
                    //第一页时显示：domain/archives
                    if (page == 1)
                    {
                        seed.AppendFormat("<a href=\"/{0}\">{1}</a>", controller.ToLower(), page);
                    }
                    else
                    {
                        seed.AppendFormat("<a href=\"/{0}/{1}\">{1}</a>", controller.ToLower(), page);
                    }
                }
                return seed;
            });
            html.Append(@"</div>");
            return MvcHtmlString.Create(html.ToString());
        }



        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin(this HtmlHelper helper)
        {
            CengZai.Model.User user = null;
            try
            {
                user = System.Web.HttpContext.Current.Session["LOGIN_USER"] as CengZai.Model.User;
            }
            catch { }
            return (user != null);
        }


        /// <summary>
        /// 取邮箱域名
        /// </summary>
        /// <param name="email"></param>
        public static string GetEmailDomain(this HtmlHelper helper, string email)
        {
            if (string.IsNullOrEmpty(email) || email.IndexOf('@') == -1)
            {
                return email;
            }

            string domain = email.Substring(email.IndexOf('@') + 1);
            return domain;
        }

    }
}

