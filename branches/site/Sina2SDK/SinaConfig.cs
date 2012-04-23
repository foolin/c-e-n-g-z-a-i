using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;

namespace Open.Sina2SDK
{
    /// <summary>
    /// 新浪微博的配置数据
    /// </summary>
    [Serializable]
    public class SinaConfig
    {
        private NameValueCollection SinaSection = (NameValueCollection)ConfigurationManager.GetSection("OAuthGroup/SinaSection");

        /// <summary>
        /// 申请QQ登录成功后，分配给应用的appid
        /// </summary>
        /// <returns>string AppKey</returns>
        public string GetAppKey()
        {
            return SinaSection["AppKey"];
        }

        /// <summary>
        /// 申请QQ登录成功后，分配给网站的appkey
        /// </summary>
        /// <returns>string AppSecret</returns>
        public string GetAppSecret()
        {
            return SinaSection["AppSecret"];
        }

        /// <summary>
        /// 得到回调地址
        /// </summary>
        /// <returns></returns>
        public Uri GetCallBackURI()
        {
            string callbackUrl = SinaSection["CallBackURI"];
            if (!Uri.IsWellFormedUriString(callbackUrl, UriKind.Absolute))
            {
                var current = HttpContext.Current;
                if (current != null)
                {
                    var currentUrl = current.Request.Url;
                    callbackUrl = string.Format("{0}://{1}:{2}{3}", currentUrl.Scheme, currentUrl.Host, currentUrl.Port, callbackUrl);
                }
            }
            return new Uri(callbackUrl);
        }
    }
}
