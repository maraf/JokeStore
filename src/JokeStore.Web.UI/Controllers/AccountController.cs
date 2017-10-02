using JokeStore.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace JokeStore.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel account, string returnUrl)
        {
            if (ModelState.IsValid && account.Username == "admin" && account.Password == "51admin51")
            {
                ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimsIdentity.DefaultIssuer, "JokeStore"),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, "admin"),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin")
                }, "DefaultAuthenticationScheme"));
                await HttpContext.SignInAsync("DefaultAuthenticationScheme", principal);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                //TempData["Message"] = HtmlMessage.Create("Incorrect username or password!", HtmlMessageType.Error);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("list", "entry");
        }
    }
}
