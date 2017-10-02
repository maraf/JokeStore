using JokeStore.Core.Entity;
using JokeStore.Core.Repository;
using JokeStore.Web.Core;
using JokeStore.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace JokeStore.Web.Controllers
{
    public class EntryController : Controller
    {
        private IRepository repository;

        public int PageSize { get; set; }

        public EntryController(IRepository entRepo)
        {
            repository = entRepo;
            PageSize = 5;
        }

        public ActionResult List(string category = null, int page = 1)
        {
            string categoryName = null;
            if (category != null)
            {
                Entry tmp = repository.Entries.FirstOrDefault(e => e.CategoryUrl == category);
                if (tmp != null)
                    categoryName = tmp.Category;
            }

            IQueryable<Entry> result = repository.Entries;

            if (categoryName != null)
                result = result.Where(e => category == null || e.CategoryUrl == category);

            result = result
                .OrderByDescending(e => e.Created)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            if (category != null)
                result = result.Where(e => e.CategoryUrl == category);

            int totalItems = category == null
                ? repository.Entries.Count()
                : repository.Entries.Where(e => e.CategoryUrl == category).Count();

            return View(new EntryListViewModel
            {
                Category = categoryName,
                CategoryUrl = category,
                Entries = result,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = totalItems
                }
            });
        }

        public ActionResult Create()
        {
            return View(new Entry());
        }

        [HttpPost]
        public ActionResult Create(Entry entry, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                //Domain
                Domain domain = repository.Current;
                entry.Domain = domain;

                //Created
                entry.Created = DateTime.Now;

                //Approved
                entry.Approved = domain.AutoApprove;

                //Image
                if (image != null)
                {
                    entry.ImageContentType = image.ContentType;
                    entry.ImageData = new byte[image.Length];
                    using (Stream content = image.OpenReadStream())
                    {
                        content.Read(entry.ImageData, 0, (int)image.Length);
                    }
                }

                //CategoryUrl => try to find by category and pick url or escape from category
                Entry item = repository.Entries.FirstOrDefault(e => e.Category == entry.Category);
                if (item != null)
                    entry.CategoryUrl = item.CategoryUrl;
                else
                    entry.CategoryUrl = StringUtil.ToUrl(entry.Category);

                //Save
                repository.Save(entry);
                //TempData["Message"] = HtmlMessage.Create(
                //    domain.AutoApprove
                //        ? "Thanks for entry!"
                //        : "Thanks for entry! It will be visible after approval by admin."
                //);
                return RedirectToAction(nameof(List));
            }
            else
            {
                //TempData["Message"] = HtmlMessage.Create("There are some invalid fields!", HtmlMessageType.Error);
                return View(entry);
            }
        }

        public ActionResult Vote(int entryID, string direction, string returnUrl)
        {
            bool up = direction.ToLowerInvariant() == "up";

            Entry entry = repository.Entries.FirstOrDefault(e => e.ID == entryID);

            Vote vote = new Vote
            {
                UserIdentifier = HttpContext.Connection.RemoteIpAddress.ToString(),
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
            if (entry == null)
                entry = repository.NotApprovedEntries.FirstOrDefault(e => e.ID == entryID);

            if (entry != null)
                return File(entry.ImageData, entry.ImageContentType);
            else
                return null;
        }
    }
}
