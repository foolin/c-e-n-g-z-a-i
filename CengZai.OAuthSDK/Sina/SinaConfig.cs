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
            throw new NotImplementedException();
        }

        public string GetBaseUrl()
        {
            throw new NotImplementedException();
        }

        public string GetAppKey()
        {
            throw new NotImplementedException();
        }

        public string GetAppSecret()
        {
            throw new NotImplementedException();
        }

        public Uri GetCallbackUrl()
        {
            throw new NotImplementedException();
        }
    }
}
