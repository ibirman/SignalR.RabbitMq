﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using RabbitMQ.Client;

namespace SignalR.RabbitMQ.Example
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteTable.Routes.MapHubs();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
            var factory = new ConnectionFactory
            {
                UserName = "admin",
                Password = "admin",
                HostName = "http://vbrick-haproxy-1.lab.vb.loc/",
				AutomaticRecoveryEnabled = true,
				TopologyRecoveryEnabled = true
            };

            var exchangeName = "SignalR.RabbitMQ-Example";
            var configuration = new RabbitMqScaleoutConfiguration(factory, exchangeName, "");
            GlobalHost.DependencyResolver.UseRabbitMq(configuration);
        }
    }
}