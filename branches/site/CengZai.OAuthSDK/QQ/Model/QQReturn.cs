using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CengZai.OAuthSDK.QQ
{
    public class QQReturn
    {
        /// <summary>
        /// ret: 返回码
        /// </summary>
        public int ret { set; get; }

        /// <summary>
        /// msg: 如果ret＜0，会有相应的错误信息提示，返回数据全部用UTF-8编码
        /// </summary>
        public string msg { set; get; }

        /// <summary>
        /// 腾讯微博专用二级错误码
        /// </summary>
        public int errcode { set; get; } 
    }
}
