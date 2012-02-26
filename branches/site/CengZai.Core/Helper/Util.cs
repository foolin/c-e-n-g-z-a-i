using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

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


        /// <summary>
        /// 获取安全html代码
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetSafeHtml(string html)
        {
            //过滤<script></script>标记 
            System.Text.RegularExpressions.Regex script =
                  new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //过滤href=javascript: (<A>) 属性 
            System.Text.RegularExpressions.Regex attrScript =
                  new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //过滤其它控件的on...事件 
            System.Text.RegularExpressions.Regex onScript =
                  new System.Text.RegularExpressions.Regex(@" on[\s\S]*=",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //过滤iframe 
            System.Text.RegularExpressions.Regex iframe =
                  new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //过滤frameset 
            System.Text.RegularExpressions.Regex frameset =
                  new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            html = script.Replace(html, ""); //过滤<script></script>标记 
            html = attrScript.Replace(html, ""); //过滤href=javascript: (<A>) 属性 
            html = onScript.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
            html = iframe.Replace(html, ""); //过滤iframe 
            html = frameset.Replace(html, ""); //过滤frameset 

            return html;
        }


        /// <summary>
        /// 移除Html代码
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveHtml(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记    
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性    
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件    
            html = regex4.Replace(html, ""); //过滤iframe    
            html = regex5.Replace(html, ""); //过滤frameset    
            html = regex6.Replace(html, ""); //过滤frameset    
            html = regex7.Replace(html, ""); //过滤frameset    
            html = regex8.Replace(html, ""); //过滤frameset    
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }


        /// <summary>
        /// 检测非法字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool CheckBadWord(string str)
        {
            string pattern = @"select|insert|delete|from|count\(|drop table|update|truncate|asc\(|mid\(|char\(|xp_cmdshell|exec   master|netlocalgroup administrators|:|net user|""|or|and";
            if (Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase) || Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']"))
                return true;
            return false;
        }

    }
}
