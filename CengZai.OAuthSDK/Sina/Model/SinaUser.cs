using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CengZai.OAuthSDK.Sina
{
    public class SinaUser
    {
        //    "id": 1404376560,
        /// <summary>
        /// 用户UID
        /// </summary>
        public long id { set; get; }

        //"screen_name": "zaku",
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string screen_name { set; get; }

        //"name": "zaku",
        /// <summary>
        /// 友好显示名称
        /// </summary>
        public string name { set; get; }

        //"province": "11",
        /// <summary>
        /// 用户所在地区ID
        /// </summary>
        public int province { set; get; }

        //"city": "5",
        /// <summary>
        /// 用户所在城市ID
        /// </summary>
        public int city { set; get; }

        //"location": "北京 朝阳区",
        /// <summary>
        /// 用户所在地
        /// </summary>
        public string location { set; get; }

        //"description": "人生五十年，乃如梦如幻；有生斯有死，壮士复何憾。",
        /// <summary>
        /// 用户描述
        /// </summary>
        public string description { set; get; }

        //"url": "http://blog.sina.com.cn/zaku",
        /// <summary>
        /// 用户博客地址
        /// </summary>
        public string url { set; get; }

        //"profile_image_url": "http://tp1.sinaimg.cn/1404376560/50/0/1",
        /// <summary>
        /// 
        /// </summary>
        public string profile_image_url { set; get; }

        //"domain": "zaku",
        /// <summary>
        /// 用户的个性化域名
        /// </summary>
        public string domain { set; get; }

        //"gender": "m",
        /// <summary>
        /// 性别，m：男、f：女、n：未知
        /// </summary>
        public string gender { set; get; }

        //"followers_count": 1204,
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int followers_count { set; get; }

        //"friends_count": 447,
        /// <summary>
        /// 关注数
        /// </summary>
        public int friends_count { set; get; }

        //"statuses_count": 2908,
        /// <summary>
        /// 微博数
        /// </summary>
        public int statuses_count { set; get; }

        //"favourites_count": 0,
        /// <summary>
        /// 收藏数
        /// </summary>
        public int favourites_count { set; get; }

        //"created_at": "Fri Aug 28 00:00:00 +0800 2009",
        /// <summary>
        /// 创建时间
        /// </summary>
        public string created_at { set; get; }

        //"following": false,
        /// <summary>
        /// 当前登录用户是否已关注该用户
        /// </summary>
        public bool following { set; get; }

        //"allow_all_act_msg": false,
        /// <summary>
        /// 是否允许所有人给我发私信
        /// </summary>
        public bool allow_all_act_msg { set; get; }

        //"geo_enabled": true,
        /// <summary>
        /// 是否允许带有地理信息
        /// </summary>
        public bool geo_enabled { set; get; }

        //"verified": false,
        /// <summary>
        /// 是否是微博认证用户，即带V用户
        /// </summary>
        public bool verified { set; get; }

        //"allow_all_comment": true,
        /// <summary>
        /// 是否允许所有人对我的微博进行评论
        /// </summary>
        public bool allow_all_comment { set; get; }

        //"avatar_large": "http://tp1.sinaimg.cn/1404376560/180/0/1",
        public string avatar_large { set; get; }

        //"verified_reason": "认证原因",
        public string verified_reason { set; get; }

        //"follow_me": false,
        /// <summary>
        /// 该用户是否关注当前登录用户
        /// </summary>
        public bool follow_me { set; get; }

        //"online_status": 0,
        /// <summary>
        /// 用户的在线状态，0：不在线、1：在线
        /// </summary>
        public int online_status { set; get; }

        //"bi_followers_count": 215
        /// <summary>
        /// 用户的互粉数
        /// </summary>
        public int bi_followers_count { set; get; }


        //"status": {
        //    "created_at": "Tue May 24 18:04:53 +0800 2011",
        //    "id": 11142488790,
        //    "text": "我的相机到了。",
        //    "source": "<a href="http://weibo.com" rel="nofollow">新浪微博</a>",
        //    "favorited": false,
        //    "truncated": false,
        //    "in_reply_to_status_id": "",
        //    "in_reply_to_user_id": "",
        //    "in_reply_to_screen_name": "",
        //    "geo": null,
        //    "mid": "5610221544300749636",
        //    "annotations": [],
        //    "reposts_count": 5,
        //    "comments_count": 8
        //},
    }
}
