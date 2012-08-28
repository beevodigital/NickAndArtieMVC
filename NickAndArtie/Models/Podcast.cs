using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace NickAndArtie.Models
{
    public class Podcast
    {
        [Key]
        public int ID { get; set; }
        public string FileName { get; set; }
        public string Image { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        [Required(AllowEmptyStrings=true)]
        public DateTime? DatePublished { get; set; }
    }
}