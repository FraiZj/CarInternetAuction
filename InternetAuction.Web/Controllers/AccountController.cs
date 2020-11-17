using InternetAuction.BLL.Infrastructure;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.Web.ViewModels;
using Microsoft.AspNet.Identity;
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

        public JsonResult IsEmailAvailable(string email)
        {
            return Json(!_userService.GetAll().Any(u => u.Email == email), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> UserProfile(string id)
        {
            id = id ?? User.Identity.GetUserId();
            UserModel user = await _userService.GetUserModelByIdAsync(id);

            if (user is null)
                return RedirectToAction("NotFound", "Errors");

            return View(user);
        }

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

        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Delete(string id)
        {
            await _userService.Delete(id);
            return RedirectToAction("Users");
        }

        [Authorize(Roles = Roles.Admin)]
        public ActionResult Users()
        {
            var users = _userService.GetAll();
            return View(users);
        }

        protected override void Dispose(bool disposing)
        {
            _userService.Dispose();
            base.Dispose(disposing);
        }
    }
}