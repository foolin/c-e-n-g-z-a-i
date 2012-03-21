using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;
using System.Collections;

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
            pageIndex = (pageIndex < 1 || pageIndex > pageCount) ? 1 : pageIndex;
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
                html.AppendFormat("<li class=\"active\"><a>|<</a></li>");
                html.AppendFormat("<li class=\"active\"><a><</a></li>");
            }
            else
            {
                html.AppendFormat("<li>{0}</li>", ParsePageLink(url, pageTag, "|<", 1));
                html.AppendFormat("<li>{0}</li>", ParsePageLink(url, pageTag, "<", pageIndex - 1));
            }
            int margin = pageIndex - (perSideNum + 1);  //数字左边偏移，如果为负，则右边相应加差额
            if (margin > 0)
            {
                if ((pageIndex + perSideNum + 1) > pageCount)
                {
                    margin = (pageIndex + perSideNum) - pageCount;  //数字右边偏移，如果为正，则左边相应加差额
                }
                else
                {
                    margin = 0;
                }
            }
            if (perSideNum >= 0)
            {
                //当前页的前面数字
                if (pageIndex > 1)
                {
                    var mar = 0;
                    if (margin > 0)
                    {
                        mar = margin;
                    }
                    for (int i = pageIndex - (perSideNum + mar); i < pageIndex; i++)
                    {
                        if (i > 0)
                        {
                            html.AppendFormat("<li>{0}</li>", ParsePageLink(url, pageTag, i.ToString(), i));
                        }
                    }
                }
                //当前页
                html.AppendFormat("<li class=\"active\"><a>{0}</a></li>", pageIndex);
                //当前页后面数字
                if (pageIndex < pageCount)
                {
                    var mar = 0;
                    if (margin < 0)
                    {
                        mar = -margin;
                    }
                    for (int j = pageIndex + 1; (j <= pageCount) && j <= (pageIndex + perSideNum + mar); j++)
                    {
                        html.AppendFormat("<li>{0}</li>", ParsePageLink(url, pageTag, j.ToString(), j));
                    }
                }
            }
            //下一页、尾页
            if (pageIndex >= pageCount)
            {
                html.AppendFormat("<li class=\"active\"><a>></a></li>");
                html.AppendFormat("<li class=\"active\"><a>>|</a></li>");
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


        /// <summary>
        /// 获取移除Html内容
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetRemoveHtmlContent(this HtmlHelper helper, string content, int length)
        {
            content = CengZai.Helper.Util.RemoveHtml(content) + "";
            if(content.Length > length)
            {
                content = content.Substring(0, length);
            }
            return content;
        }


        /// <summary>
        /// 获取移除Html内容
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static MvcHtmlString GetAutoHtmlString(this HtmlHelper helper, string content, int length)
        {
            if (content == null || content.Length < 0)
            {
                return MvcHtmlString.Create(content);
            }
            content = CengZai.Helper.Util.RemoveHtml(content) + "";
            if (content.Length > length)
            {
                content = content.Substring(0, length);
            }
            return MvcHtmlString.Create(content);
        }


        /// <summary>
        /// 按字节长度截取字符串(支持截取带HTML代码样式的字符串)
        /// </summary>
        /// <param name="param">将要截取的字符串参数</param>
        /// <param name="length">截取的字节长度</param>
        /// <param name="endAppend">字符串末尾补上的字符串</param>
        /// <returns>返回截取后的字符串</returns>
        public static MvcHtmlString GetHtmlSubstring(this HtmlHelper helper, string param, int length)
        {
            string Pattern = null;
            MatchCollection m = null;
            StringBuilder result = new StringBuilder();
            int n = 0;
            char temp;
            bool isCode = false; //是不是HTML代码
            bool isHTML = false; //是不是HTML特殊字符,如&nbsp;
            char[] pchar = param.ToCharArray();
            for (int i = 0; i < pchar.Length; i++)
            {
                temp = pchar[i];
                if (temp == '<')
                {
                    isCode = true;
                }
                else if (temp == '&')
                {
                    isHTML = true;
                }
                else if (temp == '>' && isCode)
                {
                    n = n - 1;
                    isCode = false;
                }
                else if (temp == ';' && isHTML)
                {
                    isHTML = false;
                }

                if (!isCode && !isHTML)
                {
                    n = n + 1;
                    //UNICODE码字符占两个字节
                    if (System.Text.Encoding.Default.GetBytes(temp + "").Length > 1)
                    {
                        n = n + 1;
                    }
                }

                result.Append(temp);
                if (n >= length)
                {
                    break;
                }
            }
            //取出截取字符串中的HTML标记
            string temp_result = result.ToString().Replace("\n", "").Replace("\r", "");
            temp_result = Regex.Replace(temp_result, @"(>)[^<>]*(<?)", @"$1$2", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //去掉不需要结素标记的HTML标记
            //temp_result = temp_result.Replace(@"</?(AREA|BASE|BASEFONT|BODY|BR|COL|COLGROUP|DD|DT|FRAME|HEAD|HR|HTML|IMG|INPUT|ISINDEX|LI|LINK|META|OPTION|P|PARAM|TBODY|TD|TFOOT|TH|THEAD|TR|area|base|basefont|body|br|col|colgroup|dd|dt|frame|head|hr|html|img|input|isindex|li|link|meta|option|p|param|tbody|td|tfoot|th|thead|tr)[^<>]*/?>","");
            temp_result = Regex.Replace(temp_result
                , @"</?(AREA|BASE|BASEFONT|BODY|BR|COL|COLGROUP|DD|DT|FRAME|HEAD|HR|HTML|IMG|INPUT|ISINDEX|LI|LINK|META|OPTION|P|PARAM|TBODY|TD|TFOOT|TH|THEAD|TR|area|base|basefont|body|br|col|colgroup|dd|dt|frame|head|hr|html|img|input|isindex|li|link|meta|option|p|param|tbody|td|tfoot|th|thead|tr)[^<>]*/?>"
                , ""
                , RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //去掉成对的HTML标记
            //temp_result = temp_result.Replace(@"<([a-zA-Z]+)[^<>]*>(.*?)</\1>", "$2");
            temp_result = Regex.Replace(temp_result, @"<([a-zA-Z]+)[^<>]*>(.*?)</\1>", "$2", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //用正则表达式取出标记
            Pattern = ("<([a-zA-Z]+)[^<>]*>");
            m = Regex.Matches(temp_result, Pattern);
            ArrayList endHTML = new ArrayList();
            foreach (Match mt in m)
            {
                endHTML.Add(mt.Result("$1"));
            }
            //补全不成对的HTML标记
            for (int i = endHTML.Count - 1; i >= 0; i--)
            {
                result.Append("</");
                result.Append(endHTML[i]);
                result.Append(">");
            }
            return MvcHtmlString.Create(result.ToString());
        }



        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetDateFormat(this HtmlHelper helper, DateTime? date, string format)
        {
            string val = date + "";
            try
            {
                if (date != null && !string.IsNullOrEmpty(format))
                {
                    val = Convert.ToDateTime(date).ToString(format);
                }
            }
            catch { }
            return val;
        }
        

    }
}

