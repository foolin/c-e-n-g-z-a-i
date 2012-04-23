using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;

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
        /// 取邮箱域名
        /// </summary>
        /// <param name="email"></param>
        public static string GetEmailDomain(string email)
        {
            if (string.IsNullOrEmpty(email) || email.IndexOf('@') == -1)
            {
                return email;
            }

            string domain = email.Substring(email.IndexOf('@') + 1);
            return domain;
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
        public static bool CheckBadWord(string str)
        {
            string pattern = @"select|insert|delete|from|count\(|drop table|update|truncate|asc\(|mid\(|char\(|xp_cmdshell|exec   master|netlocalgroup administrators|:|net user|""|or|and";
            if (Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase) || Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']"))
                return true;
            return false;
        }


        /// <summary>
        /// 是否是邮箱
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            return Regex.IsMatch(email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }


        /// <summary>
        /// 获取上传图片Url
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string GetAvatarUrl(string imagePath)
        {
            string url ="";
            if (string.IsNullOrEmpty(imagePath))
            {
                url = Config.ResourceHost +  "/img/noavatar.jpg";
            }
            else if (imagePath.IndexOf(":") != -1)
            {
                return imagePath;
            }
            else
            {
                url = Config.UploadHttpPath + "/" + imagePath;
            }
            return Regex.Replace(url, "([^:])//", "$1/");
        }


        /// <summary>
        /// 取中文日期
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetChinseDate(DateTime? date, string format)
        {
            string strDate = "";
            try
            {
                DateTime dtDate = Convert.ToDateTime(date);
                strDate = NumToChinese(dtDate.ToString(format));
                strDate = strDate.Replace("年一〇", "年十");
                strDate = strDate.Replace("年一", "年十");  //11月，12月
            }
            catch { }
            return strDate;
        }

        /// <summary>
        /// 数字转中文
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static string NumToChinese(string nums)
        {
            if (string.IsNullOrEmpty(nums))
            {
                return nums;
            }
            StringBuilder result = new StringBuilder();
            char[] arr = nums.ToCharArray();
            foreach (char c in arr)
            {
                switch (c)
                {
                    case '0':
                        result.Append("〇");
                        break;
                    case '1':
                        result.Append("一");
                        break;
                    case '2':
                        result.Append("二");
                        break;
                    case '3':
                        result.Append("三");
                        break;
                    case '4':
                        result.Append("四");
                        break;
                    case '5':
                        result.Append("五");
                        break;
                    case '6':
                        result.Append("六");
                        break;
                    case '7':
                        result.Append("七");
                        break;
                    case '8':
                        result.Append("八");
                        break;
                    case '9':
                        result.Append("九");
                        break;
                    default:
                        result.Append(c);
                        break;
                }
            }
            return result.ToString();
        }


        /// <summary>
        /// 取文件物理路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            if (path.IndexOf(':') == -1)
            {
                path = HttpContext.Current.Server.MapPath(path);
            }
            return path;
        }


        /// <summary>
        /// 确保目录存在。
        /// <para>如果目录不存在，则创建目录（包括上级目录）。</para>
        /// </summary>
        /// <param name="path">目录路径（不含文件名）</param>
        /// <returns>返回一个 Boolean 值，如果目录不存在且创建目录出现异常时，返回值为 False。</returns>
        public static bool EnsureDir(string path)
        {
            try
            {
                if (Directory.Exists(path))
                    return true;
                if (EnsureDir(Directory.GetParent(path).ToString()))
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("Util.EnsureDir", ex);
                return false;
            }
        }


        /// <summary>
        /// 确保文件的目录存在。
        /// <para>如果文件目录不存在，则创建目录（包括上级目录）。</para>
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>返回一个 Boolean 值，如果目录或文件不存在且创建它们出现异常时，返回值为 False。</returns>
        public static bool EnsureFileDir(string path)
        {
            try
            {
                if (File.Exists(path))
                    return true;
                if (EnsureDir(Directory.GetParent(path).ToString()))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("Sxmobi.EnsureFileDir", ex);
                return false;
            }
        }


        /// <summary>
        /// 获取根域名
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static string GetRootDomain(string domain)
        {
            domain = (domain + "").ToLower().Trim();
            if (domain.IndexOf("www.") == 0)
            {
                domain = domain.Substring(3);
            }
            return domain;
        }

    }
}
