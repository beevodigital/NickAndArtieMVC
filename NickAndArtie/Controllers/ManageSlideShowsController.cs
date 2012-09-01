using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickAndArtie.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace NickAndArtie.Controllers
{
    [Authorize]
    public class ManageSlideShowsController : Controller
    {
        private NickAndArtieDB db = new NickAndArtieDB();

        //
        // GET: /ManageSlideShows/

        public ActionResult Index()
        {
            return View(db.SlideShows.ToList());
        }

        //
        // GET: /ManageSlideShows/Details/5

        public ActionResult Details(int id = 0)
        {
            SlideShow slideshow = db.SlideShows.Find(id);
            if (slideshow == null)
            {
                return HttpNotFound();
            }
            return View(slideshow);
        }

        //
        // GET: /ManageSlideShows/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ManageSlideShows/Create

        [HttpPost]
        public ActionResult Create(SlideShow slideshow)
        {
            if (ModelState.IsValid)
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Configuration.ConfigurationManager.AppSettings["AzureStorageConnectionString"]);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("images");
                container.CreateIfNotExist();
                container.SetPermissions(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
                );

                if (Request.Files["ImageLargeUpload"].ContentLength > 0)
                {
                    string ThisGuid = Guid.NewGuid().ToString();
                    CloudBlob blob = container.GetBlobReference(ThisGuid);
                    blob.Properties.ContentType = Request.Files["ImageLargeUpload"].ContentType;
                    blob.UploadFromStream(Request.Files["ImageLargeUpload"].InputStream);
                    slideshow.ImageLarge = blob.Uri.ToString();
                }

                db.SlideShows.Add(slideshow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slideshow);
        }

        //
        // GET: /ManageSlideShows/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SlideShow slideshow = db.SlideShows.Find(id);
            if (slideshow == null)
            {
                return HttpNotFound();
            }
            return View(slideshow);
        }

        //
        // POST: /ManageSlideShows/Edit/5

        [HttpPost]
        public ActionResult Edit(SlideShow slideshow)
        {
            if (ModelState.IsValid)
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Configuration.ConfigurationManager.AppSettings["AzureStorageConnectionString"]);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("images");
                container.CreateIfNotExist();
                container.SetPermissions(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
                );

                if (Request.Files["ImageLargeUpload"].ContentLength > 0)
                {
                    string ThisGuid = Guid.NewGuid().ToString();
                    CloudBlob blob = container.GetBlobReference(ThisGuid);
                    blob.Properties.ContentType = Request.Files["ImageLargeUpload"].ContentType;
                    blob.UploadFromStream(Request.Files["ImageLargeUpload"].InputStream);
                    slideshow.ImageLarge = blob.Uri.ToString();
                }

                db.Entry(slideshow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slideshow);
        }

        //
        // GET: /ManageSlideShows/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SlideShow slideshow = db.SlideShows.Find(id);
            if (slideshow == null)
            {
                return HttpNotFound();
            }
            return View(slideshow);
        }

        //
        // POST: /ManageSlideShows/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SlideShow slideshow = db.SlideShows.Find(id);
            db.SlideShows.Remove(slideshow);
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