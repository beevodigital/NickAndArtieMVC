using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NickAndArtie.Models
{
    public class PhotoReel
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string ImageThumb { get; set; }
        public string ImageLarge { get; set; }
    }
}