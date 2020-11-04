using InternetAuction.DAL.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using InternetAuction.DAL.Enums;

namespace InternetAuction.DAL.Tests
{
    /// <summary>
    /// Represents class with helper methods for unit testing
    /// </summary>
    internal static class UnitTestsHelper
    {
        public static IQueryable<Car> GetTestCars()
        {
            return new List<Car>
            {
                new Car {
                    Id = 1,
                    Brand = "BMW",
                    Model = "M5",
                    Year = 1,
                    Mileage = 1,
                    EngineType = "2.8",
                    BodyType = BodyType.Sedan,
                    DriveUnit = DriveUnit.FourWheelDrive,
                    Transmission = Transmission.AT,
                    CarImages = new List<CarImage> { new CarImage { Id = 1, CarId = 1 } },
                    TechnicalPassport = new TechnicalPassport { CarId = 1, PrimaryDamage = "BMW" }
                },
                new Car { Id = 2, Brand = "Audi", Model = "RS6" },
                new Car { Id = 3, Brand = "Mercedes-Benz", Model = "E63" },

            }.AsQueryable();
        }

        public static IQueryable<TechnicalPassport> GetTestTechnicalPassports()
        {
            return new List<TechnicalPassport>
            {
                new TechnicalPassport { CarId = 1, PrimaryDamage = "BMW" },
                new TechnicalPassport { CarId = 2, PrimaryDamage = "Audi" },
                new TechnicalPassport { CarId = 3, PrimaryDamage = "Mercedes" },
            }.AsQueryable();
        }

        public static IQueryable<CarImage> GetTestCarImages()
            => new List<CarImage> { new CarImage { Id = 1, CarId = 1 } }.AsQueryable();

        public static IQueryable<Lot> GetTestLots()
        {
            return new List<Lot>
            {
                new Lot 
                { 
                    Id = 1, 
                    AuctionDate = new DateTime(2020, 11, 10), 
                    CarId = 1, 
                    Car = new Car { Id = 1, Brand = "Audi", Model = "RS6" }, 
                    SellerId = 1,
                    Seller = new ApplicationUser { Id = "1" } 
                },
                new Lot { Id = 2, AuctionDate = new DateTime(2020, 11, 10), CarId = 2 },
                new Lot { Id = 3, AuctionDate = new DateTime(2020, 11, 10), CarId = 3 }
            }.AsQueryable();
        }

        /// <summary>
        /// Sets and returns mock DbSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider)
                       .Returns(new TestDbAsyncQueryProvider<T>(data.Provider));
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression)
                       .Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType)
                       .Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator())
                       .Returns(data.GetEnumerator());
            mockSet.As<IDbAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator())
                       .Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));

            return mockSet;
        }
    }
}
