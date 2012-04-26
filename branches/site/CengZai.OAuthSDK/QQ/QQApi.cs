using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CengZai.OAuthSDK.Api;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace CengZai.OAuthSDK.QQ
{
    public class QQApi : BaseApi
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        QQConfig config = new QQConfig();

        public QQApi()
        {
        }

        #region __授权___
        /// <summary>
        /// 跳转到服务器获取Authorization Code
        /// </summary>
        /// <param name="state">client端的状态值。用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回。</param>
        /// <param name="scope">请求用户授权时向用户显示的可进行授权的列表。可填写的值是【QQ登录】API文档中列出的接口，
        /// 以及一些动作型的授权（目前仅有：do_like），如果要填写多个接口名称，请用逗号隔开。
        /// 例如：scope=get_user_info,add_share,list_album,upload_pic,check_page_fans,add_t,add_pic_t,del_t,get_repost_list,get_info,get_other_info 
        /// get_fanslist,get_idolist,add_idol,del_idol
        /// 不传则默认请求对接口get_user_info进行授权。
        /// 建议控制授权项的数量，只传入必要的接口名称，因为授权项越多，用户越可能拒绝进行任何授权。</param>
        /// <returns></returns>
        public string GetAuthorizationUrl(string state, string scope = "")
        {
            string url = string.Format("{0}/oauth2.0/authorize?response_type=code&client_id={1}&redirect_uri={2}&state={3}"
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
                string urlAccessToken = string.Format("{0}/oauth2.0/token?grant_type=authorization_code&code={1}&client_id={2}&client_secret={3}&redirect_uri={4}"
                    , this.config.GetBaseUrl()
                    , code
                    , this.config.GetAppKey()
                    , this.config.GetAppSecret()
                    , this.config.GetCallbackUrl());
                string responseAccessToken = HttpUtil.HttpGet(urlAccessToken);
                NameValueCollection nvcParameters = HelpUtil.StringToParameters(responseAccessToken);
                if (nvcParameters != null)
                {
                    string accessToken = nvcParameters["access_token"];
                    int expiresIn = 0;
                    int.TryParse(nvcParameters["expires_in"], out expiresIn);
                    if (string.IsNullOrEmpty(accessToken))
                    {
                        return false;
                    }
                    string urlOpenId = string.Format("{0}/oauth2.0/me?access_token={1}", this.config.GetBaseUrl(), accessToken);
                    string responseOpenId = HttpUtil.HttpGet(urlOpenId);
                    Match matchOpenId = Regex.Match(responseOpenId, @"openid"":""(?<openid>\w+)""");
                    if (!matchOpenId.Success)
                    {
                        return false;
                    }
                    string openId = matchOpenId.Groups["openid"].Value;
                    this.Token = new OAuthToken
                    {
                        AccessToken = accessToken,
                        ExpiresAt = expiresIn,
                        Uid = openId
                    };
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region __QZone__

        /// <summary>
        /// 获取登录用户信息，目前可获取用户在QQ空间的昵称、头像信息及黄钻信息。
        /// </summary>
        public QQUser GetUserInfo()
        {
            QQUser user = null;
            try
            {
                string urlGetUserInfo = string.Format("{0}/user/get_user_info?{1}"
                    , this.config.GetBaseUrl()
                    , this._createCommonGetParams());
                string jsonUserInfo = HttpUtil.HttpGet(urlGetUserInfo);
                user = JsonConvert.JavascriptDeserialize<QQUser>(jsonUserInfo);
                if (user == null || user.ret > 0)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return user;
        }

        /// <summary>
        /// 第三方网站可以调用本分享接口，在用户授权的情况下，可以以用户的名义发布一条动态（feeds）到QQ空间和朋友网上，
        /// 此外还可在腾讯微博上发一条微博（用户可自己选择是否转发到微博）。
        /// 1. 网站主动推送。当用户在网站上进行操作（例如上传视频，图片，发表评论等）后，以该用户的名义发布一条feeds到QQ空间中。 
        /// 2. 用户主动分享。用户在网站上点击“分享”按钮，发布一条feeds到QQ空间中，分享某个视频，网页或者其它内容。 例如：某用户在某个第三方网站上对某条新闻发表了评论，网站将以该用户的名义发表一条动态到QQ空间中，动态的具体展示如下： 
        /// 上图中的1-6标注对feeds的组成以及规格进行了说明： 
        /// 1. 用户评论：用户在第三方网站发布的评论等UGC信息，选填项。
        /// 2. 分享的内容标题，含源网页URL，点击跳转至第三方网站网页，必填项。
        /// 3. 详细描述：网页摘要，选填项。
        /// 4. 外部图片：引用外部图片（大小不超过100 x 100 px），选填项。
        /// 5. 分享的场景：支持以下场景 1.通过网页 2.通过手机 3.通过软件 4.通过IPHONE 5.通过IPAD，选填项。
        /// 6：来源网站名称及域名：标明分享的来源，必填项。
        /// </summary>
        /// <param name="title">feeds的标题，对应上文接口说明中的2。最长36个中文字，超出部分会被截断。</param>
        /// <param name="url">分享所在网页资源的链接，点击后跳转至第三方网页，对应上文接口说明中2的超链接。请以http://开头。 </param>
        /// <param name="site">分享的来源说明。 </param>
        /// <param name="comment">用户评论内容，也叫发表分享时的分享理由，对应上文接口说明的1。禁止使用系统生产的语句进行代替。最长40个中文字，超出部分会被截断。</param>
        /// <param name="summary">所分享的网页资源的摘要内容，或者是网页的概要描述，对应上文接口说明的3。最长80个中文字，超出部分会被截断。</param>
        /// <param name="images">所分享的网页资源的代表性图片链接"，对应上文接口说明的4。
        /// 请以http://开头，长度限制255字符。多张图片以竖线（|）分隔，目前只有第一张图片有效，图片规格100*100为佳。</param>
        /// <param name="source">分享的场景，对应上文接口说明的6。取值说明：1.通过网页 2.通过手机 3.通过软件 4.通过IPHONE 5.通过 IPAD。 </param>
        /// <param name="type">分享内容的类型。4表示网页；5表示视频（type=5时，必须传入playurl）。 </param>
        /// <param name="playurl">长度限制为256字节。仅在type=5的时候有效。</param>
        /// <param name="nswb">值为1时，表示分享不默认同步到微博，其他值或者不传此参数表示默认同步到微博</param>
        /// <returns>是否成功</returns>
        public bool AddShare(string title, string url, string site="", string comment = "", string summary = "", string images = "", string source = "", string type = "", string playurl = "", string nswb="")
        {
            try
            {
                string urlAddShare = string.Format("{0}/share/add_share", this.config.GetBaseUrl());
                Dictionary<object, object> parameters = _createCommonPostParams();
                parameters.Add("title", title);
                parameters.Add("url", url);
                //parameters.Add("site", Uri.EscapeUriString(site));
                parameters.Add("comment", comment);
                parameters.Add("summary", summary);
                parameters.Add("images", images);
                parameters.Add("source", source);
                parameters.Add("type", type);
                parameters.Add("playurl", playurl);
                parameters.Add("nswb", nswb);
                string jsonResponse = HttpUtil.HttpPost(urlAddShare, parameters);
                QQReturn qqreturn = JsonConvert.JavascriptDeserialize<QQReturn>(jsonResponse);
                if (qqreturn == null || qqreturn.ret > 0)
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

        #region __微博__
        #endregion



        #region __私有辅助方法__
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string _createCommonGetParams()
        {
            if(this.Token == null){
                return "";
            }
            return string.Format(@"access_token={0}&oauth_consumer_key={1}&openid={2}&format=json"
                , this.Token.AccessToken
                , this.config.GetAppKey()
                , this.Token.Uid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private Dictionary<object, object> _createCommonPostParams()
        {
            Dictionary<object, object> dic = new Dictionary<object, object>();
            dic.Add("access_token", this.Token.AccessToken);
            dic.Add("oauth_consumer_key",this.config.GetAppKey());
            dic.Add("openid",this.Token.Uid);
            dic.Add("format","json");
            return dic;
        }
        #endregion

    }
}
