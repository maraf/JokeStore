using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JokeStore.Web.Controllers
{
    public class DomainController : Controller
    {
        public string Heading()
        {
            return "JokeStore";
        }

        public string SubHeading()
        {
            return "Vtípky, vtípky, vtípky ...";
        }
    }
}
