using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;

namespace ToDoListV99
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //To allow for automatic migrations:
            //inform framework about which context to check and about config.
            //this came from YouTube, Aristotele Pitaridis, Asp.net mvc 5: 3.4 Model-Enable automatic migration
            GlobalConfiguration.Configure(WebApiConfig.Register);
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<Models.MyDbContext, Migrations.Configuration>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
