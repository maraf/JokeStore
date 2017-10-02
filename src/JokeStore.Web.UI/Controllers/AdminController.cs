using JokeStore.Core.Entity;
using JokeStore.Core.Repository;
using JokeStore.Web.Core;
using JokeStore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        #region ENTRIES

        public ActionResult NotApprovedEntry()
        {
            return View(repository.NotApprovedEntries);
        }

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

        public ActionResult EditEntry(int entryID)
        {
            Entry entry = repository.Entries.FirstOrDefault(e => e.ID == entryID);
            if(entry == null)
                entry = repository.NotApprovedEntries.FirstOrDefault(e => e.ID == entryID);

            if (entry != null)
            {
                return View(entry);
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult EditEntry(Entry entry)
        {
            entry.Domain = repository.Domains.FirstOrDefault(e => e.ID == entry.Domain.ID);

            if (ModelState.IsValid)
            {
                repository.Save(entry);
                return RedirectToAction("index");
            }
            return View(entry);
        }

        [HttpPost]
        public ActionResult DeleteEntry(int entryID)
        {
            Entry entry = repository.NotApprovedEntries.FirstOrDefault(e => e.ID == entryID);
            if (entry != null)
                repository.Delete(entry);

            return View("index");
        }

        #endregion

        #region DOMAINS

        public ActionResult ListDomain()
        {
            return View(repository.Domains);
        }

        public ActionResult EditDomain(int domainID)
        {
            Domain domain = repository.Domains.FirstOrDefault(d => d.ID == domainID);
            if(domain != null)
                return View(domain);

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult EditDomain(Domain domain)
        {
            if (ModelState.IsValid)
            {
                repository.Save(domain);
                return RedirectToAction("index");
            }
            return View(domain);
        }

        public ActionResult CreateDomain()
        {
            return View("EditDomain", new Domain());
        }

        [HttpPost]
        public ActionResult DeleteDomain(int domainID)
        {
            Domain domain = repository.Domains.FirstOrDefault(d => d.ID == domainID);
            if (domain != null && repository.Entries.Count(e => e.Domain.ID == domainID) == 0)
            {
                repository.Delete(domain);
                TempData["Message"] = HtmlMessage.Create(String.Format("Domain with url '{0}' deleted.", domain.Url));
                return RedirectToAction("index");
            }
            else
            {
                TempData["Message"] = HtmlMessage.Create("No such domain or domain contains entries!", HtmlMessageType.Error);
                return RedirectToAction("index");
            }
        }

        #endregion
    }
}
