using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JokeStore.Web.Models
{
    public class CategoryListViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public CategoryListViewModel() { }

        public CategoryListViewModel(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}