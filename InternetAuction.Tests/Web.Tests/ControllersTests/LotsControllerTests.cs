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
            var betController = new LotsController(mockLotService.Object);

            var result = (ViewResult)(betController.ActiveLots(null, null));

            Assert.IsNotNull(result);
            Assert.AreEqual(GetTestLotsModels().Where(l => l.IsActive).Count(), ((LotViewModel)result.Model).Lots.Count());
        }

        [Test]
        public void LotsController_ActiveLots_WithFiltersReturnsProperLots()
        {
            var mockLotService = new Mock<ILotService>();
            mockLotService
                .Setup(m => m.SearchLotModels(It.IsAny<SearchModel>()))
                .Returns(GetTestLotsModels());
            var betController = new LotsController(mockLotService.Object);
            var searchModel = new SearchModel
            {
                MinPrice = 1500,
                MaxPrice = 3000
            };

            var result = (ViewResult)(betController.ActiveLots(searchModel, null));

            Assert.IsNotNull(result);
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
            var betController = new LotsController(mockLotService.Object);

            var result = (ViewResult)(betController.AllLots(null, null));

            Assert.IsNotNull(result);
            Assert.AreEqual(GetTestLotsModels().Count(), ((LotViewModel)result.Model).Lots.Count());
        }

        [Test]
        public void LotsController_ArchieveLots_ReturnsArchieveLots()
        {
            var mockLotService = new Mock<ILotService>();
            mockLotService
                .Setup(m => m.SearchLotModels(It.IsAny<SearchModel>()))
                .Returns(GetTestLotsModels());
            var betController = new LotsController(mockLotService.Object);

            var result = (ViewResult)(betController.ArchiveLots(null, null));

            Assert.IsNotNull(result);
            Assert.AreEqual(GetTestLotsModels().Where(l => !l.IsActive).Count(), ((LotViewModel)result.Model).Lots.Count());
        }
    }
}
