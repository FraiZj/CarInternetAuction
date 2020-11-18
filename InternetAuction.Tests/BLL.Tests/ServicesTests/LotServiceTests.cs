using InternetAuction.BLL.EnumsDtos;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Services;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Enums;
using InternetAuction.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.Tests.BLL.Tests.ServicesTests
{
    [TestFixture]
    public class LotServiceTests
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

        private IQueryable<Lot> GetTestLotsEntities()
        {
            return new List<Lot>
            {
                new Lot
                {
                    TurnkeyPrice = 5000,
                    SellerId = "1",
                    SaleType = SaleType.BrandNew,
                    IsActive = false,
                    Car = new Car
                    {
                        Brand = "CarBrand1",
                        Model = "CarModel1",
                        Year = 2014,
                        Mileage = 100000,
                    }
                },
                new Lot
                {
                    TurnkeyPrice = 8000,
                    SellerId = "2",
                    SaleType = SaleType.BrandNew,
                    IsActive = true,
                    StartPrice = 2000,
                    Car = new Car
                    {
                        Brand = "CarBrand2",
                        Model = "CarModel2",
                        Year = 2014,
                        Mileage = 100000,
                    }
                },
                new Lot
                {
                    TurnkeyPrice = 3000,
                    SellerId = "3",
                    SaleType = SaleType.BrandNew,
                    IsActive = true,
                    StartPrice = 1000,
                    Car = new Car
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
        public async Task LotService_AddAsync_AddsLot()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.LotRepository.Add(It.IsAny<Lot>()));
            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var lot = new LotModel
            {
                TurnkeyPrice = 5000,
                SellerId = "1",
                SaleType = SaleTypeDto.BrandNew,
                StartPrice = 1000,
                Car = new CarModel
                {
                    Brand = "CarBrand",
                    Model = "CarModel",
                    Year = 2014,
                    Mileage = 100000,
                    EngineType = "2.3",
                    TechnicalPassport = new TechnicalPassportModel
                    {
                        VIN = "4Y1SL65848Z411439",
                        HasKeys = true,
                        Transmission = TransmissionDto.MT,
                        DriveUnit = DriveUnitDto.FourWheelDrive,
                        BodyType = BodyTypeDto.PeopleMover
                    }
                }
            };

            var result = await lotService.AddAsync(lot);

            Assert.IsTrue(result.Succedeed);
            mockUnitOfWork.Verify(
                m => m.LotRepository.Add(It.Is<Lot>(
                    l => l.Car.Brand == lot.Car.Brand
                    && l.TurnkeyPrice == lot.TurnkeyPrice
                    && l.SaleType == SaleType.BrandNew)),
                Times.Once);
            mockUnitOfWork.Verify(
               m => m.SaveAsync(),
               Times.Once);
        }

        [Test]
        public async Task LotService_AddAsync_ReturnErrorsWithInvalidModel()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.LotRepository.Add(It.IsAny<Lot>()));
            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var lot = new LotModel
            {
                TurnkeyPrice = 5000,
                SellerId = "1",
                SaleType = SaleTypeDto.BrandNew,
                Car = new CarModel
                {
                    Brand = "",
                    Model = "CarModel",
                    Year = 2022,
                    Mileage = 100000,
                    TechnicalPassport = new TechnicalPassportModel
                    {
                        VIN = "4Y1SL65848Z411439",
                        HasKeys = true,
                        Transmission = TransmissionDto.AT,
                        DriveUnit = DriveUnitDto.FourWheelDrive,
                    }
                }
            };

            var result = await lotService.AddAsync(lot);

            Assert.IsFalse(result.Succedeed);
            mockUnitOfWork.Verify(
                m => m.LotRepository.Add(It.Is<Lot>(
                    l => l.Car.Brand == lot.Car.Brand
                    && l.TurnkeyPrice == lot.TurnkeyPrice
                    && l.SaleType == SaleType.BrandNew)),
                Times.Never);
            mockUnitOfWork.Verify(
               m => m.SaveAsync(),
               Times.Never);
        }

        [Test]
        public async Task LotService_DeleteByIdAsync_DeletesLot()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.LotRepository.DeleteByIdAsync(It.IsAny<int>()));
            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var id = 1;

            var result = await lotService.DeleteByIdAsync(id);

            Assert.IsTrue(result.Succedeed);
            mockUnitOfWork.Verify(
                m => m.LotRepository.DeleteByIdAsync(id), Times.Once);
            mockUnitOfWork.Verify(
               m => m.SaveAsync(),
               Times.Once);
        }

        [Test]
        public void LotService_GetAll_ReturnsAllLots()
        {
            var expected = GetTestLotsModels().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.LotRepository.FindAllWithDetails()).Returns(GetTestLotsEntities());
            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = lotService.GetAll().ToList();

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].SaleType, actual[i].SaleType);
                Assert.AreEqual(expected[i].TurnkeyPrice, actual[i].TurnkeyPrice);
                Assert.AreEqual(expected[i].IsActive, actual[i].IsActive);
            }
        }

        [Test]
        public async Task LotService_GetByIdWithDetails_ReturnsProperLot()
        {
            var expected = GetTestLotsModels().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.LotRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestLotsEntities().First());
            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = await lotService.GetByIdAsync(expected.Id);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.SaleType, actual.SaleType);
            Assert.AreEqual(expected.TurnkeyPrice, actual.TurnkeyPrice);
            Assert.AreEqual(expected.IsActive, actual.IsActive);
        }

        [Test]
        public void LotService_SearchLotModels_ReturnsProperLots()
        {
            var minPrice = 500;
            var maxPrice = 3000;
            var expected = GetTestLotsModels().Where(l => l.StartPrice > minPrice && l.StartPrice < maxPrice).ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.LotRepository.FindAll()).Returns(GetTestLotsEntities());
            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var searchModel = new SearchModel
            {
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            var actual = lotService.SearchLotModels(searchModel).ToList();

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].SaleType, actual[i].SaleType);
                Assert.AreEqual(expected[i].TurnkeyPrice, actual[i].TurnkeyPrice);
                Assert.AreEqual(expected[i].IsActive, actual[i].IsActive);
            }
        }

        [Test]
        public async Task LotService_SellLot_UpdatesLot()
        {
            var bet = new Bet() { Id = 1, UserId = "1", LotId = 1 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.LotRepository.Update(It.IsAny<Lot>()));
            mockUnitOfWork
                .Setup(m => m.BetRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(bet);
            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var result = await lotService.SellLot(1, bet.Id);

            Assert.IsTrue(result.Succedeed);
            mockUnitOfWork.Verify(
                m => m.LotRepository.Update(It.Is<Lot>(
                    l => l.BuyerId == bet.UserId
                    && !l.IsActive)),
                Times.Once);
            mockUnitOfWork.Verify(
               m => m.SaveAsync(),
               Times.Once);
        }

        [Test]
        public async Task LotService_UpdateAsync_UpdatesLot()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.LotRepository.Update(It.IsAny<Lot>()));
            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var lot = new LotModel
            {
                TurnkeyPrice = 5000,
                SellerId = "2",
                SaleType = SaleTypeDto.BrandNew,
                IsActive = true,
                Car = new CarModel
                {
                    Brand = "CarBrand",
                    Model = "CarModel",
                    Year = 2014,
                    Mileage = 100000,
                    EngineType = "2.3",
                    TechnicalPassport = new TechnicalPassportModel
                    {
                        VIN = "4Y1SL65848Z411439",
                        HasKeys = true,
                        Transmission = TransmissionDto.MT,
                        DriveUnit = DriveUnitDto.FourWheelDrive,
                        BodyType = BodyTypeDto.PeopleMover
                    }
                }
            };

            var result = await lotService.UpdateAsync(lot);

            Assert.IsTrue(result.Succedeed);
            mockUnitOfWork.Verify(
                m => m.LotRepository.Update(It.Is<Lot>(
                    l => l.Car.Brand == lot.Car.Brand
                    && l.TurnkeyPrice == lot.TurnkeyPrice
                    && l.SaleType == SaleType.BrandNew)),
                Times.Once);
            mockUnitOfWork.Verify(
               m => m.SaveAsync(),
               Times.Once);
        }
    }
}
