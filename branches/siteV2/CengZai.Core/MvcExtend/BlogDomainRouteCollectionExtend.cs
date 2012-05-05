using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class BlogDomainRouteCollectionExtend
    {
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#",
            Justification = "This is not a regular URL as it may contain special routing characters.")]
        public static Route MapBlogDomainRoute(this RouteCollection routes, string name, string username, string url)
        {
            return MapBlogDomainRoute(routes, name, username, url, null /* defaults */, (object)null /* constraints */);
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#",
            Justification = "This is not a regular URL as it may contain special routing characters.")]
        public static Route MapBlogDomainRoute(this RouteCollection routes, string name, string username, string url, object defaults)
        {
            return MapBlogDomainRoute(routes, name, username, url, defaults, (object)null /* constraints */);
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#",
            Justification = "This is not a regular URL as it may contain special routing characters.")]
        public static Route MapBlogDomainRoute(this RouteCollection routes, string name, string username, string url, object defaults, object constraints)
        {
            return MapBlogDomainRoute(routes, name, username, url, defaults, constraints, null /* namespaces */);
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#",
            Justification = "This is not a regular URL as it may contain special routing characters.")]
        public static Route MapBlogDomainRoute(this RouteCollection routes, string name, string username, string url, string[] namespaces)
        {
            return MapBlogDomainRoute(routes, name, username, url, null /* defaults */, null /* constraints */, namespaces);
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#",
            Justification = "This is not a regular URL as it may contain special routing characters.")]
        public static Route MapBlogDomainRoute(this RouteCollection routes, string name, string username, string url, object defaults, string[] namespaces)
        {
            return MapBlogDomainRoute(routes, name, username, url, defaults, null /* constraints */, namespaces);
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#",
            Justification = "This is not a regular URL as it may contain special routing characters.")]
        public static Route MapBlogDomainRoute(this RouteCollection routes, string name, string username, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            if (username == null)
            {
                throw new ArgumentNullException("BlogDomain");
            }

            Route route = new BlogDomainRoute(username, url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints)
            };

            if ((namespaces != null) && (namespaces.Length > 0))
            {
                route.DataTokens = new RouteValueDictionary();
                route.DataTokens["Namespaces"] = namespaces;
            }

            routes.Add(name, route);

            return route;
        }
    }
}