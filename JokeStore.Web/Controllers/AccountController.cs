﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JokeStore.Web.Core.Auth;
using JokeStore.Web.Models;

namespace JokeStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;

        public AccountController(IAuthProvider provider)
        {
            authProvider = provider;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel account, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(account.Username, account.Password))
                    return Redirect(returnUrl ?? Url.Action("index", "admin"));
                else
                    TempData["Message"] = HtmlMessage.Create("Incorrect username or password!", HtmlMessageType.Error);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            authProvider.SignOut();
            return RedirectToAction("list", "entry");
        }
    }
}
