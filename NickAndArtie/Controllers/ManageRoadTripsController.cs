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
    public class ManageRoadTripsController : Controller
    {
        private NickAndArtieDB db = new NickAndArtieDB();

        //
        // GET: /ManageRoadTrips/

        public ActionResult Index()
        {
            return View(db.RoadTrips.ToList());
        }

        //
        // GET: /ManageRoadTrips/Details/5

        public ActionResult Details(int id = 0)
        {
            RoadTrip roadtrip = db.RoadTrips.Find(id);
            if (roadtrip == null)
            {
                return HttpNotFound();
            }
            return View(roadtrip);
        }

        //
        // GET: /ManageRoadTrips/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ManageRoadTrips/Create

        [HttpPost]
        public ActionResult Create(RoadTrip roadtrip)
        {
            if (ModelState.IsValid)
            {
                db.RoadTrips.Add(roadtrip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roadtrip);
        }

        //
        // GET: /ManageRoadTrips/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RoadTrip roadtrip = db.RoadTrips.Find(id);
            if (roadtrip == null)
            {
                return HttpNotFound();
            }
            return View(roadtrip);
        }

        //
        // POST: /ManageRoadTrips/Edit/5

        [HttpPost]
        public ActionResult Edit(RoadTrip roadtrip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roadtrip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roadtrip);
        }

        //
        // GET: /ManageRoadTrips/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RoadTrip roadtrip = db.RoadTrips.Find(id);
            if (roadtrip == null)
            {
                return HttpNotFound();
            }
            return View(roadtrip);
        }

        //
        // POST: /ManageRoadTrips/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            RoadTrip roadtrip = db.RoadTrips.Find(id);
            db.RoadTrips.Remove(roadtrip);
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