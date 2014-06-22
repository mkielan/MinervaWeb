using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minerva.Infrastructure.Extensions
{
    public static class WebExtensions
    {
        public static string ControllerName(this WebViewPage @this)
        {
            return HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
        }

        public static string ActionName(this WebViewPage @this)
        {
            return HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
        }
    }
}