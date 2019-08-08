using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaret.WebUI.IdentityCore;
using ETicaret.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signinManager;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signinManager)
        {

            userManager = _userManager;
            signinManager = _signinManager;
            

        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();

        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if(await userManager.IsInRoleAsync(user, "admin"))
                    {
                        await signinManager.SignOutAsync();
                        var result = await signinManager.PasswordSignInAsync(user, model.Password, false,false);

                        if (result.Succeeded)
                        {
                            return Redirect(returnUrl ?? "/Admin");
                        }
                    }
                    ModelState.AddModelError("UserName", "Invalid UserName or Password");
                    

                }
            }
            return View(model);
        }

        public RedirectResult AccessDenied(string returnUrl)
        {

            signinManager.SignOutAsync();
            return  Redirect(returnUrl ?? "/Admin");

        }

        public RedirectResult Logout()
        {
            
            signinManager.SignOutAsync();



            return Redirect("/Admin");
        }

    }
}