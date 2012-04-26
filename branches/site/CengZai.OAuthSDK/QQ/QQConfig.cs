using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CengZai.OAuthSDK.Api;

namespace CengZai.OAuthSDK.QQ
{
    public class QQConfig : IConfig
    {
        public bool IsOpen()
        {
            return true;
        }

        public string GetBaseUrl()
        {
            return "https://graph.qq.com";
        }

        public string GetAppKey()
        {
            return "100263911";
        }

        public string GetAppSecret()
        {
            return "44f9b1cf8da0527be2b27479ba9485f3";
        }

        public Uri GetCallbackUrl()
        {
            return new Uri("http://www.cengzai.com/oauth/testcallback");
        }
    }
}
