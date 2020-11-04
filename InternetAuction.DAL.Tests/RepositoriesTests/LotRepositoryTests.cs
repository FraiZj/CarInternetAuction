using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Data.Entity;

namespace InternetAuction.DAL.Tests.RepositoriesTests
{
    [TestFixture]
    public class LotRepositoryTests
    {
        private Mock<ApplicationDbContext> GetMockContext(Mock<DbSet<Lot>> mockDbSet)
        {
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext
                .Setup(m => m.Set<Lot>())
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
                AuctionDate = new DateTime(2020, 11, 15)
            };

            lotRepo.Add(lot);

            mockDbSet.Verify(
                m => m.Add(It.Is<Lot>(
                    l => l.AuctionDate == lot.AuctionDate)),
                Times.Once);
        }
    }
}
