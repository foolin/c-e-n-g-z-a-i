using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace System.Web.Mvc
{
    public class DomainRoute : Route
    {
        private Regex domainRegex;
        private Regex pathRegex;

        /// <summary>
        /// 判断是否匹配
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 判断是否匹配博客地址
        /// </summary>
        public bool IsMatch { set; get; }


        public DomainRoute(string domain, string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
            Domain = domain;
        }

        public DomainRoute(string domain, string url, object defaults)
            : base(url, new RouteValueDictionary(defaults), new MvcRouteHandler())
        {
            Domain = domain;
        }

        public DomainRoute(string domain, string url, object defaults, IRouteHandler routeHandler)
            : base(url, new RouteValueDictionary(defaults), routeHandler)
        {
            Domain = domain;
        }

        public DomainRoute(string domain, string url, object defaults, object constraints)
            : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new MvcRouteHandler())
        {
            Domain = domain;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {

            // 构造 regex
            domainRegex = CreateRegex(Domain);
            pathRegex = CreateRegex(Url);
            object constraints = Constraints;

            // 请求信息
            string requestDomain = httpContext.Request.Headers["host"];
            if (!string.IsNullOrEmpty(requestDomain))
            {
                if (requestDomain.IndexOf(":") > 0)
                {
                    requestDomain = requestDomain.Substring(0, requestDomain.IndexOf(":"));
                }
            }
            else
            {
                requestDomain = httpContext.Request.Url.Host;
            }
            string requestPath = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;

            // 匹配域名和路由
            Match domainMatch = domainRegex.Match(requestDomain);
            Match pathMatch = pathRegex.Match(requestPath);

            IsMatch = false;    //不匹配
            // 路由数据
            RouteData data = null;
            if (domainMatch.Success && pathMatch.Success)
            {
                IsMatch = true; //设置为匹配

                data = new RouteData(this, RouteHandler);

                // 添加默认选项
                if (Defaults != null)
                {
                    foreach (KeyValuePair<string, object> item in Defaults)
                    {
                        data.Values[item.Key] = item.Value;
                    }
                }

                // 匹配域名路由
                for (int i = 1; i < domainMatch.Groups.Count; i++)
                {
                    Group group = domainMatch.Groups[i];
                    if (group.Success)
                    {
                        string key = domainRegex.GroupNameFromNumber(i);

                        if (!string.IsNullOrEmpty(key) && !char.IsNumber(key, 0))
                        {
                            if (!string.IsNullOrEmpty(group.Value))
                            {
                                data.Values[key] = group.Value;
                            }
                        }
                    }
                }

                // 匹配域名路径
                for (int i = 1; i < pathMatch.Groups.Count; i++)
                {
                    Group group = pathMatch.Groups[i];
                    if (group.Success)
                    {
                        string key = pathRegex.GroupNameFromNumber(i);

                        if (!string.IsNullOrEmpty(key) && !char.IsNumber(key, 0))
                        {
                            if (!string.IsNullOrEmpty(group.Value))
                            {
                                data.Values[key] = group.Value;
                            }
                        }
                    }
                }
            }

            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return base.GetVirtualPath(requestContext, RemoveDomainTokens(values));
        }

        public DomainData GetDomainData(RequestContext requestContext, RouteValueDictionary values)
        {
            // 获得主机名
            string hostname = Domain;
            foreach (KeyValuePair<string, object> pair in values)
            {
                hostname = hostname.Replace("{" + pair.Key + "}", pair.Value.ToString());
            }

            // Return 域名数据
            return new DomainData
            {
                Protocol = "http",
                HostName = hostname,
                Fragment = ""
            };
        }

        private Regex CreateRegex(string source)
        {
            // 替换
            source = source.Replace("/", @"\/?");
            source = source.Replace(".", @"\.?");
            source = source.Replace("-", @"\-?");
            //Foolin 2012年4月13日 增加路由限制
            RouteValueDictionary constraints = Constraints as RouteValueDictionary;
            MatchCollection matchs = Regex.Matches(source, @"{(?<key>[a-zA-Z][a-zA-Z0-9_]*)}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {
                string key = matchs[i].Groups["key"].Value;
                if (constraints != null && constraints.ContainsKey(key))
                {
                    source = source.Replace("{" + key + "}", @"(?<" + key + ">(" + constraints[key] + @"))");
                }
                else
                {
                    source = source.Replace("{" + key + "}", @"(?<" + key + ">([a-zA-Z0-9_]*))");
                }
            }
            //source = source.Replace("{", @"(?<");
            //source = source.Replace("}", @">([a-zA-Z0-9_]*))");

            return new Regex("^" + source + "$");
        }

        private RouteValueDictionary RemoveDomainTokens(RouteValueDictionary values)
        {
            Regex tokenRegex = new Regex(@"({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?");
            Match tokenMatch = tokenRegex.Match(Domain);
            for (int i = 0; i < tokenMatch.Groups.Count; i++)
            {
                Group group = tokenMatch.Groups[i];
                if (group.Success)
                {
                    string key = group.Value.Replace("{", "").Replace("}", "");
                    if (values.ContainsKey(key))
                        values.Remove(key);
                }
            }

            return values;
        }
    }


    public class DomainData
    {
        public string Protocol { get; set; }
        public string HostName { get; set; }
        public string Fragment { get; set; }
    }
}
