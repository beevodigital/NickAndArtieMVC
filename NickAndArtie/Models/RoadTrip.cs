using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NickAndArtie.Models
{
    public class RoadTrip
    {
        [Key]
        public int ID { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string Location { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
    }
}