using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CengZai.OAuthSDK.Api
{
    public class BaseApi
    {
        /// <summary>
        /// OAuthToken
        /// </summary>
        public OAuthToken Token = null;
        

        public BaseApi()
        {
        }

        public BaseApi(OAuthToken oauthToken)
        {
            this.Token = oauthToken;
        }

        /// <summary>
        /// 根据AccessToken来获取相关权限
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="uid"></param>
        /// <param name="expiresAt"></param>
        public BaseApi(string accessToken, string uid, int expiresAt)
        {
            this.Token = new OAuthToken
            {
                AccessToken = accessToken,
                Uid = uid,
                ExpiresIn = expiresAt
            };
        }

    }
}
