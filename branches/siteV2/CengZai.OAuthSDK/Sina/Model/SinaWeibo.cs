using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CengZai.OAuthSDK.Sina
{
    public class SinaWeibo
    {
        //"created_at": "Tue May 31 17:46:55 +0800 2011",
        /// <summary>
        /// 创建时间
        /// </summary>
        public string created_at { set; get; }

        //"id": 11488058246,
        /// <summary>
        /// 微博ID
        /// </summary>
        public int id { set; get; }

        //"text": "求关注。"，
        /// <summary>
        /// 微博信息内容
        /// </summary>
        public string text { set; get; }

        //"source": "<a href="http://weibo.com" rel="nofollow">新浪微博</a>",
        /// <summary>
        /// 微博来源
        /// </summary>
        public string source { set; get; }

        //"favorited": false,
        /// <summary>
        /// 是否已收藏
        /// </summary>
        public bool favorited { set; get; }

        //"truncated": false,
        /// <summary>
        /// 是否被截断
        /// </summary>
        public bool truncated { set; get; }

        //"in_reply_to_status_id": "",
        /// <summary>
        /// 回复ID
        /// </summary>
        public long in_reply_to_status_id { set; get; }

        //"in_reply_to_user_id": "",
        /// <summary>
        /// 回复人UID
        /// </summary>
        public long in_reply_to_user_id { set; get; }

        //"in_reply_to_screen_name": "",
        /// <summary>
        /// 回复人昵称
        /// </summary>
        public string in_reply_to_screen_name { set; get; }

        //"geo": null,
        /// <summary>
        /// 地理信息字段
        /// </summary>
        public SinaGeos geo { set; get; }

        //"mid": "5612814510546515491",
        /// <summary>
        /// 微博MID
        /// </summary>
        public long mid { set; get; }

        //"reposts_count": 8,
        /// <summary>
        /// 转发数
        /// </summary>
        public int reposts_count { set; get; }

        //"comments_count": 9,
        /// <summary>
        /// 评论数
        /// </summary>
        public int comments_count { set; get; }

        //"annotations": [],
        /// <summary>
        /// 微博附加注释信息
        /// </summary>
        public string[] annotations { set; get; }

        /// <summary>
        /// 微博作者的用户信息字段
        /// </summary>
        public SinaUser user { set; get; }
    }
}
