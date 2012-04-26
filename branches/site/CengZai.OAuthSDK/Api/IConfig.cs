using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CengZai.OAuthSDK.Api
{
    public interface IConfig
    {
        /// <summary>
        /// 是否打开
        /// </summary>
        /// <returns></returns>
        bool IsOpen();


        /// <summary>
        /// 获取ApiUrl
        /// </summary>
        /// <returns></returns>
        string GetBaseUrl();

        /// <summary>
        /// AppKey
        /// </summary>
        /// <returns></returns>
        string GetAppKey();

        /// <summary>
        /// AppSecret
        /// </summary>
        /// <returns></returns>
        string GetAppSecret();

        /// <summary>
        /// 回调Url
        /// </summary>
        /// <returns></returns>
        Uri GetCallbackUrl();
    }
}
