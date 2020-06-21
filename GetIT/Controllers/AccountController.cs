using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetIT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GetIT.Controllers
{
    public class AccountController : Controller
    {
        UserManager<IdentityUser> _UserManager;
        SignInManager<IdentityUser> _SignInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager)
        {
            _UserManager = userManager;
            _SignInManager = signinManager;
        }

        [HttpGet]
        public IActionResult Register( )
        {           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserVM userVM)
        {
            if(ModelState.IsValid)
            {
                IdentityUser identityUser = new IdentityUser
                {
                    UserName = userVM.UserName,
                    Email = userVM.Email,
                };
                var result = await _UserManager.CreateAsync(identityUser, userVM.Password);

                if(result.Succeeded)
                {
                    await _SignInManager.SignInAsync(identityUser, isPersistent: false);                    
                   return RedirectToAction("index","Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }                   
                }

            }
            return View(userVM);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM,string returnUrl)
        {
            if(ModelState.IsValid)
            {
                IdentityUser identityUser1 = await _UserManager.FindByNameAsync(loginVM.UserName);
                var result = await _SignInManager.PasswordSignInAsync(identityUser1, loginVM.Password, loginVM.RememberMe, false);
                if(result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("index", "home");
                }else
                {
                    ModelState.AddModelError("", "Invalid Username or password");
                }
            }
            return View(loginVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Profile()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}