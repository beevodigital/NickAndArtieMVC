using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Web;
using System.Web.Mvc;
using NickAndArtie.Models;
using System.ServiceModel.Syndication;

namespace NickAndArtie.Controllers
{
    public class PodcastsController : Controller
    {
        private NickAndArtieDB db = new NickAndArtieDB();
        //
        // GET: /Podcasts/

        public ActionResult Index()
        {
            ViewBag.YoutubeVideos = db.YoutubeVideos.OrderBy(x => x.SortOrder).Take(5).ToList();
            ViewBag.Podcasts = db.Podcasts.OrderByDescending(x => x.DatePublished).Take(25).ToList();
            return View();
        }

        public ActionResult PopUp()
        {
            ViewBag.Podcasts = db.Podcasts.OrderByDescending(x => x.DatePublished).Take(100).ToList();
            return View();
        }

        public ActionResult Feed()
        {
            XNamespace itunesNS = "http://www.itunes.com/dtds/podcast-1.0.dtd";
            string prefix = "itunes";

            var feed = new SyndicationFeed("The Nick &amp; Artie Show: Podcast", "The Nick &amp; Artie Show - Starring Nick Di Paolo and Artie Lange.", new Uri("http://www.nickandartie"));
            feed.Categories.Add(new SyndicationCategory("Sports &amp; Recreation"));
            feed.AttributeExtensions.Add(new XmlQualifiedName(prefix, "http://www.w3.org/2000/xmlns/"), itunesNS.NamespaceName);
            feed.Copyright = new TextSyndicationContent("2012 DirecTV, Inc. All rights reserved.");
            feed.Language = "en-us";
            feed.Copyright = new TextSyndicationContent(DateTime.Now.Year + " " + "The Nick &amp; Artie Show");
            //feed.ImageUrl = new Uri(imageUrl);
            feed.LastUpdatedTime = DateTime.Now;
            feed.Authors.Add(new SyndicationPerson() { Name = "The Nick &amp; Artie Show", Email = "nickandartie@nickandartie.com" });
            var extensions = feed.ElementExtensions;
            extensions.Add(new XElement(itunesNS + "subtitle", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), "The Nick &amp; Artie Show - Starring Nick Di Paolo and Artie Lange.").CreateReader());
            //extensions.Add(new XElement(itunesNS + "image", new XAttribute("href", imageUrl)).CreateReader());
            extensions.Add(new XElement(itunesNS + "author", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), "Nick Di Paolo &amp; Artie Lange").CreateReader());
            extensions.Add(new XElement(itunesNS + "summary", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), "The Nick &amp; Artie Show - Starring Nick Di Paolo and Artie Lange.").CreateReader());
            extensions.Add(new XElement(itunesNS + "category", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), new XAttribute("text", "Sports &amp; Recreation"), new XElement(itunesNS + "category", new XAttribute("text", "Sports &amp; Recreation"))).CreateReader());
            extensions.Add(new XElement(itunesNS + "explicit", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), "no").CreateReader());
            extensions.Add(new XDocument(new XElement(itunesNS + "owner", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), new XElement(itunesNS + "name", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), "The Nick &amp; Artie Show"), new XElement(itunesNS + "email", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), "nickandartie@nickandartie.com"))).CreateReader());

            var feedItems = new List<SyndicationItem>();

            var GetItems = db.Podcasts.OrderByDescending(x => x.DatePublished).Take(500).ToList();

            foreach (var i in GetItems)
            {
                var item = new SyndicationItem(i.Title, null, new Uri(i.FileName));
                item.Summary = new TextSyndicationContent("The Nick &amp; Artie Show");
                item.Id = i.ID.ToString();
                if (i.DatePublished != null)
                    item.PublishDate = (DateTimeOffset)i.DatePublished;
                item.Links.Add(new SyndicationLink()
                {
                    Title = i.Title,
                    Uri = new Uri("http://www.nickandartie.com/load.mp3?url=" + i.FileName),
                    Length = 0,
                    MediaType = "audio/mpeg"
                });
                var itemExt = item.ElementExtensions;
                itemExt.Add(new XElement(itunesNS + "subtitle", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), "The Nick &amp; Artie Show - Starring Nick Di Paolo and Artie Lange.").CreateReader());
                itemExt.Add(new XElement(itunesNS + "summary", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), "The Nick &amp; Artie Show - Starring Nick Di Paolo and Artie Lange.").CreateReader());
                itemExt.Add(new XElement(itunesNS + "duration", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), "0").CreateReader());
                itemExt.Add(new XElement(itunesNS + "keywords", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), "Sports").CreateReader());
                itemExt.Add(new XElement(itunesNS + "explicit", new XAttribute(XNamespace.Xmlns + prefix, itunesNS.NamespaceName), "no").CreateReader());
                itemExt.Add(new XElement("enclosure", new XAttribute("url", i.FileName),
                    new XAttribute("length", "0"), new XAttribute("type", "audio/mpeg")));
                feedItems.Add(item);
            }
            
            feed.Items = feedItems;


            return new SyndicationFeedResult(feed);
        }

    }

    public class SyndicationFeedResult : ContentResult
    {
        public SyndicationFeedResult(SyndicationFeed feed)
        : base()
        {
        using (var memstream = new MemoryStream())
        using (var writer = new XmlTextWriter(memstream, System.Text.UTF8Encoding.UTF8))
        {
        feed.SaveAsRss20(writer);
        writer.Flush();
        memstream.Position = 0;
        Content = new StreamReader(memstream).ReadToEnd();
        ContentType = "application/rss+xml" ;
        }
        }
    }
}
