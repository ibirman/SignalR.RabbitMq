﻿using System;
using System.Web.Mvc;

namespace SignalR.RabbitMQ.Example
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}