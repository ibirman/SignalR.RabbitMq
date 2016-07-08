using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;

namespace SignalR.RabbitMQ.Example
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional }
            );
        }
    }
}
