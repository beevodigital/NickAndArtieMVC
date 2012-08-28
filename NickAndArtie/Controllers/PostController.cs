using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickAndArtie.Models;

namespace NickAndArtie.Controllers
{
    public class PostController : Controller
    {
        private NickAndArtieDB db = new NickAndArtieDB();
        //
        // GET: /Post/

        public ActionResult Index()
        {
            ViewBag.Podcasts = db.Podcasts.OrderByDescending(x => x.DatePublished).Take(15).ToList();
            return View();
        }

    }
}
