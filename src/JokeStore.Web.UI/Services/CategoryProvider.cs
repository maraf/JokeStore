using JokeStore.Core.Repository;
using JokeStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokeStore.Web.Services
{
    public class CategoryProvider
    {
        private IEntryRepository repository;

        public CategoryProvider(IEntryRepository entRepo)
        {
            repository = entRepo;
        }

        public IEnumerable<CategoryListViewModel> GetList()
        {
            var result = repository.Entries
                .Select(e => new { e.Category, e.CategoryUrl })
                .Distinct()
                .OrderBy(e => e.Category);

            List<CategoryListViewModel> categories = new List<CategoryListViewModel>();
            foreach (var item in result)
                categories.Add(new CategoryListViewModel(item.Category, item.CategoryUrl));

            return categories;
        }
    }
}
