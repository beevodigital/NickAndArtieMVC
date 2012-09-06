using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickAndArtie.Models;

namespace NickAndArtie.Controllers
{
    [Authorize]
    public class ManageYoutubeVideosController : Controller
    {
        private NickAndArtieDB db = new NickAndArtieDB();

        //
        // GET: /ManageYoutubeVideos/

        public ActionResult Index()
        {
            return View(db.YoutubeVideos.ToList());
        }

        //
        // GET: /ManageYoutubeVideos/Details/5

        public ActionResult Details(int id = 0)
        {
            YoutubeVideo youtubevideo = db.YoutubeVideos.Find(id);
            if (youtubevideo == null)
            {
                return HttpNotFound();
            }
            return View(youtubevideo);
        }

        //
        // GET: /ManageYoutubeVideos/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ManageYoutubeVideos/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(YoutubeVideo youtubevideo)
        {
            if (ModelState.IsValid)
            {
                db.YoutubeVideos.Add(youtubevideo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(youtubevideo);
        }

        //
        // GET: /ManageYoutubeVideos/Edit/5

        public ActionResult Edit(int id = 0)
        {
            YoutubeVideo youtubevideo = db.YoutubeVideos.Find(id);
            if (youtubevideo == null)
            {
                return HttpNotFound();
            }
            return View(youtubevideo);
        }

        //
        // POST: /ManageYoutubeVideos/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(YoutubeVideo youtubevideo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(youtubevideo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(youtubevideo);
        }

        //
        // GET: /ManageYoutubeVideos/Delete/5

        public ActionResult Delete(int id = 0)
        {
            YoutubeVideo youtubevideo = db.YoutubeVideos.Find(id);
            if (youtubevideo == null)
            {
                return HttpNotFound();
            }
            return View(youtubevideo);
        }

        //
        // POST: /ManageYoutubeVideos/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            YoutubeVideo youtubevideo = db.YoutubeVideos.Find(id);
            db.YoutubeVideos.Remove(youtubevideo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}