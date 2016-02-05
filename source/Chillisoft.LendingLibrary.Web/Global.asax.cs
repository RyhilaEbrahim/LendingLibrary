using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.DB.Migrations;
using Chillisoft.LendingLibrary.Web.Models;

namespace Chillisoft.LendingLibrary.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureAutoMapper();
            var connectionString = GetConnectionString();
            MigrateDatabaseWith(connectionString);
        }

        private void ConfigureAutoMapper()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BorrowerViewModel, Borrower>();
                cfg.CreateMap<Borrower, BorrowerViewModel>();
            });
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
