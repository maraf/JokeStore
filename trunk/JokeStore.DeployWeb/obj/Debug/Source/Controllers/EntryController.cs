using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JokeStore.Core.Entity;
using JokeStore.Core.Repository;
using JokeStore.Web.Core;
using JokeStore.Web.Models;

namespace JokeStore.Web.Controllers
{
    public class EntryController : Controller
    {
        private IEntryRepository repository;

        public int PageSize { get; set; }

        public EntryController(IEntryRepository entRepo)
        {
            repository = entRepo;
            PageSize = 5;
        }

        public ActionResult List(string category = null, int page = 1)
        {
            string categoryName = null;
            Entry tmp = repository.Entries.FirstOrDefault(e => e.CategoryUrl == category);
            if (tmp != null)
                categoryName = tmp.Category;

            IEnumerable<Entry> result = repository.Entries
                .Where(e => category == null || e.CategoryUrl == category)
                .OrderByDescending(e => e.Created)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            if (category != null)
                result = result.Where(e => e.CategoryUrl == category);

            return View(new EntryListViewModel
            {
                Category = categoryName,
                CategoryUrl = category,
                Entries = result,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null 
                        ? repository.Entries.Count()
                        : repository.Entries.Where(e => e.CategoryUrl == category).Count()
                }
            });
        }

        public ActionResult Create()
        {
            return View(new Entry());
        }

        [HttpPost]
        public ActionResult Create(Entry entry, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                //Created
                entry.Created = DateTime.Now;

                //Approved
                entry.Approved = false;

                //Image
                if (image != null)
                {
                    entry.ImageContentType = image.ContentType;
                    entry.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(entry.ImageData, 0, image.ContentLength);
                }

                //CategoryUrl => try to find by category and pick url or escape from category
                Entry item = repository.Entries.FirstOrDefault(e => e.Category == entry.Category);
                if (item != null)
                    entry.CategoryUrl = item.CategoryUrl;
                else
                    entry.CategoryUrl = StringUtil.ToUrl(entry.Category);

                //Save
                repository.Save(entry);
                TempData["Message"] = HtmlMessage.Create("Thanks for entry! It will be visible after approval by admin.");
            }
            return RedirectToAction("list");
        }

        public ActionResult Vote(int entryID, string direction, string returnUrl)
        {
            bool up = direction.ToLowerInvariant() == "up";

            Entry entry = repository.Entries.FirstOrDefault(e => e.ID == entryID);
            Vote vote = new Vote {
                UserIdentifier = Request.UserHostAddress,
                Value = up ? 1 : 0
            };

            if (entry != null && repository.AddVote(entryID, vote))
            {
                if (up)
                    entry.ThumbsUp++;
                else
                    entry.ThumbsDown++;

                entry.Votes++;
                repository.Save(entry);
            }

            if (returnUrl != null)
                return Redirect(returnUrl);
            else
                return new EmptyResult();
        }

        public FileContentResult GetImage(int entryID)
        {
            Entry entry = repository.Entries.FirstOrDefault(e => e.ID == entryID);
            if (entry != null)
                return File(entry.ImageData, entry.ImageContentType);
            else
                return null;
        }
    }
}
