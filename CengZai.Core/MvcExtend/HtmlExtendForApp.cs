using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace System.Web.Mvc
{
    public static class HtmlExtendForApp
    {
        /// <summary>
        /// 取证书名称
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static string GetCertificateName(this HtmlHelper helper, int? certificate)
        {
            return new CengZai.BLL.Lover().GetCertificateName(certificate);
        }

        /// <summary>
        /// 取状态节点名称
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="flow"></param>
        /// <returns></returns>
        public static string GetLoverFlowName(this HtmlHelper helper, int? flow)
        {
            return new CengZai.BLL.Lover().GetFlowName(flow);
        }

    }
}
