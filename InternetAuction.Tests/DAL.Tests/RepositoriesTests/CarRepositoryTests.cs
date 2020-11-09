using InternetAuction.DAL;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Enums;
using InternetAuction.DAL.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.Tests.DAL.Tests.RepositoriesTests
{
    [TestFixture]
    public class CarRepositoryTests
    {
        private IQueryable<TechnicalPassport> GetTestTechnicalPassports()
        {
            return new List<TechnicalPassport>
            {
                new TechnicalPassport { CarId = 1, PrimaryDamage = "BMW" },
                new TechnicalPassport { CarId = 2, PrimaryDamage = "Audi" },
                new TechnicalPassport { CarId = 3, PrimaryDamage = "Mercedes" },
            }.AsQueryable();
        }

        private IQueryable<Car> GetTestCars()
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
                    CarImages = new List<CarImage> { new CarImage { Id = 1, CarId = 1 } },
                    TechnicalPassport = new TechnicalPassport { 
                        CarId = 1, 
                        PrimaryDamage = "BMW",
                        BodyType = BodyType.Sedan,
                        DriveUnit = DriveUnit.FourWheelDrive,
                        Transmission = Transmission.AT,
                    }
                },
                new Car { Id = 2, Brand = "Audi", Model = "RS6" },
                new Car { Id = 3, Brand = "Mercedes-Benz", Model = "E63" },

            }.AsQueryable();
        }

        /// <summary>
        /// Sets and returns mock context
        /// </summary>
        /// <param name="carDbSet"></param>
        /// <param name="techPassportDbSet"></param>
        /// <returns></returns>
        private Mock<ApplicationDbContext> GetMockContext(Mock<DbSet<Car>> carDbSet, Mock<DbSet<TechnicalPassport>> techPassportDbSet)
        {
            var mockContext =  new Mock<ApplicationDbContext>();
            mockContext
                .Setup(m => m.CarImages)
                .Returns(UnitTestHelper.GetMockDbSet<CarImage>(new List<CarImage> { new CarImage { Id = 1, CarId = 1 } }.AsQueryable()).Object);
            mockContext
                .Setup(m => m.Set<Car>())
                .Returns(carDbSet.Object);
            mockContext
                .Setup(m => m.TechnicalPassports)
                .Returns(techPassportDbSet.Object);
            mockContext
                .Setup(m => m.Set<Car>().Include(It.IsAny<string>()))
                .Returns(carDbSet.Object);

            return mockContext;
        }

        [Test]
        public void CarRepository_Add_AddsCar()
        {
            var mockCarDbSet = UnitTestHelper.GetMockDbSet<Car>(GetTestCars());
            var mockTechnicalPassportDbSet = UnitTestHelper.GetMockDbSet<TechnicalPassport>(GetTestTechnicalPassports());
            var mockContext = GetMockContext(mockCarDbSet, mockTechnicalPassportDbSet);
            var carRepo = new CarRepository(mockContext.Object);
            var car = new Car 
            { 
                Brand = "Ferrari", 
                Model = "LaFerrari" 
            };

            carRepo.Add(car);

            mockCarDbSet.Verify(
                m => m.Add(It.Is<Car>(
                    c => c.Brand == car.Brand
                         && c.Model == car.Model)), 
                Times.Once);
        }

        [Test]
        public void CarRepository_AddTechnicalPassport_AddsTechnicalPassport()
        {
            var mockCarDbSet = UnitTestHelper.GetMockDbSet<Car>(GetTestCars());
            var mockTechnicalPassportDbSet = UnitTestHelper.GetMockDbSet<TechnicalPassport>(GetTestTechnicalPassports());
            var mockContext = GetMockContext(mockCarDbSet, mockTechnicalPassportDbSet);
            var carRepo = new CarRepository(mockContext.Object);
            var technicalPassport = new TechnicalPassport 
            { 
                CarId = 100,
                PrimaryDamage = "Damage"
            };

            carRepo.AddTechnicalPassport(technicalPassport);

            mockTechnicalPassportDbSet.Verify(
                m => m.Add(It.Is<TechnicalPassport>(
                    c => c.CarId == technicalPassport.CarId
                         && c.PrimaryDamage == technicalPassport.PrimaryDamage)), 
                Times.Once);
        }

        [Test]
        public void CarRepository_Delete_DeletesCar()
        {
            var mockCarDbSet = UnitTestHelper.GetMockDbSet<Car>(GetTestCars());
            var mockTechnicalPassportDbSet = UnitTestHelper.GetMockDbSet<TechnicalPassport>(GetTestTechnicalPassports());
            var mockContext = GetMockContext(mockCarDbSet, mockTechnicalPassportDbSet);
            var carRepo = new CarRepository(mockContext.Object);
            var car = GetTestCars().First();

            carRepo.Delete(car);

            mockCarDbSet.Verify(
                m => m.Remove(It.Is<Car>(
                    c => c.Brand == car.Brand
                         && c.Model == car.Model
                         && c.Id == car.Id)),
                Times.Once);

            mockTechnicalPassportDbSet.Verify(
                m => m.Remove(It.Is<TechnicalPassport>(
                    p => p.CarId == car.Id)),
                Times.Once);
        }

        [Test]
        public async Task CarRepository_DeleteByIdAsync_DeletesCar()
        {
            var mockCarDbSet = UnitTestHelper.GetMockDbSet<Car>(GetTestCars());
            var mockTechnicalPassportDbSet = UnitTestHelper.GetMockDbSet<TechnicalPassport>(GetTestTechnicalPassports());
            var mockContext = GetMockContext(mockCarDbSet, mockTechnicalPassportDbSet);
            var carRepo = new CarRepository(mockContext.Object);
            var car = GetTestCars().First();

            await carRepo.DeleteByIdAsync(car.Id);

            mockCarDbSet.Verify(
                m => m.Remove(It.Is<Car>(
                    c => c.Brand == car.Brand
                         && c.Model == car.Model
                         && c.Id == car.Id)),
                Times.Once);

            mockTechnicalPassportDbSet.Verify(
                m => m.Remove(It.Is<TechnicalPassport>(
                    p => p.CarId == car.Id)),
                Times.Once);
        }

        [Test]
        public void CarRepository_FindAll_ReturnsAllCars()
        {
            var testCars = GetTestCars().ToList();
            var mockCarDbSet = UnitTestHelper.GetMockDbSet<Car>(testCars.AsQueryable());
            var mockTechnicalPassportDbSet = UnitTestHelper.GetMockDbSet<TechnicalPassport>(GetTestTechnicalPassports());
            var mockContext = GetMockContext(mockCarDbSet, mockTechnicalPassportDbSet);
            var carRepo = new CarRepository(mockContext.Object);

            var cars = carRepo.FindAll().ToList();

            Assert.AreEqual(testCars.Count, cars.Count);
            for (int i = 0; i < cars.Count; i++)
            {
                Assert.AreEqual(testCars[i].Id, cars[i].Id);
                Assert.AreEqual(testCars[i].Brand, cars[i].Brand);
                Assert.AreEqual(testCars[i].EngineType, cars[i].EngineType);
                Assert.AreEqual(testCars[i].CarImages?.First().Id, cars[i].CarImages?.First().Id);
            }
        }

        [Test]
        public void CarRepository_FindAllWithTechnicalPassport_ReturnsAllCarsWithTechnicalPassport()
        {
            var testCars = GetTestCars().ToList();
            var mockCarDbSet = UnitTestHelper.GetMockDbSet<Car>(testCars.AsQueryable());
            var mockTechnicalPassportDbSet = UnitTestHelper.GetMockDbSet<TechnicalPassport>(GetTestTechnicalPassports());
            var mockContext = GetMockContext(mockCarDbSet, mockTechnicalPassportDbSet);
            var carRepo = new CarRepository(mockContext.Object);

            var cars = carRepo.FindAllWithDetails().ToList();

            Assert.AreEqual(testCars.Count, cars.Count);
            for (int i = 0; i < cars.Count; i++)
            {
                Assert.AreEqual(testCars[i].Id, cars[i].Id);
                Assert.AreEqual(testCars[i].Brand, cars[i].Brand);
                Assert.AreEqual(testCars[i].EngineType, cars[i].EngineType);
                Assert.AreEqual(testCars[i].CarImages?.First().Id, cars[i].CarImages?.First().Id);
                Assert.AreEqual(testCars[i].TechnicalPassport?.CarId, cars[i].TechnicalPassport?.CarId);
            }
        }

        [Test]
        public async Task CarRepository_GetByIdAsync_ReturnsProperCar()
        {
            var car = GetTestCars().First();
            var mockCarDbSet = UnitTestHelper.GetMockDbSet<Car>(GetTestCars().AsQueryable());
            var mockTechnicalPassportDbSet = UnitTestHelper.GetMockDbSet<TechnicalPassport>(GetTestTechnicalPassports());
            var mockContext = GetMockContext(mockCarDbSet, mockTechnicalPassportDbSet);
            var carRepo = new CarRepository(mockContext.Object);

            var result = await carRepo.GetByIdAsync(1);

            Assert.AreEqual(car.Id, car.Id);
            Assert.AreEqual(car.Brand, result.Brand);
            Assert.AreEqual(car.EngineType, result.EngineType);
            Assert.AreEqual(car.CarImages?.First().Id, result.CarImages?.First().Id);
        }

        [Test]
        public async Task CarRepository_GetByIdWithTechnicalPassportAsync_ReturnsProperCarWithTechnicalPassport()
        {
            var car = GetTestCars().First();
            var mockCarDbSet = UnitTestHelper.GetMockDbSet<Car>(GetTestCars().AsQueryable());
            var mockTechnicalPassportDbSet = UnitTestHelper.GetMockDbSet<TechnicalPassport>(GetTestTechnicalPassports());
            var mockContext = GetMockContext(mockCarDbSet, mockTechnicalPassportDbSet);
            var carRepo = new CarRepository(mockContext.Object);

            var result = await carRepo.GetByIdAsync(1);

            Assert.AreEqual(car.Id, car.Id);
            Assert.AreEqual(car.Brand, result.Brand);
            Assert.AreEqual(car.EngineType, result.EngineType);
            Assert.AreEqual(car.CarImages?.First().Id, result.CarImages?.First().Id);
            Assert.AreEqual(car.TechnicalPassport?.CarId, result.TechnicalPassport?.CarId);
        }

        [Test]
        public void CarRepository_Update_UpdatesCar()
        {
            var mockCarDbSet = UnitTestHelper.GetMockDbSet<Car>(GetTestCars());
            var mockTechnicalPassportDbSet = UnitTestHelper.GetMockDbSet<TechnicalPassport>(GetTestTechnicalPassports());
            var mockContext = GetMockContext(mockCarDbSet, mockTechnicalPassportDbSet);
            var carRepo = new CarRepository(mockContext.Object);
            var car = new Car
            {
                Id = 1,
                Brand = "Ferarri",
                Model = "LaFerrari"
            };

            carRepo.Update(car);

            mockCarDbSet.Verify(
                m => m.Attach(It.Is<Car>(
                    c => c.Brand == car.Brand
                         && c.Model == car.Model
                         && c.Id == car.Id)),
                Times.Once);
        }

        [Test]
        public void CarRepository_UpdateTechnicalPassport_UpdatesTechnicalPassport()
        {
            var mockCarDbSet = UnitTestHelper.GetMockDbSet<Car>(GetTestCars());
            var mockTechnicalPassportDbSet = UnitTestHelper.GetMockDbSet<TechnicalPassport>(GetTestTechnicalPassports());
            var mockContext = GetMockContext(mockCarDbSet, mockTechnicalPassportDbSet);
            var carRepo = new CarRepository(mockContext.Object);
            var techPassport = new TechnicalPassport
            {
                CarId = 1,
                PrimaryDamage = "Damage"
            };

            carRepo.UpdateTechnicalPassport(techPassport);

            mockTechnicalPassportDbSet.Verify(
                m => m.Attach(It.Is<TechnicalPassport>(
                    p => p.CarId == techPassport.CarId
                         && p.PrimaryDamage == techPassport.PrimaryDamage)),
                Times.Once);
        }
    }
}
