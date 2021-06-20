﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetIT.DatabaseLayer.Dto;
using GetIT.Models;
using GetIT.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;

namespace GetIT.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _UserManager;
        SignInManager<ApplicationUser> _SignInManager;
        RoleManager<IdentityRole> _RoleManager;
        private readonly IEmailService _EmailService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _UserManager = userManager;
            _SignInManager = signinManager;
            _RoleManager = roleManager;
            _EmailService = emailService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register( )
        {           
            return View();
        }

        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<JsonResult> IsEmailInUse(string email)
        {
            var user = await _UserManager.FindByEmailAsync(email);
            if(user == null)
            {
                return Json(true);
            }else
            {
                return Json($"Email {email} is already in use");
            }            
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserVM userVM)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser identityUser = new ApplicationUser
                {
                    UserName = userVM.UserName,
                    Email = userVM.Email,      
                    City = userVM.City,
                    EmailConfirmed = false
                };
                var result = await _UserManager.CreateAsync(identityUser, userVM.Password);

                if(result.Succeeded)
                {
                    // generation of email token
                    var emailCode = await _UserManager.GenerateEmailConfirmationTokenAsync(identityUser);

                    var url = Url.Action(nameof(VerifyEmail), "Account", new { userId = identityUser.Id, code = emailCode }, Request.Scheme,Request.Host.ToString());

                    await _EmailService.SendAsync("hari@gmail.com", "Verify Email",$"Click the following link to verify <a href=\"{url}\">Verify</a>",true);

                    ModelState.AddModelError("", "Registered. Please verify your email by click on the link sent to you email Inbox");

                    //await _SignInManager.SignInAsync(identityUser, isPersistent: false);                    
                    //return RedirectToAction("index","Home");
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
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string userid, string code)
        {
            var user = await _UserManager.FindByIdAsync(userid);

            var result = await _UserManager.ConfirmEmailAsync(user, code);

            if(result.Succeeded)
            {                
                return RedirectToRoute("/Home/Index");
            }

            return RedirectToRoute("/Home/Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole(CreateRole createRoleVM)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = createRoleVM.RoleName
                };

                var identityResult = await _RoleManager.CreateAsync(identityRole);
                
                if(identityResult.Succeeded)
                {
                    return RedirectToAction("AllRoles","Account");
                }
                else
                {
                    foreach(var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return View(createRoleVM);
                }               
            }else
            {
                return View(createRoleVM);
            }          
        }

        [HttpGet]
        public ActionResult AllRoles()
        {
            var allRoles = _RoleManager.Roles;
            return View(allRoles);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM loginVM,string returnUrl)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser identityUser1 = await _UserManager.FindByNameAsync(loginVM.UserName);
                if(identityUser1 != null)
                {
                    var result = await _SignInManager.PasswordSignInAsync(identityUser1, loginVM.Password, loginVM.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return LocalRedirect(returnUrl);
                        else
                            return RedirectToAction("index", "home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username or password");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or password");
                }
            }
            return View(loginVM);
        }

        public async Task<ActionResult> EditRole(string roleId)
        {
            var roleObj = await _RoleManager.FindByIdAsync(roleId);

            if(roleObj == null)
            {
                RedirectToAction("AllRoles");
            }

            EditRoleVM editRoleVM = new EditRoleVM
            {
                RoleID = roleObj.Id,
                RoleName = roleObj.Name                
            };

            var users = await _UserManager.GetUsersInRoleAsync(roleObj.Name);
            editRoleVM.Users = users.Select(x => x.UserName).ToList();

            return View(editRoleVM);
        }

        [HttpPost]
        public async Task<ActionResult> EditRole(EditRoleVM editRoleVM)
        {
            var roleObj = await _RoleManager.FindByIdAsync(editRoleVM.RoleID);

            roleObj.Name = editRoleVM.RoleName;

            var result = await _RoleManager.UpdateAsync(roleObj);

            if(result.Succeeded)
            {
                return RedirectToAction("AllRoles");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View(editRoleVM);
            }            
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