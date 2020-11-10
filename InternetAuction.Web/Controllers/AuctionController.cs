using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InternetAuction.Web.Controllers
{
    public class LotsController : Controller
    {
        private readonly ILotService lotService;

        public LotsController(ILotService lotService)
        {
            this.lotService = lotService;
        }

        public ActionResult Lots()
        {
            var lots = lotService.GetAllActiveLots();
            return View(lots);
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
                var lot = new LotModel
                {
                    SaleType = model.SaleType,
                    SellerId = "1",
                    AuctionDate = DateTime.UtcNow.AddDays(3),
                    Bets = new List<int>(),
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

                return View("Details", model);
            }

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}