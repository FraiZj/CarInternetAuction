using InternetAuction.BLL.Infrastructure;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.Web.Infrastructure;
using InternetAuction.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
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
            _lotService = lotService;
        }

        private LotViewModel CreateLotViewModel(IEnumerable<LotModel> lots, int page)
        {
            var pageSize = 6;

            if (Math.Ceiling((double)lots.Count() / pageSize) < page || page < 1) 
                page = 1;

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
        public ActionResult ActiveLots(int page = 1)
        {
            ViewBag.Title = "Active Lots";
            var lotViewModel = CreateLotViewModel(_lotService.GetAll().Where(l => l.IsActive), page);
            return View("Lots", lotViewModel);
        }

        [Authorize(Roles = Roles.Admin)]
        public ActionResult AllLots(int page = 1)
        {
            ViewBag.Title = "All Lots";
            var lotViewModel = CreateLotViewModel(_lotService.GetAll(), page);
            return View("Lots", lotViewModel);
        }

        [Authorize(Roles = Roles.Admin)]
        public ActionResult ArchiveLots(int page = 1)
        {
            ViewBag.Title = "Archive Lots";
            var lotViewModel = CreateLotViewModel(_lotService.GetAll().Where(l => !l.IsActive), page);
            return View("Lots", lotViewModel);
        }

        [Authorize]
        public ActionResult SoldLots(string userId, int page = 1)
        {
            if (!User.IsInRole("Admin")
                && User.Identity.GetUserId() != userId)
                return RedirectToAction("Forbidden", "Errors");

            ViewBag.Title = "Sold Lots";
            ViewBag.UserId = userId;
            var lotViewModel = CreateLotViewModel(_lotService.GetAll().Where(l => l.SellerId == userId), page);
            return View("Lots", lotViewModel);
        }

        [Authorize]
        public ActionResult PurchasedLots(string userId, int page = 1)
        {
            if (!User.IsInRole("Admin")
                && User.Identity.GetUserId() != userId)
                return RedirectToAction("Forbidden", "Errors");

            ViewBag.Title = "Purchased Lots";
            ViewBag.UserId = userId;

            var lotViewModel = CreateLotViewModel(_lotService.GetAll().Where(l => l.BuyerId == userId), page);
            return View("Lots", lotViewModel);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            var lot = await _lotService.GetByIdAsync(id);

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
            var lot = await _lotService.GetByIdAsync(id);

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
            var lot = await _lotService.GetByIdAsync(id);

            if (lot is null)
                return RedirectToAction("NotFound", "Errors");

            if (!User.IsInRole("Admin")
                && User.Identity.GetUserId() != lot.SellerId)
                return RedirectToAction("Forbidden", "Errors");

            await _lotService.DeleteByIdAsync(id);
            return RedirectToAction("ActiveLots", "Lots");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search(SearchModel model, int page = 1)
        {
            if (model is null)
                ModelState.AddModelError("", "Invalid search parameters");

            if (ModelState.IsValid)
            {
                ViewBag.Query = "Search";
                ViewBag.SearchModel = model;

                var lotViewModel = CreateLotViewModel(_lotService.SearchLotModels(model), page);
                return View("Lots", lotViewModel);
            }

            return PartialView("SearchPartial");
        }

        [HttpPost]
        public async Task<ActionResult> Sell(int lotId, string userId)
        {
            await _lotService.SellLot(lotId, userId);

            return RedirectToAction("Details", "Lots", new { id = lotId });
        }
    }
}