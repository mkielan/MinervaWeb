using Castle.MicroKernel.Registration;
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
using Minerva.Models;
using Minerva.Infrastructore;
using System.Reflection;

namespace Minerva
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            InjectContainer();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory(_container.Kernel));
        }

        /// <summary>
        /// Installl Windsor Castole IoC container
        /// </summary>
        private static void InjectContainer()
        {
            _container = new WindsorContainer();

            _container.Install(FromAssembly.This());
            /*
            _container.Register(
                Component.For<INodeService>()
                    .ImplementedBy<NodeServiceClient>()
                    .LifestyleSingleton()
                );*/
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
