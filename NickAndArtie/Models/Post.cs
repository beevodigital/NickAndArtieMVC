using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NickAndArtie.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string HeadingShort { get; set; }
        public DateTime AirDate { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
        public string ImageLarge { get; set; }
        public string ImageThumb { get; set; }
        public string HeadingLong { get; set; }
        public string HeadingSub { get; set; }
        public string Slug { get; set; }
        public bool IsActive { get; set; }
    }

}