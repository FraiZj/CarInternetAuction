using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Tests.RepositoriesTests
{
    [TestFixture]
    public class LotRepositoryTests
    {
        /// <summary>
        /// Sets and returns mock context
        /// </summary>
        /// <param name="mockDbSet"></param>
        /// <returns></returns>
        private Mock<ApplicationDbContext> GetMockContext(Mock<DbSet<Lot>> mockDbSet)
        {
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext
                .Setup(m => m.Set<Lot>())
                .Returns(mockDbSet.Object);
            mockContext
                .Setup(m => m.Set<Lot>().Include(It.IsAny<string>()))
                .Returns(mockDbSet.Object);

            return mockContext;
        }

        [Test]
        public void LotRepository_Add_AddsLot()
        {
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Lot>(UnitTestsHelper.GetTestLots());
            var mockContext = GetMockContext(mockDbSet);
            var lotRepo = new LotRepository(mockContext.Object);
            var lot = new Lot
            {
                Id = 100,
                AuctionDate = new DateTime(2020, 11, 15),
                CarId = 1
            };

            lotRepo.Add(lot);

            mockDbSet.Verify(
                m => m.Add(It.Is<Lot>(
                    l => l.Id == lot.Id
                        && l.AuctionDate == lot.AuctionDate
                        && l.CarId == lot.CarId)),
                Times.Once);
        }
        
        [Test]
        public void LotRepository_Delete_DeletesLot()
        {
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Lot>(UnitTestsHelper.GetTestLots());
            var mockContext = GetMockContext(mockDbSet);
            var lotRepo = new LotRepository(mockContext.Object);
            var lot = new Lot
            {
                Id = 1,
                AuctionDate = new DateTime(2020, 11, 15),
                CarId = 1
            };

            lotRepo.Delete(lot);

            mockDbSet.Verify(
                m => m.Remove(It.Is<Lot>(
                    l => l.Id == lot.Id 
                        && l.AuctionDate == lot.AuctionDate 
                        && l.CarId == lot.CarId)),
                Times.Once);
        }

        [Test]
        public async Task LotRepository_DeleteByIdAsync_DeletesLot()
        {
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Lot>(UnitTestsHelper.GetTestLots());
            var mockContext = GetMockContext(mockDbSet);
            var lotRepo = new LotRepository(mockContext.Object);
            var id = 1;

            await lotRepo.DeleteByIdAsync(id);

            mockDbSet.Verify(
                m => m.Remove(It.Is<Lot>(
                    l => l.Id == id)),
                Times.Once);
        }

        [Test]
        public void LotRepository_FindAll_ReturnsAllLots()
        {
            var lots = UnitTestsHelper.GetTestLots().ToList();
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Lot>(lots.AsQueryable());
            var mockContext = GetMockContext(mockDbSet);
            var lotRepo = new LotRepository(mockContext.Object);

            var result = lotRepo.FindAll().ToList();

            Assert.AreEqual(lots.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(lots[i].Id, result[i].Id);
                Assert.AreEqual(lots[i].AuctionDate, result[i].AuctionDate);
                Assert.AreEqual(lots[i].Car?.Id, result[i].Car?.Id);
            }
        }

        [Test]
        public void LotRepository_FindAllWithDetails_ReturnsAllLotsWithDetails()
        {
            var lots = UnitTestsHelper.GetTestLots().ToList();
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Lot>(lots.AsQueryable());
            var mockContext = GetMockContext(mockDbSet);
            var lotRepo = new LotRepository(mockContext.Object);

            var result = lotRepo.FindAllWithDetails().ToList();

            Assert.AreEqual(lots.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(lots[i].Id, result[i].Id);
                Assert.AreEqual(lots[i].AuctionDate, result[i].AuctionDate);
                Assert.AreEqual(lots[i].Car?.Id, result[i].Car?.Id);
                Assert.AreEqual(lots[i].Seller?.Id, result[i].Seller?.Id);
            }
        }

        [Test]
        public async Task LotRepository_GetByIdAsync_ReturnsProperLot()
        {
            var lot = UnitTestsHelper.GetTestLots().First();
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Lot>(UnitTestsHelper.GetTestLots().AsQueryable());
            var mockContext = GetMockContext(mockDbSet);
            var lotRepo = new LotRepository(mockContext.Object);

            var result = await lotRepo.GetByIdAsync(lot.Id);

            Assert.AreEqual(lot.Id, result.Id);
            Assert.AreEqual(lot.AuctionDate, result.AuctionDate);
            Assert.AreEqual(lot.Car?.Id, result.Car?.Id);
        }

        [Test]
        public async Task LotRepository_GetByIdWithDetailsAsync_ReturnsProperLotWithDetails()
        {
            var lot = UnitTestsHelper.GetTestLots().First();
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Lot>(UnitTestsHelper.GetTestLots().AsQueryable());
            var mockContext = GetMockContext(mockDbSet);
            var lotRepo = new LotRepository(mockContext.Object);

            var result = await lotRepo.GetByIdWithDetailsAsync(lot.Id);

            Assert.AreEqual(lot.Id, result.Id);
            Assert.AreEqual(lot.AuctionDate, result.AuctionDate);
            Assert.AreEqual(lot.Car?.Id, result.Car?.Id);
            Assert.AreEqual(lot.Seller?.Id, result.Seller?.Id);
        }

        [Test]
        public void LotRepository_Update_UpdatesLot()
        {
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Lot>(UnitTestsHelper.GetTestLots());
            var mockContext = GetMockContext(mockDbSet);
            var lotRepo = new LotRepository(mockContext.Object);
            var lot = new Lot
            {
                Id = 1,
                AuctionDate = new DateTime(2020, 11, 15),
                CarId = 1
            };

            lotRepo.Update(lot);

            mockDbSet.Verify(
                m => m.Attach(It.Is<Lot>(
                    l => l.Id == lot.Id
                        && l.AuctionDate == lot.AuctionDate
                        && l.CarId == lot.CarId)),
                Times.Once);
        }
    }
}
