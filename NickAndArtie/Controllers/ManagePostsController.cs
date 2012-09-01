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
    public class ManagePostsController : Controller
    {
        private NickAndArtieDB db = new NickAndArtieDB();

        //
        // GET: /ManagePosts/

        public ActionResult Index()
        {
          return View(db.Posts.ToList());
        }

        //
        // GET: /ManagePosts/Details/5

        public ActionResult Details(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // GET: /ManagePosts/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ManagePosts/Create

        [HttpPost]
        public ActionResult Create(Post post)
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
                    post.ImageLarge = blob.Uri.ToString();
                }

                if (Request.Files["ImageThumbUpload"].ContentLength > 0)
                {
                    string ThumbsGuid = Guid.NewGuid().ToString();
                    CloudBlob thumbBlob = container.GetBlobReference(ThumbsGuid);
                    thumbBlob.Properties.ContentType = Request.Files["ImageThumbUpload"].ContentType;
                    thumbBlob.UploadFromStream(Request.Files["ImageThumbUpload"].InputStream);
                    post.ImageThumb = thumbBlob.Uri.ToString();
                }
                string stopHere = "";

                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        //
        // GET: /ManagePosts/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Post post = db.Posts.Find(id);
            
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /ManagePosts/Edit/5

        [HttpPost]
        public ActionResult Edit(Post post)
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
                    post.ImageLarge = blob.Uri.ToString();
                }

                if (Request.Files["ImageThumbUpload"].ContentLength > 0)
                {
                    string ThumbsGuid = Guid.NewGuid().ToString();
                    CloudBlob thumbBlob = container.GetBlobReference(ThumbsGuid);
                    thumbBlob.Properties.ContentType = Request.Files["ImageThumbUpload"].ContentType;
                    thumbBlob.UploadFromStream(Request.Files["ImageThumbUpload"].InputStream);
                    post.ImageThumb = thumbBlob.Uri.ToString();
                }

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        //
        // GET: /ManagePosts/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /ManagePosts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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