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
using Minerva.Infrastructure;
using System.Reflection;
using Minerva.Repositories;
using Minerva.Entities;
using Minerva.Entities.Sources;

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

            //ControllerBuilder.Current.SetControllerFactory(new ControllerFactory(_container.Kernel));
        }

        /// <summary>
        /// Installl Windsor Castle IoC container
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
            /*
            _container.Register(
                Component.For<GenericRepository<MinervaDbContext, Directory, Int64>>()
                    .ImplementedBy<DirectoryRepository>()
                    //.LifestyleSingleton()
                );

            _container.Register(
                Component.For<GenericRepository<MinervaDbContext, Comment, Int64>>()
                    .ImplementedBy<CommentRepository>()
                    //.LifestyleSingleton()
                );

            _container.Register(
                Component.For<GenericRepository<MinervaDbContext, DiskStructure, Int64>>()
                    .ImplementedBy<DiskStructureRepository>()
                    //.LifestyleSingleton()
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
