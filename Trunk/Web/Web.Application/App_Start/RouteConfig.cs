using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AttributeRouting.Web.Mvc;

namespace SportsWebPt.Platform.Web.Application
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapAttributeRoutes(c =>
                {
                    c.AddRoutesFromAssembly(Assembly.GetExecutingAssembly());
                });

            routes.MapRoute(
                "Default", // Route name
                "{*catchall}", // URL with parameters
                new { controller = "Index", action = "Index" } // Parameter defaults
            );

        }
    }
}