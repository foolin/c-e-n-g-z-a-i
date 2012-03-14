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
            return new CengZai.BLL.Lover().GetCertificateName(certificate);
        }
    }
}
