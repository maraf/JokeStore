using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JokeStore.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace JokeStore.Core.Repository.EntityFramework
{
    public class DataContext : DbContext
    {
        public DbSet<Domain> Domains { get; set; }

        public DbSet<Entry> Entries { get; set; }

        public DbSet<Vote> Votes { get; set; }
    }
}
