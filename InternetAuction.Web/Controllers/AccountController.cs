﻿using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.Web.ViewModels;
using Microsoft.Owin.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InternetAuction.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserModel { Email = model.Email, Password = model.Password };
                var claim = await _userService.Login(user);

                if (claim is null)
                {
                    ModelState.AddModelError("Password", "Incorrect password");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true,
                    }, claim);

                    return RedirectToAction("Create", "Lots");
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("ActiveLots", "Lots");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userModel = new UserModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.UserName,
                    Password = model.Password
                };

                var operationDetails = await _userService.Register(userModel);
                if (operationDetails.Succedeed)
                {
                    return View("Login");
                }
                else
                {
                    foreach (var error in operationDetails.ValidationResults)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault(), error.ErrorMessage);
                    }
                }
            }

            return View(model);
        }
    }
}