using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NickAndArtie.Controllers
{
    public class PodcastsController : Controller
    {
        //
        // GET: /Podcasts/

        public ActionResult Index()
        {
            return View();
        }

    }
}
