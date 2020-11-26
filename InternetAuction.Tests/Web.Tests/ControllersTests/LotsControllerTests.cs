using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using Moq;
using NUnit.Framework;
using InternetAuction.Web.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using InternetAuction.BLL.EnumsDtos;
using InternetAuction.Web.ViewModels;
using System.Threading.Tasks;

namespace InternetAuction.Tests.Web.Tests.ControllersTests
{
    [TestFixture]
    public class LotsControllerTests
    {
        private IQueryable<LotModel> GetTestLotsModels()
        {
            return new List<LotModel>
            {
                new LotModel
                {
                    Id = 1,
                    SellerId = "1",
                    SaleType = SaleTypeDto.BrandNew,
                    IsActive = false,
                    TurnkeyPrice = 5000,
                    Car = new CarModel
                    {
                        Brand = "CarBrand1",
                        Model = "CarModel1",
                        Year = 2014,
                        Mileage = 100000,
                    },
                    Bets = new List<BetModel>
                    { 
                        new BetModel { Id = 1, UserId = "2" }
                    }
                },
                new LotModel
                {
                    SellerId = "2",
                    SaleType = SaleTypeDto.BrandNew,
                    IsActive = true,
                    StartPrice = 2000,
                    TurnkeyPrice = 8000,
                    Car = new CarModel
                    {
                        Brand = "CarBrand2",
                        Model = "CarModel2",
                        Year = 2014,
                        Mileage = 100000,
                    }
                },
                new LotModel
                {
                    SellerId = "3",
                    SaleType = SaleTypeDto.BrandNew,
                    IsActive = true,
                    StartPrice = 1000,
                    TurnkeyPrice = 3000,
                    Car = new CarModel
                    {
                        Brand = "CarBrand3",
                        Model = "CarModel3",
                        Year = 2019,
                        Mileage = 100000,
                    }
                },
            }.AsQueryable();
        }

        [Test]
        public void LotsController_ActiveLots_ReturnsActiveLots()
        {
            var mockLotService = new Mock<ILotService>();
            mockLotService
                .Setup(m => m.SearchLotModels(It.IsAny<SearchModel>()))
                .Returns(GetTestLotsModels());
            var lotsController = new LotsController(mockLotService.Object);

            var result = (ViewResult)(lotsController.ActiveLots(null, null));

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(LotViewModel), result.Model);
            Assert.AreEqual(GetTestLotsModels().Where(l => l.IsActive).Count(), ((LotViewModel)result.Model).Lots.Count());
        }

        [Test]
        public void LotsController_ActiveLots_WithFiltersReturnsProperLots()
        {
            var mockLotService = new Mock<ILotService>();
            mockLotService
                .Setup(m => m.SearchLotModels(It.IsAny<SearchModel>()))
                .Returns(GetTestLotsModels());
            var lotsController = new LotsController(mockLotService.Object);
            var searchModel = new SearchModel
            {
                MinPrice = 1500,
                MaxPrice = 3000
            };

            var result = (ViewResult)(lotsController.ActiveLots(searchModel, null));

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(LotViewModel), result.Model);
            Assert.AreEqual(GetTestLotsModels().Where(l => l.StartPrice > 500 
                                        && l.StartPrice < 3000
                                        && l.IsActive).Count(), 
                            ((LotViewModel)result.Model).Lots.Count());
        }

        [Test]
        public void LotsController_AllLots_ReturnsAllLots()
        {
            var mockLotService = new Mock<ILotService>();
            mockLotService
                .Setup(m => m.SearchLotModels(It.IsAny<SearchModel>()))
                .Returns(GetTestLotsModels());
            var lotsController = new LotsController(mockLotService.Object);

            var result = (ViewResult)(lotsController.AllLots(null, null));

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(LotViewModel), result.Model);
            Assert.AreEqual(GetTestLotsModels().Count(), ((LotViewModel)result.Model).Lots.Count());
        }

        [Test]
        public void LotsController_ArchieveLots_ReturnsArchieveLots()
        {
            var mockLotService = new Mock<ILotService>();
            mockLotService
                .Setup(m => m.SearchLotModels(It.IsAny<SearchModel>()))
                .Returns(GetTestLotsModels());
            var lotsController = new LotsController(mockLotService.Object);

            var result = (ViewResult)(lotsController.ArchiveLots(null, null));

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(LotViewModel), result.Model);
            Assert.AreEqual(GetTestLotsModels().Where(l => !l.IsActive).Count(), ((LotViewModel)result.Model).Lots.Count());
        }

        [Test]
        public async Task LotsController_Details_ReturnsViewProperLot()
        {
            var lot = GetTestLotsModels().First();
            var mockLotService = new Mock<ILotService>();
            mockLotService
                .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(lot);
            var lotsController = new LotsController(mockLotService.Object);

            var result = (ViewResult)(await lotsController.Details(lot.Id));

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(LotModel), result.Model);
            var model = (LotModel)result.Model;
            Assert.AreEqual(lot.Id, model.Id);
            Assert.AreEqual(lot.StartPrice, model.StartPrice);
            Assert.AreEqual(lot.SellerId, model.SellerId);
        }

        [Test]
        public async Task LotsController_Details_WithNotExistingLotReturnsNotFound()
        {
            var mockLotService = new Mock<ILotService>();
            mockLotService
                .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((LotModel)null);
            var lotsController = new LotsController(mockLotService.Object);

            var result = await lotsController.Details(0);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), result);
            Assert.AreEqual("Errors", ((RedirectToRouteResult)result).RouteValues["controller"]);
            Assert.AreEqual("NotFound", ((RedirectToRouteResult)result).RouteValues["action"]);
        }

        [Test]
        public void LotsController_Create_ReturnsProperView()
        {
            var mockLotService = new Mock<ILotService>();
            var lotsController = new LotsController(mockLotService.Object);

            var result = lotsController.Create();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ViewResult), result);
            Assert.IsInstanceOf(typeof(LotModel), ((ViewResult)result).Model);
        }

        [Test]
        public async Task LotsController_Sell_SellsLot() 
        {
            var lot = GetTestLotsModels().First();
            var mockLotService = new Mock<ILotService>();
            mockLotService
                .Setup(m => m.SellLot(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new InternetAuction.BLL.Infrastructure.OperationDetails(true));
            var lotsController = new LotsController(mockLotService.Object);

            var result = await lotsController.Sell(lot.Id, lot.Bets.First().Id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), result);
            var res = ((RedirectToRouteResult)result);
            Assert.AreEqual("Lots", res.RouteValues["controller"]);
            Assert.AreEqual("Details", res.RouteValues["action"]);
            Assert.AreEqual("Lot sold successfully", lotsController.TempData["Success"]);
        }
    }
}
