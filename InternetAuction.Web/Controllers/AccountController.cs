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

                    if (string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectToAction("ActiveLots", "Lots");
                    else
                        return Redirect(returnUrl);
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

        [Authorize]
        public async Task<ActionResult> UserProfile(string id)
        {
            id = id ?? User.Identity.GetUserId();
            UserModel user = await _userService.GetUserModelByIdAsync(id);

            if (user is null)
                return RedirectToAction("NotFound", "Errors");

            return View(user);
        }
    }
}