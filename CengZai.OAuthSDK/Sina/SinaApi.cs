using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CengZai.OAuthSDK.Api;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.IO;

namespace CengZai.OAuthSDK.Sina
{
    public class SinaApi : BaseApi
    {

        /// <summary>
        /// 配置文件
        /// </summary>
        SinaConfig config = new SinaConfig();

        public SinaApi()
        {
        }

        #region __授权___
        /// <summary>
        /// 跳转到服务器获取Authorization Code
        /// </summary>
        /// <param name="state">client端的状态值。用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回。</param>
        /// <param name="display">授权页面的终端类型，取值见下面的说明。
        /// default	默认的授权页面，适用于web浏览器。
        /// mobile	移动终端的授权页面，适用于支持html5的手机。
        /// popup	弹窗类型的授权页，适用于web浏览器小窗口。
        /// wap1.2	wap1.2的授权页面。
        /// wap2.0	wap2.0的授权页面。
        /// js	微博JS-SDK专用授权页面，弹窗类型，返回结果为JSONP回掉函数。
        /// apponweibo	默认的站内应用授权页，授权后不返回access_token，只刷新站内应用父框架。
        /// </param>
        /// <returns></returns>
        public string GetAuthorizationUrl(string state, string display = "")
        {
            string url = string.Format("{0}/oauth2/authorize?client_id={1}&redirect_uri={2}&response_type=code&state={3}&display={4}"
                , this.config.GetBaseUrl(), this.config.GetAppKey(), config.GetCallbackUrl().ToString(), state, display);
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
                string urlAccessToken = string.Format("{0}/oauth2/access_token", this.config.GetBaseUrl());
                List<HttpParameter> parameters = new List<HttpParameter>();
                parameters.Add(new HttpParameter("client_id", this.config.GetAppKey()));
                parameters.Add(new HttpParameter("client_secret", this.config.GetAppSecret()));
                parameters.Add(new HttpParameter("grant_type", "authorization_code"));
                parameters.Add(new HttpParameter("redirect_uri", this.config.GetCallbackUrl().ToString()));
                parameters.Add(new HttpParameter("code", code));
                string responseAccessToken = HttpUtil.HttpPost(urlAccessToken, parameters);

                if (string.IsNullOrEmpty(responseAccessToken))
                {
                    return false;
                }

                _AccessTokenModel tokenModel = JsonConvert.JavascriptDeserialize<_AccessTokenModel>(responseAccessToken);
                if (tokenModel == null)
                {
                    return false;
                }

                //赋值
                this.Token = new OAuthToken
                {
                    AccessToken = tokenModel.access_token,
                    ExpiresIn = tokenModel.expires_in,
                    Uid = tokenModel.uid.ToString()
                };
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region __用户__
        /// <summary>
        /// 取当前登录用户
        /// </summary>
        /// <returns></returns>
        public SinaUser GetUserInfo()
        {
            try
            {
                if (this.Token == null || string.IsNullOrEmpty(this.Token.Uid))
                {
                    return null;
                }
                return GetUserInfo(long.Parse(this.Token.Uid));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 取某个用户的信息
        /// </summary>
        /// <param name="uid">用户Uid</param>
        /// <returns></returns>
        public SinaUser GetUserInfo(long uid)
        {
            SinaUser user = null;
            try
            {
                string urlGetUserInfo = string.Format("{0}/2/users/show.json?access_token={1}&uid={2}"
                    , this.config.GetBaseUrl()
                    , this.Token.AccessToken
                    , uid);
                string jsonUserInfo = HttpUtil.HttpGet(urlGetUserInfo);
                user = JsonConvert.JavascriptDeserialize<SinaUser>(jsonUserInfo);
            }
            catch (Exception ex)
            {
                return null;
            }
            return user;
        }
        #endregion


        #region __微博__
        /// <summary>
        /// 发布一条新微博
        /// </summary>
        /// <param name="content">要发布的微博文本内容，必须做URLencode，内容不超过140个汉字。</param>
        /// <param name="jing">纬度，有效范围：-90.0到+90.0，+表示北纬，默认为0.0。</param>
        /// <param name="wei">经度，有效范围：-180.0到+180.0，+表示东经，默认为0.0。</param>
        /// <param name="annotations">元数据，主要是为了方便第三方应用记录一些适合于自己使用的信息，每条微博可以包含一个或者多个元数据，必须以json字串的形式提交，字串长度不超过512个字符，具体内容可以自定。</param>
        /// <returns></returns>
        public SinaWeibo AddWeibo(string content, float jing = 0.0f, float wei = 0.0f, string annotations = "")
        {
            try
            {
                string urlAddWeibo = string.Format("{0}/2/statuses/update.json", this.config.GetBaseUrl());
                List<HttpParameter> parameters = new List<HttpParameter>();
                parameters.Add(new HttpParameter("access_token", this.Token.AccessToken)); //采用OAuth授权方式为必填参数，其他授权方式不需要此参数，OAuth授权后获得。
                parameters.Add(new HttpParameter("status", content));
                parameters.Add(new HttpParameter("lat", jing));
                parameters.Add(new HttpParameter("long", wei));
                parameters.Add(new HttpParameter("annotations", annotations));
                string jsonResponse = HttpUtil.HttpPost(urlAddWeibo, parameters);
                SinaWeibo weibo = JsonConvert.JavascriptDeserialize<SinaWeibo>(jsonResponse);
                return weibo; 
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 发布一条新图片微博
        /// </summary>
        /// <param name="content">要发布的微博文本内容，必须做URLencode，内容不超过140个汉字。</param>
        /// <param name="picBytes">要上传的图片，仅支持JPEG、GIF、PNG格式，图片大小小于5M。</param>
        /// <param name="jing">纬度，有效范围：-90.0到+90.0，+表示北纬，默认为0.0。</param>
        /// <param name="wei">经度，有效范围：-180.0到+180.0，+表示东经，默认为0.0。</param>
        /// <param name="annotations">元数据，主要是为了方便第三方应用记录一些适合于自己使用的信息，每条微博可以包含一个或者多个元数据，必须以json字串的形式提交，字串长度不超过512个字符，具体内容可以自定。</param>
        /// <returns></returns>
        public SinaWeibo AddWeibo(string content, byte[] picBytes, float jing = 0, float wei = 0, string annotations = "")
        {
            try
            {
                string urlAddWeibo = string.Format("{0}/2/statuses/upload.json", this.config.GetBaseUrl());
                List<HttpParameter> parameters = new List<HttpParameter>();
                parameters.Add(new HttpParameter("access_token", this.Token.AccessToken)); //采用OAuth授权方式为必填参数，其他授权方式不需要此参数，OAuth授权后获得。
                parameters.Add(new HttpParameter("status", content));
                parameters.Add(new HttpParameter("lat", jing));
                parameters.Add(new HttpParameter("long", wei));
                parameters.Add(new HttpParameter("annotations", annotations));
                string jsonResponse = HttpUtil.HttpPost(urlAddWeibo, parameters, "pic", picBytes);
                SinaWeibo weibo = JsonConvert.JavascriptDeserialize<SinaWeibo>(jsonResponse);
                return weibo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 发布一条新图片微博
        /// </summary>
        /// <param name="content">要发布的微博文本内容，必须做URLencode，内容不超过140个汉字。</param>
        /// <param name="picPath">要上传的图片路径，仅支持JPEG、GIF、PNG格式，图片大小小于5M。</param>
        /// <param name="jing">纬度，有效范围：-90.0到+90.0，+表示北纬，默认为0.0。</param>
        /// <param name="wei">经度，有效范围：-180.0到+180.0，+表示东经，默认为0.0。</param>
        /// <param name="annotations">元数据，主要是为了方便第三方应用记录一些适合于自己使用的信息，每条微博可以包含一个或者多个元数据，必须以json字串的形式提交，字串长度不超过512个字符，具体内容可以自定。</param>
        /// <returns></returns>
        public SinaWeibo AddWeibo(string content, string picPath, float jing = 0, float wei = 0, string annotations = "")
        {
            try
            {
                if (string.IsNullOrEmpty(picPath))
                {
                    return AddWeibo(content, jing, wei, annotations);
                }

                if (picPath.IndexOf(":") == -1)
                {
                    picPath = System.Web.HttpContext.Current.Server.MapPath(picPath);
                }

                byte[] fileBytes = File.ReadAllBytes(picPath);
                return AddWeibo(content, fileBytes, jing, wei, annotations);
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// 发布一条新图片微博
        /// </summary>
        /// <param name="content">要发布的微博文本内容，必须做URLencode，内容不超过140个汉字。</param>
        /// <param name="picPath">要上传的图片路径，仅支持JPEG、GIF、PNG格式，图片大小小于5M。</param>
        /// <param name="jing">纬度，有效范围：-90.0到+90.0，+表示北纬，默认为0.0。</param>
        /// <param name="wei">经度，有效范围：-180.0到+180.0，+表示东经，默认为0.0。</param>
        /// <param name="annotations">元数据，主要是为了方便第三方应用记录一些适合于自己使用的信息，每条微博可以包含一个或者多个元数据，必须以json字串的形式提交，字串长度不超过512个字符，具体内容可以自定。</param>
        /// <returns></returns>
        public SinaWeibo AddWeiboTestFiles(string content, string picPath, float jing = 0, float wei = 0, string annotations = "")
        {
            try
            {
                if (string.IsNullOrEmpty(picPath))
                {
                    return AddWeibo(content, jing, wei, annotations);
                }

                if (picPath.IndexOf(":") == -1)
                {
                    picPath = System.Web.HttpContext.Current.Server.MapPath(picPath);
                }

                List<HttpParameter> files = new List<HttpParameter>();
                files.Add(new HttpParameter("pic", picPath));

                string urlAddWeibo = string.Format("{0}/2/statuses/upload.json", this.config.GetBaseUrl());
                List<HttpParameter> parameters = new List<HttpParameter>();
                parameters.Add(new HttpParameter("access_token", this.Token.AccessToken)); //采用OAuth授权方式为必填参数，其他授权方式不需要此参数，OAuth授权后获得。
                parameters.Add(new HttpParameter("status", content));
                parameters.Add(new HttpParameter("lat", jing));
                parameters.Add(new HttpParameter("long", wei));
                parameters.Add(new HttpParameter("annotations", annotations));
                
                string jsonResponse = HttpUtil.HttpPost(urlAddWeibo, parameters, files);
                SinaWeibo weibo = JsonConvert.JavascriptDeserialize<SinaWeibo>(jsonResponse);
                return weibo;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


        #region __关系__
        #endregion
    }
}
