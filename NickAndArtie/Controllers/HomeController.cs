using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using NickAndArtie.Models;
using System.Web.Security;

namespace NickAndArtie.Controllers
{
    public class HomeController : Controller
    {
        private NickAndArtieDB db = new NickAndArtieDB();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Podcasts = db.Podcasts.OrderByDescending(x => x.DatePublished).Take(3).ToList();
            ViewBag.SlideShowImages = db.SlideShows.OrderByDescending(x => x.ID).Take(5).ToList();
            ViewBag.Posts = db.Posts.OrderByDescending(x => x.AirDate).Take(7).ToList();
            ViewBag.PhotoReel = db.PhotoReels.OrderByDescending(x => x.ID).Take(10).ToList();
            ViewBag.RoadTrips = db.RoadTrips.OrderBy(x => x.DateOfEvent).ToList();

            ViewBag.RoadTrips2 = db.RoadTrips.OrderBy(x => x.DateOfEvent).ToList();

            var userAgent = Request.UserAgent.ToLower();
            if (userAgent.Contains("iphone") || userAgent.Contains("android"))
            {
                ViewBag.IsMobile = true;
            }
            else if (userAgent.Contains("ipad"))
            {
                ViewBag.IsMobile = true;
            }
            else
            {
                ViewBag.IsMobile = false;
            }
            
            return View();
        }

        public ActionResult Login()
        {
            // Ensure there's a return URL
            if (Request.QueryString["ReturnUrl"] == null)
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(FormsAuthentication.DefaultUrl));

            if (TempData.ContainsKey("allowLogin"))
            {
                // See if they've supplied credentials
                string authHeader = Request.Headers["Authorization"];
                if ((authHeader != null) && (authHeader.StartsWith("Basic")))
                {
                    // Parse username and password out of the HTTP headers
                    authHeader = authHeader.Substring("Basic".Length).Trim();
                    byte[] authHeaderBytes = Convert.FromBase64String(authHeader);
                    authHeader = Encoding.UTF7.GetString(authHeaderBytes);
                    string userName = authHeader.Split(':')[0];
                    string password = authHeader.Split(':')[1];

                    // Validate login attempt
                    if (FormsAuthentication.Authenticate(userName, password))
                    {
                        FormsAuthentication.RedirectFromLoginPage(userName, false);
                        return Redirect("/manage");
                    }
                }
            }

            // Force the browser to pop up the login prompt
            Response.StatusCode = 401;
            Response.AppendHeader("WWW-Authenticate", "Basic");
            TempData["allowLogin"] = true;

            // This gets shown if they click "Cancel" to the login prompt
            Response.Write("You must log in to access this URL.");
            return View();
        }
    }
}
