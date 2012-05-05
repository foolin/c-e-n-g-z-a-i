using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html; 
using System.Web.Routing;
using System.Text.RegularExpressions;
using System.Collections;

namespace System.Web.Mvc
{

    public static class HtmlExtend
    {

        /// <summary>
        /// 分页函数条：
        /// 1.代码必须初始化ViewData["PageSize"]、ViewData["TotalCount"]的值
        /// 2.以“page”作为分页标识
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper helper)
        {
            int pageSize = 20;
            int totalCount = 0;
            try
            {
                pageSize = Convert.ToInt32(helper.ViewData["PageSize"]);
            }
            catch{
                pageSize = 20;
            }
            int.TryParse(helper.ViewData["TotalCount"] + "", out totalCount);
            return Pager(helper, "page", pageSize, totalCount);
        }

        /// <summary>
        /// 扩展Html
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="index"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, string page, int pageSize, int totalCount)
        {
            page = string.IsNullOrEmpty(page) ? "page" : page;  //页标识
            int pageIndex = 1; //当前页
            int pageCount = 0;  //总页数

            var queryString = helper.ViewContext.HttpContext.Request.QueryString;
            var dicRoutValues = new System.Web.Routing.RouteValueDictionary(helper.ViewContext.RouteData.Values);
            if (!string.IsNullOrEmpty(queryString[page]))
            {
                //与相应的QueryString绑定 
                foreach (string key in queryString.Keys)
                    if (queryString[key] != null && !string.IsNullOrEmpty(key))
                        dicRoutValues[key] = queryString[key];
                int.TryParse(queryString[page], out pageIndex);
            }
            else
            {
                //获取 ～/Page/{page number} 的页号参数
                if (dicRoutValues.ContainsKey(page))
                {
                    int.TryParse(dicRoutValues[page].ToString(), out pageIndex);
                }
            }
            pageSize = pageSize < 1 ? 20 : pageSize;
            pageCount = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            pageIndex = (pageIndex < 1 || pageIndex > pageCount) ? 1 : pageIndex;
            


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
                dicRoutValues[page] = 1;
                html.AppendFormat("<li>{0}</li>", helper.RouteLink("|<", dicRoutValues));
                dicRoutValues[page] = pageIndex - 1;
                html.AppendFormat("<li>{0}</li>", helper.RouteLink("<", dicRoutValues));
            }
            int perSideNum = 3;
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
                            dicRoutValues[page] = i;
                            html.AppendFormat("<li>{0}</li>", helper.RouteLink(i.ToString(), dicRoutValues));
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
                        dicRoutValues[page] = j;
                        html.AppendFormat("<li>{0}</li>", helper.RouteLink(j.ToString(), dicRoutValues));
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
                dicRoutValues[page] = pageIndex + 1;
                html.AppendFormat("<li>{0}</li>", helper.RouteLink(">", dicRoutValues));
                dicRoutValues[page] = pageIndex + 1;
                html.AppendFormat("<li>{0}</li>", helper.RouteLink(">|", dicRoutValues));
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

        /* 网上分页 ***** /
        /// <summary>  
        /// 分页Pager显示  
        /// </summary>   
        /// <param name="html"></param>  
        /// <param name="page">标识当前页码的QueryStringKey</param>   
        /// <param name="pageSize">每页显示</param>  
        /// <param name="totalCount">总数据量</param>  
        /// <returns></returns> 
        public static MvcHtmlString Pager(this HtmlHelper html, string page, int pageSize, int totalCount)
        {
            var queryString = html.ViewContext.HttpContext.Request.QueryString;
            int currentPage = 1; //当前页  
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数  
            var dict = new System.Web.Routing.RouteValueDictionary(html.ViewContext.RouteData.Values);
            var output = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(queryString[page]))
            {
                //与相应的QueryString绑定 
                foreach (string key in queryString.Keys)
                    if (queryString[key] != null && !string.IsNullOrEmpty(key))
                        dict[key] = queryString[key];
                int.TryParse(queryString[page], out currentPage);
            }
            else
            {
                //获取 ～/Page/{page number} 的页号参数
                if (dict.ContainsKey(page))
                {
                    int.TryParse(dict[page].ToString(), out currentPage);
                }
            }
            if (currentPage <= 0) currentPage = 1;
            if (totalPages > 1)
            {
                if (currentPage != 1)
                {
                    //处理首页连接  
                    dict[page] = 1;
                    output.AppendFormat("{0} ", html.RouteLink("首页", dict));
                }
                if (currentPage > 1)
                {
                    //处理上一页的连接  
                    dict[page] = currentPage - 1;
                    output.Append(html.RouteLink("上一页", dict));
                }
                else
                {
                    output.Append("上一页");
                }
                output.Append(" ");
                int currint = 5;
                for (int i = 0; i <= 10; i++)
                {
                    //一共最多显示10个页码，前面5个，后面5个  
                    if ((currentPage + i - currint) >= 1 && (currentPage + i - currint) <= totalPages)
                        if (currint == i)
                        {
                            //当前页处理  
                            output.Append(string.Format("[{0}]", currentPage));
                        }
                        else
                        {
                            //一般页处理 
                            dict[page] = currentPage + i - currint;
                            output.Append(html.RouteLink((currentPage + i - currint).ToString(), dict));
                        }
                    output.Append(" ");
                }
                if (currentPage < totalPages)
                {
                    //处理下一页的链接 
                    dict[page] = currentPage + 1;
                    output.Append(html.RouteLink("下一页", dict));
                }
                else
                {
                    output.Append("下一页");
                }
                output.Append(" ");
                if (currentPage != totalPages)
                {
                    dict[page] = totalPages;
                    output.Append(html.RouteLink("末页", dict));
                }
                output.Append(" ");
            }
            output.AppendFormat("{0} / {1}", currentPage, totalPages);//这个统计加不加都行 
            return MvcHtmlString.Create(output.ToString());
        }
        */


        /// <summary>
        /// 取邮箱域名
        /// </summary>
        /// <param name="email"></param>
        public static string GetEmailDomain(this HtmlHelper helper, string email)
        {
            return CengZai.Helper.Util.GetEmailDomain(email);
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

