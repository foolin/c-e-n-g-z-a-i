using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CengZai.OAuthSDK.Api;
using System.Collections.Specialized;

namespace CengZai.OAuthSDK.RenRen
{
    public class RenApi : BaseApi
    {
        RenConfig config = new RenConfig();

        #region __授权___
        /// <summary>
        /// 跳转到服务器获取Authorization Code
        /// </summary>
        /// <param name="state">client端的状态值。用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回。</param>
        /// <param name="scope">scope：非必须参数。以空格分隔的权限列表，若不传递此参数，代表请求用户的默认权限。详情请查看：http://wiki.dev.renren.com/wiki/%E6%9D%83%E9%99%90%E5%88%97%E8%A1%A8 </param>
        /// <returns></returns>
        public string GetAuthorizationUrl(string state, string scope = "")
        {
            string url = string.Format("{0}/oauth/authorize?client_id={1}&redirect_uri={2}&response_type=code&state={3}"
                , this.config.GetBaseUrl(), this.config.GetAppKey(), config.GetCallbackUrl().ToString(), state);
            if (!string.IsNullOrEmpty(scope))
            {
                url += "&scope=" + scope;
            }
            //System.Web.HttpContext.Current.Response.Redirect(url);
            return url;
        }

        /// <summary>
        /// 使用Authentication Code获取Access Token。
        /// </summary>
        /// <param name="code">获得的Authorization Code。</param>
        public bool GetAccessToken(string code)
        {
            try
            {
                string urlAccessToken = string.Format("{0}/oauth/token?grant_type=authorization_code&client_id={1}&redirect_uri={2}&client_secret={3}&code={4}"
                    , this.config.GetBaseUrl()
                    , this.config.GetAppKey()
                    , this.config.GetCallbackUrl()
                    , this.config.GetAppSecret()
                    , code
                    );
                string responseAccessToken = HttpUtil.HttpGet(urlAccessToken);
                _AccessTokenModel accessToken = JsonConvert.DeserializeObject<_AccessTokenModel>(responseAccessToken);
                if (accessToken == null || string.IsNullOrEmpty(accessToken.access_token))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
