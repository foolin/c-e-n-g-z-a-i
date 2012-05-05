using System.Web;
using System.Web.Routing;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace System.Web.Mvc
{
    /// <summary>
    /// A route class to work with a specific username
    /// </summary>
    public class BlogDomainRoute : Route
    {
        /// <summary>
        /// The username to route against
        /// </summary>
        private readonly string username;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogDomainRoute"/> class.
        /// </summary>
        /// <param name="username">The sub domain.</param>
        /// <param name="url">The URL.</param>
        /// <param name="routeHandler">The route handler.</param>
        public BlogDomainRoute(string username, string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
            this.username = username.ToLower();
        }

        /// <summary>
        /// Returns information about the requested route.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <returns>
        /// An object that contains the values from the route definition.
        /// </returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var url = httpContext.Request.Headers["HOST"];
            var index = url.IndexOf(".");

            if (index < 0)
            {
                return null;
            }

            var possibleusername = url.Substring(0, index).ToLower();

            //if (possibleusername == username)
            //{
            //    var result = base.GetRouteData(httpContext);
            //    return result;
            //}

            if (possibleusername == username || Regex.IsMatch(possibleusername, username))
            {
                var result = base.GetRouteData(httpContext);
                if (result == null)
                {
                    result = new RouteData(this, RouteHandler);
                }
                result.Values["username"] = possibleusername;
                return result;
            }

            return null;
        }

        /// <summary>
        /// Returns information about the URL that is associated with the route.
        /// </summary>
        /// <param name="requestContext">An object that encapsulates information about the requested route.</param>
        /// <param name="values">An object that contains the parameters for a route.</param>
        /// <returns>
        /// An object that contains information about the URL that is associated with the route.
        /// </returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            // Checks if the area to generate the route against is this same as the username
            // If so we remove the area value so it won't be added to the URL as a query parameter
            if (values != null && values.ContainsKey("Area"))
            {
                if (values["Area"].ToString().ToLower() == this.username)
                {
                    values.Remove("Area");
                    return base.GetVirtualPath(requestContext, values);
                }
            }

            return null;
        }
    }
}