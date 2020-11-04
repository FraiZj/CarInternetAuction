using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Repositories;
using Moq;
using NUnit.Framework;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Tests.RepositoriesTests
{
    [TestFixture]
    public class CarRepositoryTests
    {
        private Mock<ApplicationDbContext> GetMockContext(Mock<DbSet<Car>> carDbSet, Mock<DbSet<TechnicalPassport>> techPassportDbSet)
        {
            var mockContext =  new Mock<ApplicationDbContext>();
            mockContext
                .Setup(m => m.CarImages)
                .Returns(UnitTestsHelper.GetMockDbSet<CarImage>(UnitTestsHelper.GetTestCarImages()).Object);
            mockContext
                .Setup(m => m.Set<Car>())
                .Returns(carDbSet.Object);
            mockContext
                .Setup(m => m.TechnicalPassports)
                .Returns(techPassportDbSet.Object);
            

            return mockContext;
        }

        [Test]
        public void CarRepository_Add_AddsCar()
        {
            var mockCarDbSet = UnitTestsHelper.GetMockDbSet<Car>(UnitTestsHelper.GetTestCars());
            var mockTechnicalPassportDbSet = UnitTestsHelper.GetMockDbSet<TechnicalPassport>(UnitTestsHelper.GetTestTechnicalPassports());
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
            var mockCarDbSet = UnitTestsHelper.GetMockDbSet<Car>(UnitTestsHelper.GetTestCars());
            var mockTechnicalPassportDbSet = UnitTestsHelper.GetMockDbSet<TechnicalPassport>(UnitTestsHelper.GetTestTechnicalPassports());
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
            var mockCarDbSet = UnitTestsHelper.GetMockDbSet<Car>(UnitTestsHelper.GetTestCars());
            var mockTechnicalPassportDbSet = UnitTestsHelper.GetMockDbSet<TechnicalPassport>(UnitTestsHelper.GetTestTechnicalPassports());
            var mockContext = GetMockContext(mockCarDbSet, mockTechnicalPassportDbSet);
            var carRepo = new CarRepository(mockContext.Object);
            var car = UnitTestsHelper.GetTestCars().First();

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
        public async Task CarRepository_DeleteByIdAsync()
        {
            var mockCarDbSet = UnitTestsHelper.GetMockDbSet<Car>(UnitTestsHelper.GetTestCars());
            var mockTechnicalPassportDbSet = UnitTestsHelper.GetMockDbSet<TechnicalPassport>(UnitTestsHelper.GetTestTechnicalPassports());
            var mockContext = GetMockContext(mockCarDbSet, mockTechnicalPassportDbSet);
            var carRepo = new CarRepository(mockContext.Object);
            var car = UnitTestsHelper.GetTestCars().First();

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
        public void CarRepository_Update_UpdatesCar()
        {
            var mockCarDbSet = UnitTestsHelper.GetMockDbSet<Car>(UnitTestsHelper.GetTestCars());
            var mockTechnicalPassportDbSet = UnitTestsHelper.GetMockDbSet<TechnicalPassport>(UnitTestsHelper.GetTestTechnicalPassports());
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
            var mockCarDbSet = UnitTestsHelper.GetMockDbSet<Car>(UnitTestsHelper.GetTestCars());
            var mockTechnicalPassportDbSet = UnitTestsHelper.GetMockDbSet<TechnicalPassport>(UnitTestsHelper.GetTestTechnicalPassports());
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
