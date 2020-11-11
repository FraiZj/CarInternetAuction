using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using Microsoft.AspNet.Identity;
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

        [AllowAnonymous]
        public ActionResult ActiveLots()
        {
            var lots = _lotService.GetAllActiveLots();
            return View("Lots", lots);
        }

        [Authorize(Roles = "Admin")]
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
    }
}