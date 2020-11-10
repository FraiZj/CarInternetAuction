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
                    AuctionDate = new DateTime(2020, 11, 08),
                    SellerId = "1",
                    SaleType = SaleTypeDto.BrandNew,
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
                    AuctionDate = new DateTime(2020, 11, 10),
                    SellerId = "2",
                    SaleType = SaleTypeDto.BrandNew,
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
                    AuctionDate = new DateTime(2020, 11, 12),
                    SellerId = "3",
                    SaleType = SaleTypeDto.BrandNew,
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
        public async Task LotService_AddAsync_AddsLot()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.LotRepository.Add(It.IsAny<Lot>()));
            var lotService = new LotService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var lot = new LotModel
            {
                AuctionDate = DateTime.Now,
                SellerId = "1",
                SaleType = SaleTypeDto.BrandNew,
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
                    && l.AuctionDate == lot.AuctionDate
                    && l.SaleType == SaleType.BrandNew)),
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
                AuctionDate = DateTime.Now,
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
                    && l.AuctionDate == lot.AuctionDate
                    && l.SaleType == SaleType.BrandNew)),
                Times.Never);
        }
    }
}
