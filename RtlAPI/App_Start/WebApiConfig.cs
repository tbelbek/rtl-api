using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RtlAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "FetchData",
                "values/fetch-data",
                new { controller = "Values", action = "FetchData" }
            );

            config.Routes.MapHttpRoute(
                "GetData",
                "values/get-data",
                new { controller = "Values", action = "GetData" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
