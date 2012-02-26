using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using JokeStore.Core.Repository.EntityFramework;
using JokeStore.Web.Core;

namespace JokeStore.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "", 
                new { controller = "Entry", action = "List", category = (string)null, page = 1 }
            );

            routes.MapRoute(null,
                "page{page}",
                new { controller = "Entry", action = "List", category = (string)null },
                new { page = @"\d+" } // Constraints: page must be numerical
            );

            routes.MapRoute(null,
                "{category}",
                new { controller = "Entry", action = "List", page = 1 }
            );

            routes.MapRoute(null,
                "{category}/page{page}",
                new { controller = "Entry", action = "List" },
                new { page = @"\d+" }
            );

            routes.MapRoute(null,
                "vote/{entryID}/{direction}",
                new { controller = "Entry", action = "Vote" },
                new { entryID = @"\d+" }
            );

            routes.MapRoute(null, "{controller}/{action}");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer<DataContext>(new DataContextInitializer());
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }
    }
}