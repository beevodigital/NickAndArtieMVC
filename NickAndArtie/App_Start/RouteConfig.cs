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
                name: "ManageLogin",
                url: "home/login",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ManagePhotoReel",
                url: "ManagePhotoReel/{action}/{id}",
                defaults: new { controller = "ManagePhotoReel", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ManagePodcasts",
                url: "ManagePodcasts/{action}/{id}",
                defaults: new { controller = "ManagePodcasts", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ManagePosts",
                url: "ManagePosts/{action}/{id}",
                defaults: new { controller = "ManagePosts", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ManageRoadTrips",
                url: "ManageRoadTrips/{action}/{id}",
                defaults: new { controller = "ManageRoadTrips", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ManageSlideShows",
                url: "ManageSlideShows/{action}/{id}",
                defaults: new { controller = "ManageSlideShows", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ManageYoutubeVideos",
                url: "ManageYoutubeVideos/{action}/{id}",
                defaults: new { controller = "ManageYoutubeVideos", action = "Index", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    name: "UnderContruction",
            //    url: "{*url}",
            //    defaults: new { controller = "Utility", action = "Index", id = UrlParameter.Optional }
            //);

           

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