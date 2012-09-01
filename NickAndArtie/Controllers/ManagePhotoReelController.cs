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
    public class ManagePhotoReelController : Controller
    {
        private NickAndArtieDB db = new NickAndArtieDB();

        //
        // GET: /ManagePhotoReel/

        public ActionResult Index()
        {
            return View(db.PhotoReels.ToList());
        }

        //
        // GET: /ManagePhotoReel/Details/5

        public ActionResult Details(int id = 0)
        {
            PhotoReel photoreel = db.PhotoReels.Find(id);
            if (photoreel == null)
            {
                return HttpNotFound();
            }
            return View(photoreel);
        }

        //
        // GET: /ManagePhotoReel/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ManagePhotoReel/Create

        [HttpPost]
        public ActionResult Create(PhotoReel photoreel)
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
                    photoreel.ImageLarge = blob.Uri.ToString();
                }

                if (Request.Files["ImageThumbUpload"].ContentLength > 0)
                {
                    string ThumbsGuid = Guid.NewGuid().ToString();
                    CloudBlob thumbBlob = container.GetBlobReference(ThumbsGuid);
                    thumbBlob.Properties.ContentType = Request.Files["ImageThumbUpload"].ContentType;
                    thumbBlob.UploadFromStream(Request.Files["ImageThumbUpload"].InputStream);
                    photoreel.ImageThumb = thumbBlob.Uri.ToString();
                }

                db.PhotoReels.Add(photoreel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(photoreel);
        }

        //
        // GET: /ManagePhotoReel/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PhotoReel photoreel = db.PhotoReels.Find(id);
            if (photoreel == null)
            {
                return HttpNotFound();
            }
            return View(photoreel);
        }

        //
        // POST: /ManagePhotoReel/Edit/5

        [HttpPost]
        public ActionResult Edit(PhotoReel photoreel)
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
                    photoreel.ImageLarge = blob.Uri.ToString();
                }

                if (Request.Files["ImageThumbUpload"].ContentLength > 0)
                {
                    string ThumbsGuid = Guid.NewGuid().ToString();
                    CloudBlob thumbBlob = container.GetBlobReference(ThumbsGuid);
                    thumbBlob.Properties.ContentType = Request.Files["ImageThumbUpload"].ContentType;
                    thumbBlob.UploadFromStream(Request.Files["ImageThumbUpload"].InputStream);
                    photoreel.ImageThumb = thumbBlob.Uri.ToString();
                }

                db.Entry(photoreel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photoreel);
        }

        //
        // GET: /ManagePhotoReel/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PhotoReel photoreel = db.PhotoReels.Find(id);
            if (photoreel == null)
            {
                return HttpNotFound();
            }
            return View(photoreel);
        }

        //
        // POST: /ManagePhotoReel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PhotoReel photoreel = db.PhotoReels.Find(id);
            db.PhotoReels.Remove(photoreel);
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