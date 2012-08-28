using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NickAndArtie.Models
{
    public class NickAndArtieDB : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<PhotoReel> PhotoReels { get; set; }
        public DbSet<RoadTrip> RoadTrips { get; set; }
    }
}