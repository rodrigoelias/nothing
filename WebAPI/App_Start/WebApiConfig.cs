using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI.App_Start;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new HandleExceptionFilterAttribute());
            
            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
