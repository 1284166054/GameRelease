﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace 游戏发布站
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            



            routes.MapRoute(
                name: "Reg",
                url: "Reg/{id}",
                defaults: new { controller = "Admin", action = "Reg", id = UrlParameter.Optional }
           );



            routes.MapRoute(
                name: "Login",
                url: "Login/{id}",
                defaults: new { controller = "Admin", action = "Login", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}