using InternetAuction.DAL.Entities;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace InternetAuction.DAL.Tests
{
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
                    TransmissionId = 1,
                    DriveUnitId = 1,
                    EngineType = "2.8",
                    BodyTypeId = 1,
                    CarImages = new List<CarImage> { new CarImage { Id = 1, CarId = 1 } }
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

        public static IQueryable<DriveUnit> GetTestDriveUnits()
            => new List<DriveUnit> { new DriveUnit { Id = 1, Name = "TestDriveUnit" } }.AsQueryable();

        public static IQueryable<BodyType> GetTestBodyTypes()
            => new List<BodyType> { new BodyType { Id = 1, Name = "TestBodyType", Cars = new List<Car> { } } }.AsQueryable();

        public static IQueryable<Transmission> GetTestTransmissions()
            => new List<Transmission> { new Transmission { Id = 1, Name = "TestTransmission" } }.AsQueryable();

        public static IQueryable<CarImage> GetTestCarImages()
            => new List<CarImage> { new CarImage { Id = 1, CarId = 1 } }.AsQueryable();

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
