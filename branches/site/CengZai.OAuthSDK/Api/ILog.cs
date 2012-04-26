using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CengZai.OAuthSDK.Api
{
    public interface ILog
    {
        void Error(string msg, Exception ex);
    }
}
