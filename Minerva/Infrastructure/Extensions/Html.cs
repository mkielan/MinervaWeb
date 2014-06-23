using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minerva.Infrastructure.Extensions
{
    public static class Html
    {
        public static MvcHtmlString ImageActionLink(this HtmlHelper html, string imageSource, string url, string title = null, bool newPage = false)
        {
            var blank = newPage ? " target=\"_blank\"" : "";
            title = title != null ? " title=" + title : "";

            string imageActionLink = string.Format("<a href=\"{0}\"{2}{3}><img width=\"24\" height=\"24\" src=\"{1}\" /></a>", url, imageSource, title, blank);

            return new MvcHtmlString(imageActionLink);
        }
    }
}