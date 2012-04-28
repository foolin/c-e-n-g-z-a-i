using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CengZai.OAuthSDK.Api;
using System.Collections.Specialized;
using System.Configuration;

namespace CengZai.OAuthSDK.RenRen
{
    public class RenConfig : IConfig
    {
        private NameValueCollection Config = (NameValueCollection)ConfigurationManager.GetSection("OAuthSDK/RenRen");


        /// <summary>
        /// 判断是否打开
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            return Config["Open"] == "1";
        }

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
