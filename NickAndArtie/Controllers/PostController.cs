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

        public ActionResult Index(string id)
        {
            ViewBag.Podcasts = db.Podcasts.OrderByDescending(x => x.DatePublished).Take(15).ToList();
            ViewBag.Post = db.Posts.Where(x => x.Slug.Equals(id)).FirstOrDefault();
            return View();
        }

    }
}
