using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Risk.Models;

namespace Risk
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            

            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Inicio",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Inicio", action = "Inicio", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BusquedaRiks",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Assign", action = "BusquedaRiks", datosTablaGeneralBusqueda = new DatosTablaModel() }
            );
        }
    }
}
