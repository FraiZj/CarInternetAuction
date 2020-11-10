using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InternetAuction.Web.Controllers
{
    [Authorize]
    public class LotsController : Controller
    {
        private readonly ILotService lotService;

        public LotsController(ILotService lotService)
        {
            this.lotService = lotService;
        }

        public ActionResult ActiveLots()
        {
            var lots = lotService.GetAllActiveLots();
            return View(lots);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AllLots()
        {
            var lots = lotService.GetAll();
            return View(lots);
        }

        public async Task<ActionResult> Details(int id)
        {
            var lot = await lotService.GetByIdWithDetailsAsync(id);

            //if (lot is null)
            //    return RedirectToAction("NotFound", "Errors");

            return View(lot);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var lotModel = new LotModel();
            return View(lotModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(LotModel model)
        {
            if (ModelState.IsValid)
            {
                var lot = new LotModel
                {
                    SaleType = model.SaleType,
                    SellerId = User.Identity.GetUserId(),
                    AuctionDate = DateTime.UtcNow.AddDays(3),
                    Car = new CarModel
                    {
                        Brand = model.Car.Brand,
                        Model = model.Car.Model,
                        Year = model.Car.Year,
                        Mileage = model.Car.Mileage,
                        EngineType = model.Car.EngineType,
                        TechnicalPassport = new TechnicalPassportModel
                        {
                            VIN = model.Car.TechnicalPassport.VIN,
                            Transmission = model.Car.TechnicalPassport.Transmission,
                            DriveUnit = model.Car.TechnicalPassport.DriveUnit,
                            BodyType = model.Car.TechnicalPassport.BodyType,
                            HasKeys = model.Car.TechnicalPassport.HasKeys
                        }
                    }
                };

                await lotService.AddAsync(lot);



                return RedirectToAction("Details", new { id = lot.Id });
            }

            return View(model);
        }

        //[Authorize]
        //public async Task<ActionResult> BuyLot(int lotId)
        //{

        //    lotService.UpdateAsync();
        //}
    }
}