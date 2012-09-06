using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NickAndArtie
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Itunes",
                url: "wp-content/podcast/feed.xml",
                defaults: new { controller = "Podcasts", action = "Feed", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AdminHome",
                url: "manage",
                defaults: new { controller = "ManagePosts", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PostDetail",
                url: "post/{id}",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}