using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JokeStore.Core.Entity;

namespace JokeStore.Web.Models
{
    public class EntryListViewModel
    {
        public string Category { get; set; }
        public string CategoryUrl { get; set; }
        public IEnumerable<Entry> Entries { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}