using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CengZai.OAuthSDK.Api;
using System.Collections.Specialized;
using System.Configuration;

namespace CengZai.OAuthSDK.Sina
{
    class SinaConfig : IConfig
    {
        private NameValueCollection SinaSection = (NameValueCollection)ConfigurationManager.GetSection("OAuthGroup/SinaSection");

        public bool IsOpen()
        {
            return true;
        }

        public string GetBaseUrl()
        {
            return "https://api.weibo.com";
        }

        public string GetAppKey()
        {
            return "3345715608";
        }

        public string GetAppSecret()
        {
            return "9fc050dd16eb5478dc53b63ffb4a4b31";
        }

        public Uri GetCallbackUrl()
        {
            return new Uri("http://www.cengzai.com/oauth/testcallback");
        }
    }
}
