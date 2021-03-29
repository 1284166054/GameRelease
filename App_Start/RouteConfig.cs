using System;
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
               name: "seting",
               url: "seting/{id}",
               defaults: new { controller = "Admin", action = "seting", id = UrlParameter.Optional }
          );

            routes.MapRoute(
               name: "dashboard",
               url: "dashboard/{id}",
               defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
          );

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
                name: "Logout",
                url: "Logout/{id}",
                defaults: new { controller = "Admin", action = "Logout", id = UrlParameter.Optional }
           );


            routes.MapRoute(
                name: "Verification",
                url: "Verification/{id}",
                defaults: new { controller = "Home", action = "Verification", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

        }
    }
}
