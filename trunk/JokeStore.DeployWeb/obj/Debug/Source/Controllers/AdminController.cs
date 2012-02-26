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
    [Authorize]
    public class AdminController : Controller
    {
        private IRepository repository;

        public AdminController(IRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotApprovedEntry()
        {
            return View(repository.NotApprovedEntries);
        }

        [ActionName("approve-entry")]
        [HttpPost]
        public ActionResult ApproveEntry(int entryID, string returnUrl)
        {
            Entry entry = repository.NotApprovedEntries.FirstOrDefault(e => e.ID == entryID);
            if (entry != null)
            {
                entry.Approved = true;
                repository.Save(entry);
                TempData["Message"] = HtmlMessage.Create(String.Format("Entry '{0}' approved.", StringUtil.Cut(entry.Content)));
                return Redirect(returnUrl ?? Url.Action("index"));
            }

            return View();
        }

        [ActionName("edit-entry")]
        public ActionResult EditEntry(int entryID)
        {
            Entry entry = repository.Entries.FirstOrDefault(e => e.ID == entryID);
            if(entry == null)
                entry = repository.NotApprovedEntries.FirstOrDefault(e => e.ID == entryID);

            if (entry != null)
            {
                return View("EditEntry", entry);
            }

            return new EmptyResult();
        }

        [ActionName("edit-entry")]
        [HttpPost]
        public ActionResult EditEntry(Entry entry)
        {
            entry.Domain = repository.Domains.FirstOrDefault(e => e.ID == entry.Domain.ID);

            if (ModelState.IsValid)
            {
                repository.Save(entry);
                return RedirectToAction("index");
            }
            return View("EditEntry", entry);
        }
    }
}
