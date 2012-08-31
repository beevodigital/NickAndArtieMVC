using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using NickAndArtie.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace NickAndArtie.Controllers
{
    public class ManagePodcastsController : Controller
    {
        private NickAndArtieDB db = new NickAndArtieDB();

        //
        // GET: /ManagePodcasts/

        public ActionResult Import()
        {
            XDocument ThisFeed = XDocument.Load("http://www.nickandartie.com/pickle/odplaylist.xml");

            Response.Write(ThisFeed.Element("playlist").Elements().Count() + "<br/>");

            foreach (var ThisItem in ThisFeed.Element("playlist").Elements().Reverse())
            {
                var ThisPodcast = new Podcast();
                ThisPodcast.FileName = ThisItem.Element("filename").Value;
                ThisPodcast.DatePublished = DateTime.Now;
                ThisPodcast.Image = ThisItem.Element("image").Value;
                ThisPodcast.Title = ThisItem.Element("title").Value;
                ThisPodcast.Artist = ThisItem.Element("artist").Value;
                ThisPodcast.DateCreated = DateTime.Now;

                db.Podcasts.Add(ThisPodcast);
                db.SaveChanges();
            }

            return Content("");
        }

        public ActionResult Index()
        {
            return View(db.Podcasts.OrderByDescending(x => x.DatePublished).ToList());
        }

        //
        // GET: /ManagePodcasts/Details/5

        public ActionResult Details(int id = 0)
        {
            Podcast podcast = db.Podcasts.Find(id);
            if (podcast == null)
            {
                return HttpNotFound();
            }
            return View(podcast);
        }

        //
        // GET: /ManagePodcasts/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ManagePodcasts/Create

        [HttpPost]
        public ActionResult Create(Podcast podcast)
        {
            if (ModelState.IsValid)
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Configuration.ConfigurationManager.AppSettings["AzureStorageConnectionString"]);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("podcasts");
                container.CreateIfNotExist();
                container.SetPermissions(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
                );

                if (Request.Files["FileNameUpload"].ContentLength > 0)
                {
                    string ThisGuid = Guid.NewGuid().ToString();
                    CloudBlob blob = container.GetBlobReference(ThisGuid);
                    blob.Properties.ContentType = Request.Files["FileNameUpload"].ContentType;
                    blob.UploadFromStream(Request.Files["FileNameUpload"].InputStream);
                    podcast.FileName = blob.Uri.ToString();
                }

                db.Podcasts.Add(podcast);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(podcast);
        }

        //
        // GET: /ManagePodcasts/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Podcast podcast = db.Podcasts.Find(id);
            if (podcast == null)
            {
                return HttpNotFound();
            }
            return View(podcast);
        }

        //
        // POST: /ManagePodcasts/Edit/5

        [HttpPost]
        public ActionResult Edit(Podcast podcast)
        {
            if (ModelState.IsValid)
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Configuration.ConfigurationManager.AppSettings["AzureStorageConnectionString"]);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("podcasts");
                container.CreateIfNotExist();
                container.SetPermissions(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
                );

                if (Request.Files["FileNameUpload"].ContentLength > 0)
                {
                    string ThisGuid = Guid.NewGuid().ToString();
                    CloudBlob blob = container.GetBlobReference(ThisGuid);
                    blob.Properties.ContentType = Request.Files["FileNameUpload"].ContentType;
                    blob.UploadFromStream(Request.Files["FileNameUpload"].InputStream);
                    podcast.FileName = blob.Uri.ToString();
                }

                db.Entry(podcast).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(podcast);
        }

        //
        // GET: /ManagePodcasts/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Podcast podcast = db.Podcasts.Find(id);
            if (podcast == null)
            {
                return HttpNotFound();
            }
            return View(podcast);
        }

        //
        // POST: /ManagePodcasts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Podcast podcast = db.Podcasts.Find(id);
            db.Podcasts.Remove(podcast);
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