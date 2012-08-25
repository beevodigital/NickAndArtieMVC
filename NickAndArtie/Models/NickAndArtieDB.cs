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
    }
}