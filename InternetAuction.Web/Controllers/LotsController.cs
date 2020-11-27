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
    /// <summary>
    /// Represents lots controller class
    /// </summary>
    [Authorize]
    public class LotsController : Controller
    {
        private readonly ILotService _lotService;

        /// <summary>
        /// Initializes an instance of the lots controller with lot service
        /// </summary>
        /// <param name="lotService"></param>
        public LotsController(ILotService lotService)
        {
            _lotService = lotService;
        }

        /// <summary>
        /// Returns lots view with active lots
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [OutputCache(Duration = 300, VaryByParam = "searchModel;orderBy;page")]
        public ActionResult ActiveLots(SearchModel searchModel, string orderBy, int page = 1)
        {
            ViewBag.Title = "Active Lots";
            var lots = _lotService.SearchLotModels(searchModel).Where(l => l.IsActive);
            var sortedLots = GetSortedLots(lots, orderBy);
            var lotViewModel = CreateLotViewModel(sortedLots, page);
            lotViewModel.SearchModel = searchModel;
            return View("Lots", lotViewModel);
        }

        /// <summary>
        /// Returns lots view with all lots
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize(Roles = Roles.Admin)]
        [OutputCache(Duration = 300, VaryByParam = "searchModel;orderBy;page")]
        public ActionResult AllLots(SearchModel searchModel, string orderBy, int page = 1)
        {
            ViewBag.Title = "All Lots";
            var lots = _lotService.SearchLotModels(searchModel);
            var sortedLots = GetSortedLots(lots, orderBy);
            var lotViewModel = CreateLotViewModel(sortedLots, page);
            lotViewModel.SearchModel = searchModel;
            return View("Lots", lotViewModel);
        }

        /// <summary>
        /// Returns lots view with archive lots
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize(Roles = Roles.Admin)]
        [OutputCache(Duration = 300, VaryByParam = "searchModel;orderBy;page")]
        public ActionResult ArchiveLots(SearchModel searchModel, string orderBy, int page = 1)
        {
            ViewBag.Title = "Archive Lots";
            var lots = _lotService.SearchLotModels(searchModel).Where(l => !l.IsActive);
            var sortedLots = GetSortedLots(lots, orderBy);
            var lotViewModel = CreateLotViewModel(sortedLots, page);
            lotViewModel.SearchModel = searchModel;
            return View("Lots", lotViewModel);
        }

        /// <summary>
        /// Returns lots view with user`s sale lots
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchModel"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [OutputCache(Duration = 300, VaryByParam = "searchModel;userId;orderBy;page")]
        public ActionResult SoldLots(string userId, SearchModel searchModel, string orderBy, int page = 1)
        {
            userId = string.IsNullOrWhiteSpace(userId) ?
                User.Identity.GetUserId()
                : userId;

            if (!User.IsInRole("Admin")
                && User.Identity.GetUserId() != userId)
                return RedirectToAction("Forbidden", "Errors");

            ViewBag.Title = "Sold Lots";
            ViewBag.UserId = userId;
            var lots = _lotService.SearchLotModels(searchModel).Where(l => l.SellerId == userId);
            var sortedLots = GetSortedLots(lots, orderBy);
            var lotViewModel = CreateLotViewModel(sortedLots, page);
            lotViewModel.SearchModel = searchModel;
            return View("Lots", lotViewModel);
        }

        /// <summary>
        /// Returns lots view with user`s purchased lots
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="userId"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [OutputCache(Duration = 300, VaryByParam = "searchModel;userId;orderBy;page")]
        public ActionResult PurchasedLots(SearchModel searchModel, string userId, string orderBy, int page = 1)
        {
            userId = string.IsNullOrWhiteSpace(userId) ?
                User.Identity.GetUserId()
                : userId;

            if (!User.IsInRole("Admin")
                && User.Identity.GetUserId() != userId)
                return RedirectToAction("Forbidden", "Errors");

            ViewBag.Title = "Purchased Lots";
            ViewBag.UserId = userId;

            var lots = _lotService.SearchLotModels(searchModel).Where(l => l.BuyerId == userId);
            var sortedLots = GetSortedLots(lots, orderBy);
            var lotViewModel = CreateLotViewModel(sortedLots, page);
            lotViewModel.SearchModel = searchModel;
            return View("Lots", lotViewModel);
        }

        /// <summary>
        /// Returns view with lot details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [OutputCache(Duration = 300, VaryByParam = "id")]
        public async Task<ActionResult> Details(int id)
        {
            var lot = await _lotService.GetByIdAsync(id);

            if (lot is null)
                return RedirectToAction("NotFound", "Errors");

            return View(lot);
        }

        /// <summary>
        /// Returns lot creation view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            var lotModel = new LotModel();
            return View(lotModel);
        }

        /// <summary>
        /// Creates lot
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns lot editor view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Udpates lot info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(LotModel model)
        {
            if (!User.IsInRole("Admin")
                && User.Identity.GetUserId() != model.SellerId)
                return RedirectToAction("Forbidden", "Errors");

            if (ModelState.IsValid)
            {
                var result = await _lotService.UpdateAsync(model);

                if (result.Succedeed)
                {
                    return RedirectToAction("Details", new { id = ((LotModel)result.ReturnValue).Id });
                }

                foreach (var error in result.ValidationResults)
                {
                    ModelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
                }
            }

            return View(model);
        }

        /// <summary>
        /// Deletes lot by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Sells lot
        /// </summary>
        /// <param name="lotId"></param>
        /// <param name="betId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Sell(int lotId, int betId)
        {
            var result = await _lotService.SellLot(lotId, betId);

            if (!result.Succedeed)
            {
                TempData["Error"] = "Lot not sold";
                return View("NotFound", "Errors");
            }

            TempData["Success"] = "Lot sold successfully";
            return RedirectToAction("Details", "Lots", new { id = lotId });
        }

        /// <summary>
        /// Buys lot
        /// </summary>
        /// <param name="lotId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Buy(int lotId)
        {
            var result = await _lotService.BuyLot(lotId, User.Identity.GetUserId());

            if (!result.Succedeed)
            {
                TempData["Error"] = "Lot not purchased";
                return View("NotFound", "Errors");
            }

            TempData["Success"] = "Lot purchased successfully";
            return RedirectToAction("Details", "Lots", new { id = lotId });
        }

        /// <summary>
        /// Creates lot view model
        /// </summary>
        /// <param name="lots"></param>
        /// <param name="page"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Sorts lots
        /// </summary>
        /// <param name="lots"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        private IEnumerable<LotModel> GetSortedLots(IEnumerable<LotModel> lots, string orderBy)
        {
            switch (orderBy)
            {
                case "BrandDesc": return lots.OrderByDescending(l => l.Car.Brand);
                case "StartPrice": return lots.OrderBy(l => l.StartPrice);
                case "StartPriceDesc": return lots.OrderByDescending(l => l.StartPrice);
                default: return lots.OrderBy(l => l.Car.Brand);
            }
        }
    }
}