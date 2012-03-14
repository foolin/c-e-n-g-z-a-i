using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class HtmlExtendForApp
    {
        public static string GetCertificateName(this HtmlHelper helper, int? certificate)
        {
            if (certificate == (int)CengZai.Model.LoverCertificate.Love)
            {
                return "恋爱证";
            }
            if (certificate == (int)CengZai.Model.LoverCertificate.Marry)
            {
                return "结婚证";
            }

            return "";
        }
    }
}
