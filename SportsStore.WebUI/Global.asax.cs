using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SportStore.Domain.Entities;
using SportsStore.WebUI.Binders;
using SportsStore.WebUI.Infrastructure;

namespace SportsStore.WebUI
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
                            "", //When URL is Empty
                            new
                                {
                                    controller = "Product",
                                    action = "List",
                                    category = (string) null,
                                    page = 1
                                }
                );


            routes.MapRoute(        
                null,
                "Page{page}",       //Matches Page1 / Page2 etc  but not PageABC
                new { Controller = "Product", action = "List", category=(string)null },
                new { page = @"\d+"}    //Constraints: page must be numerical
                );


            routes.MapRoute(null,
                            "{category}", //Matches /Football or /AnythingWithNoSlash
                            new {Controller = "Product", action = "List", page = 1}
                );

            routes.MapRoute(null,
                            "{category}/Page{page}", //Matches /Football/Page567 
                            new { Controller = "Product", action = "List" }
                );


            routes.MapRoute(
                null, 
                "{controller}/{action}"
            );


            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Product", action = "List", id = UrlParameter.Optional} // Parameter defaults
            //);

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            //This allows us to decouple httpcontext etc from our controllers - as this will now be injected in with access to httpContext.
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());

        }
    }
}