using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using JokeStore.Core.Entity;

namespace JokeStore.Core.Repository.EntityFramework
{
    public class DataContext : DbContext
    {
        public DbSet<Domain> Domains { get; set; }

        public DbSet<Entry> Entries { get; set; }

        public DbSet<Vote> Votes { get; set; }
    }
}
