using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CengZai.OAuthSDK.QQ
{
    /// <summary>
    /// QQ登录用户
    /// </summary>
    public class QQUser : QQReturn
    {
        /// <summary>
        /// nickname: 昵称
        /// </summary>
        public string nickname { set; get; }

        /// <summary>
        /// figureurl: 大小为30×30像素的头像URL
        /// </summary>
        public string figureurl { set; get; }

        /// <summary>
        /// figureurl_1: 大小为50×50像素的头像URL
        /// </summary>
        public string figureurl_1 { set; get; }

        /// <summary>
        /// figureurl_2: 大小为100×100像素的头像URL
        /// </summary>
        public string figureurl_2 { set; get; }

        /// <summary>
        /// gender: 性别。如果获取不到则默认返回“男”
        /// </summary>
        public string gender { set; get; }

        /// <summary>
        /// vip: 标识用户是否为黄钻用户（0：不是；1：是）
        /// </summary>
        public string vip { set; get; }

        /// <summary>
        /// level: 黄钻等级（如果是黄钻用户才返回此参数）
        /// </summary>
        public string level { set; get; }
     }
}
