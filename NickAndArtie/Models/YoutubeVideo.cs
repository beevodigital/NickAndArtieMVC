using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NickAndArtie.Models
{
    public class YoutubeVideo
    {
        public int ID { get; set; }
        public string Embed { get; set; }
        public bool IsFeatured { get; set; }
        public int SortOrder { get; set; }
    }
}