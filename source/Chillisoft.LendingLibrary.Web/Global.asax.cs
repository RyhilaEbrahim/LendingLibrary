using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.DB.Migrations;
using Chillisoft.LendingLibrary.Web.Bootstrappers;
using Chillisoft.LendingLibrary.Web.Bootstrappers.AutoMapper;
using Chillisoft.LendingLibrary.Web.Bootstrappers.Ioc;
using Chillisoft.LendingLibrary.Web.Models;

namespace Chillisoft.LendingLibrary.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container = new WindsorContainer();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var connectionString = GetConnectionString();
            MigrateDatabaseWith(connectionString);
            BootstrapContainer();
        }

        protected void Application_End()
        {
            if (_container != null) _container.Dispose();
        }

        private static void BootstrapContainer()
        {
            _container = new WindsorBootstrapper().Boostrap();
        }
  
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        }

        private void MigrateDatabaseWith(string connectionString)
        {
            var runner = new Migrator(connectionString);
            runner.MigrateToLatest();
        }
        

    }
}
