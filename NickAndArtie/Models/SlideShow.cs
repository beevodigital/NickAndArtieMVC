using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NickAndArtie.Models
{
    public class SlideShow
    {
        public int ID { get; set; }
        public string ImageLarge { get; set; }
        public string ImageDestination { get; set; }
        public bool IsListenLive { get; set; }
        public int Order { get; set; }
    }
}