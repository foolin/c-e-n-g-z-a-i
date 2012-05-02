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
        private NameValueCollection Config = (NameValueCollection)ConfigurationManager.GetSection("OAuthSDK/Sina");

        /// <summary>
        /// 取接口的基础Url
        /// </summary>
        /// <returns></returns>
        public string GetBaseUrl()
        {
            return Config["BaseUrl"];
        }

        /// <summary>
        /// 应用Key
        /// </summary>
        /// <returns></returns>
        public string GetAppKey()
        {
            return Config["AppKey"];
        }

        /// <summary>
        /// 应用AppSecret
        /// </summary>
        /// <returns></returns>
        public string GetAppSecret()
        {
            return Config["AppSecret"];
        }

        /// <summary>
        /// 授权后回调Url
        /// </summary>
        /// <returns></returns>
        public string GetCallbackUrl()
        {
            return Config["CallbackUrl"];
        }
    }
}
