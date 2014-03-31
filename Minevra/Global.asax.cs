using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Minevra
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // todo close install windsor container
        }

        /// <summary>
        /// Installl Windsor Castole IoC container
        /// </summary>
        private static void InjectContainer()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());

            // todo register classes
        }

        /// <summary>
        /// Destroy Windsor Castle IoC container
        /// </summary>
        protected void Application_End()
        {
            _container.Dispose();
        }
    }
}
