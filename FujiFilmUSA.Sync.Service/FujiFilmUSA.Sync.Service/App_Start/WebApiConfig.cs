using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FujiFilmUSA.Sync.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //redirect to swagger ui automatically
            config.Routes.MapHttpRoute(
            name: "Swagger UI",
            routeTemplate: "",
            defaults: null,
            constraints: null,
            handler: new RedirectHandler(message => message.RequestUri.ToString().TrimEnd('/'), "swagger/ui/index"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "ChartPDF", action = "GetGraphicsChartFiles", id = RouteParameter.Optional }
            );
        }
    }
}
