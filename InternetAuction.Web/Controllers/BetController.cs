using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InternetAuction.Web.Controllers
{
    /// <summary>
    /// Represents bet controller class
    /// </summary>
    [Authorize]
    public class BetController : Controller
    {
        private readonly IBetService _betService;

        /// <summary>
        /// Initializes an instance of the bet controller with bet service
        /// </summary>
        /// <param name="betService"></param>
        public BetController(IBetService betService)
        {
            _betService = betService;
        }

        /// <summary>
        /// Adds a bet
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PlaceBet(BetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var betModel = new BetModel
                {
                    LotId = model.LotId,
                    UserId = User.Identity.GetUserId(),
                    Sum = model.Sum,
                    BetDate = DateTime.UtcNow
                };

                var result = await _betService.AddAsync(betModel);

                if (result.Succedeed)
                {
                    TempData["BetAccepted"] = "Your bet has been accepted";
                    return RedirectToAction("Details", "Lots", new { id = model.LotId });
                }

                foreach (var error in result.ValidationResults)
                {
                    ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
                }
            }

            return PartialView("PlaceBetPartial");
        }

        protected override void Dispose(bool disposing)
        {
            _betService.Dispose();
            base.Dispose(disposing);
        }
    }
}