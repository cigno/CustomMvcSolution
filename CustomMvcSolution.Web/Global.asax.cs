// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper restore RedundantUsingDirective
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CustomMvcSolution.Web.Infrastructure;

namespace CustomMvcSolution.Web
{
    // Nota: per istruzioni su come abilitare la modalità classica di IIS6 o IIS7, 
    // visitare il sito Web all'indirizzo http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Nome route
                "{controller}/{action}/{id}", // URL con parametri
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Valori predefiniti parametri
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

                CustomSetup();
        }

        private static void CustomSetup()
        {
            // NINJECT CONTROLLER BIND
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            Database.SetInitializer(new DbInitializer());
        }
    }
}