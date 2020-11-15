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
    [Authorize]
    public class BetController : Controller
    {
        private readonly IBetService _betService;

        public BetController(IBetService betService)
        {
            _betService = betService;
        }

        [HttpPost]
        public async Task<ActionResult> PlaceBet(BetViewModel model)
        { 
            if (model is null)
                ModelState.AddModelError("model", "Bet cannot be null");

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
                    return RedirectToAction("Details", "Lots", new { id = model.LotId });
                }

                foreach (var error in result.ValidationResults)
                {
                    ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
                }
            }

            return PartialView();
        }

        //[HttpGet]
        //public ActionResult Bets(LotModel model)
        //{
        //    if (!User.IsInRole("Admin")
        //        && User.Identity.GetUserId() != model.SellerId)
        //        return RedirectToAction("Forbidden", "Errors");


        //}
    }
}