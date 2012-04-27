using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CengZai.OAuthSDK.Sina
{
    internal class _AccessTokenModel
    {
       //"access_token": "ACCESS_TOKEN",
        public string access_token { set; get; }

       //"expires_in": 1234,
        public int expires_in { set; get; }

       //"remind_in":"798114",
        public int remind_in { set; get; }

       //"uid":"12341234"
        public long uid { set; get; }
    }
}
