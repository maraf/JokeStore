using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JokeStore.Core.Entity;
using JokeStore.Core.Repository;

namespace JokeStore.Web.Controllers
{
    public class LayoutController : Controller
    {
        private IDomainRepository repository;
        private IDomainResolver resolver;

        public LayoutController(IDomainRepository repo, IDomainResolver reso)
        {
            repository = repo;
            resolver = reso;
        }

        public ActionResult HtmlHead()
        {
            return PartialView(GetModel());
        }

        public ActionResult WebHead()
        {
            return PartialView(GetModel());
        }

        private Domain GetModel()
        {
            Domain domain = repository.Domains.FirstOrDefault(d => d.Url == resolver.DomainName);
            if (domain != null)
                return domain;

            return new Domain
            {
                Heading = "Unknown domain",
                SubHeading = "Sorry, but this not a known domain!"
            };
        }
    }
}
