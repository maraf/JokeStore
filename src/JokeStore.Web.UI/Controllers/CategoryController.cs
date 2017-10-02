using JokeStore.Core.Entity;
using JokeStore.Core.Repository;
using JokeStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JokeStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private IEntryRepository repository;

        public CategoryController(IEntryRepository entRepo)
        {
            repository = entRepo;
        }

        public ActionResult Menu(string category)
        {
            ViewBag.SelectedCategory = category;

            var result = repository.Entries
                .Select(e => new { e.Category, e.CategoryUrl })
                .Distinct()
                .OrderBy(e => e.Category);

            List<CategoryListViewModel> categories = new List<CategoryListViewModel>();
            foreach (var item in result)
                categories.Add(new CategoryListViewModel(item.Category, item.CategoryUrl));

            return PartialView(categories);
        }
    }
}
