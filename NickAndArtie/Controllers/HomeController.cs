using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickAndArtie.Models;

namespace NickAndArtie.Controllers
{
    public class HomeController : Controller
    {
        private NickAndArtieDB db = new NickAndArtieDB();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Podcasts = db.Podcasts.OrderByDescending(x => x.DatePublished).Take(5).ToList();
            ViewBag.SlideShowImages = db.SlideShows.OrderByDescending(x => x.ID).Take(5).ToList();
            ViewBag.Posts = db.Posts.OrderByDescending(x => x.AirDate).Take(7).ToList();
            ViewBag.PhotoReel = db.PhotoReels.OrderByDescending(x => x.ID).Take(10).ToList();
            ViewBag.RoadTrips = db.RoadTrips.OrderBy(x => x.DateOfEvent).ToList();

            return View();
        }
    }
}
