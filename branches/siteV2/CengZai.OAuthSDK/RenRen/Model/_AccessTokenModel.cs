using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CengZai.OAuthSDK.RenRen
{
    internal class _AccessTokenModel
    {
        //"access_token": "10000|5.a6b7dbd428f731035f771b8d15063f61.86400.1292922000-222209506",
        /// <summary>
        /// 获取的Access Token；
        /// </summary>
        public string access_token { set; get; }

        //"expires_in": 87063,
        /// <summary>
        /// Access Token的有效期，以秒为单位；
        /// </summary>
        public int expires_in { set; get; }

        //"refresh_token": "10000|0.385d55f8615fdfd9edb7c4b5ebdc3e39-222209506",
        /// <summary>
        /// 用于刷新Access Token 的 Refresh Token，长期有效，不会过期；
        /// </summary>
        public string refresh_token { set; get; }

        //"scope": "read_user_album read_user_feed"
        /// <summary>
        /// Access Token最终的访问范围，既用户实际授予的权限列表（用户在授权页面时，有可能会取消掉某些请求的权限）。关于权限的具体信息请参考权限列表。
        /// </summary>
        public string scope { set; get; }
    }
}
