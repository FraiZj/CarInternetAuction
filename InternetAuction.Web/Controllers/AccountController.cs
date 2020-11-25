using InternetAuction.BLL.Infrastructure;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.Web.Infrastructure;
using InternetAuction.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InternetAuction.Web.Controllers
{
    /// <summary>
    /// Represents the account controller class
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        /// <summary>
        /// Initializes an instance of the user controller class with user service
        /// </summary>
        /// <param name="userService"></param>
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Returns login view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Authenticates the user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
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

                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("ActiveLots", "Lots");
                }
            }

            return View(model);
        }

        /// <summary>
        /// Logs out user account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("ActiveLots", "Lots");
        }

        /// <summary>
        /// Returns register view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register user account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                    Password = model.Password
                };

                var operationDetails = await _userService.Register(userModel);

                if (operationDetails.Succedeed)
                {
                    var claim = await _userService.Login(userModel);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true,
                    }, claim);

                    return RedirectToAction("ActiveLots", "Lots");
                }

                foreach (var error in operationDetails.ValidationResults)
                {
                    ModelState.AddModelError(error.MemberNames.FirstOrDefault(), error.ErrorMessage);
                }
            }

            return View(model);
        }

        /// <summary>
        /// Checks if email is in use
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public JsonResult IsEmailAvailable(string email)
        {
            return Json(!_userService.GetAll().Any(u => u.Email == email), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns user profile view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> UserProfile(string id)
        {
            id = string.IsNullOrWhiteSpace(id) ? 
                User.Identity.GetUserId() 
                : id;
            UserModel user = await _userService.GetUserModelByIdAsync(id);

            if (user is null)
                return RedirectToAction("NotFound", "Errors");

            return View(user);
        }

        /// <summary>
        /// Returns edit user profile view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (!User.IsInRole("Admin")
                && User.Identity.GetUserId() != id)
                return RedirectToAction("Forbidden", "Errors");

            var user = await _userService.GetUserModelByIdAsync(id);

            if (user is null)
                return RedirectToAction("NotFound", "Errors");

            var editViewModel = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password
            };

            return View(editViewModel);
        }

        /// <summary>
        /// Saves user profile changes
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (!User.IsInRole("Admin")
                && User.Identity.GetUserId() != model.Id)
                return RedirectToAction("Forbidden", "Errors");

            if (ModelState.IsValid)
            {
                var userModel = new UserModel
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Password = model.Password
                };

                var result = await _userService.Update(userModel);

                if (result.Succedeed)
                {
                    return RedirectToAction("UserProfile", new { id = model.Id });
                }

                foreach (var error in result.ValidationResults)
                {
                    ModelState.AddModelError(error.ErrorMessage, error.MemberNames.First());
                }
            }

            return View(model);
        }

        /// <summary>
        /// Returns users view
        /// </summary>
        /// <param name="model"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Users(UserSearchModel model, string orderBy, int page = 1)
        {
            var users = _userService.SearchUsers(model);
            var sortedUsers = GetSortedUsers(users, orderBy);
            var userViewModel = CreateUserViewModel(sortedUsers, page);
            return View(userViewModel);
        }

        /// <summary>
        /// Sorts users
        /// </summary>
        /// <param name="users"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        private IEnumerable<UserModel> GetSortedUsers(IEnumerable<UserModel> users, string orderBy)
        {
            switch (orderBy)
            {
                case "EmailDesc": return users.OrderByDescending(u => u.Email);
                case "LastName": return users.OrderBy(u => u.LastName);
                case "LastNameDesc": return users.OrderByDescending(u => u.LastName);
                default: return users.OrderBy(u => u.Email);
            }
        }

        /// <summary>
        /// Creates user view model
        /// </summary>
        /// <param name="users"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private UserViewModel CreateUserViewModel(IEnumerable<UserModel> users, int page)
        {
            var pageSize = 3;

            if (Math.Ceiling((double)users.Count() / pageSize) < page || page < 1)
                page = 1;

            var lotsPerPage = users.Skip((page - 1) * pageSize).Take(pageSize);
            
            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = users.Count()
            };

            return new UserViewModel
            {
                Users = lotsPerPage,
                PageInfo = pageInfo
            };
        }

        protected override void Dispose(bool disposing)
        {
            _userService.Dispose();
            base.Dispose(disposing);
        }
    }
}