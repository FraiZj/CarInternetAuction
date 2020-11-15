using InternetAuction.BLL.Infrastructure;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.Web.Infrastructure;
using InternetAuction.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InternetAuction.Web.Controllers
{
    [Authorize]
    public class LotsController : Controller
    {
        private readonly ILotService _lotService;

        public LotsController(ILotService lotService)
        {
            this._lotService = lotService;
        }

        private LotViewModel CreateLotViewModel(IEnumerable<LotModel> lots, int page)
        {
            var pageSize = 4;
            var lotsPerPage = lots.Skip((page - 1) * pageSize).Take(pageSize);
            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = lots.Count()
            };

            return new LotViewModel
            {
                Lots = lotsPerPage,
                PageInfo = pageInfo
            };
        }

        [AllowAnonymous]
        public ActionResult ActiveLots()
        {
            var lots = _lotService.GetAllActiveLots();
            return View("Lots", lots);
        }

        [Authorize(Roles = Roles.Admin)]
        public ActionResult AllLots()
        {
            var lots = _lotService.GetAll();
            return View("Lots", lots);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            var lot = await _lotService.GetByIdWithDetailsAsync(id);

            if (lot is null)
                return RedirectToAction("NotFound", "Errors");

            return View(lot);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var lotModel = new LotModel();
            return View(lotModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(LotModel model)
        {
            if (ModelState.IsValid)
            {
                model.SellerId = User.Identity.GetUserId();
                var result = await _lotService.AddAsync(model);

                if (result.Succedeed)
                {
                    return RedirectToAction("Details", new { id = (int)result.ReturnValue });
                }

                foreach (var error in result.ValidationResults)
                {
                    ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var lot = await _lotService.GetByIdWithDetailsAsync(id);

            if (lot is null)
                return RedirectToAction("NotFound", "Errors");

            if (!User.IsInRole("Admin")
                && User.Identity.GetUserId() != lot.SellerId)
                return RedirectToAction("Forbidden", "Errors");

            return View(lot);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(LotModel model)
        {
            if (!User.IsInRole("Admin")
                && User.Identity.GetUserId() != model.SellerId)
                return RedirectToAction("Forbidden", "Errors");

            // TODO: add images editing
            if (ModelState.IsValid)
            {
                var result = await _lotService.UpdateAsync(model);

                if (result.Succedeed)
                {
                    return RedirectToAction("Details", new { id = (int)result.ReturnValue });
                }

                foreach (var error in result.ValidationResults)
                {
                    ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var lot = await _lotService.GetByIdWithDetailsAsync(id);

            if (lot is null)
                return RedirectToAction("NotFound", "Errors");

            if (!User.IsInRole("Admin")
                && User.Identity.GetUserId() != lot.SellerId)
                return RedirectToAction("Forbidden", "Errors");

            await _lotService.DeleteByIdAsync(id);
            return RedirectToAction("ActiveLots", "Lots");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Search(SearchModel model)
        {
            if (model is null)
                ModelState.AddModelError("", "Invalid search parameters");

            if (ModelState.IsValid)
            {
                var lots = _lotService.SearchLotModels(model);
                return View("Lots", lots);
            }

            return PartialView("SearchPartial");
        }

        [HttpPost]
        public async Task<ActionResult> Buy(int lotId, string userId)
        {

            await _lotService.BuyLot(lotId, userId);

            return RedirectToAction("Details", "Lots", new { id = lotId });
        }
    }
}