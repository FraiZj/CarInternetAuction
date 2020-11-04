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
    public class BetRepositoryTests
    {
        /// <summary>
        /// Sets and returns mock context
        /// </summary>
        /// <param name="mockDbSet"></param>
        /// <returns></returns>
        private Mock<ApplicationDbContext> GetMockContext(Mock<DbSet<Bet>> mockDbSet)
        {
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext
                .Setup(m => m.Set<Bet>())
                .Returns(mockDbSet.Object);
            mockContext
                .Setup(m => m.Set<Bet>().Include(It.IsAny<string>()))
                .Returns(mockDbSet.Object);

            return mockContext;
        }

        [Test]
        public void BetRepository_Add_AddsBet()
        {
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Bet>(UnitTestsHelper.GetTestBets());
            var mockContext = GetMockContext(mockDbSet);
            var betRepo = new BetRepository(mockContext.Object);
            var bet = new Bet
            {
                Id = 100,
                BetDate = new DateTime(2020, 11, 15),
                LotId = 1
            };

            betRepo.Add(bet);

            mockDbSet.Verify(
                m => m.Add(It.Is<Bet>(
                    l => l.Id == bet.Id
                        && l.BetDate == bet.BetDate
                        && l.LotId == bet.LotId)),
                Times.Once);
        }

        [Test]
        public void BetRepository_Delete_DeletesBet()
        {
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Bet>(UnitTestsHelper.GetTestBets());
            var mockContext = GetMockContext(mockDbSet);
            var betRepo = new BetRepository(mockContext.Object);
            var bet = new Bet
            {
                Id = 1,
                BetDate = new DateTime(2020, 11, 04),
                LotId = 1
            };

            betRepo.Delete(bet);

            mockDbSet.Verify(
                m => m.Remove(It.Is<Bet>(
                    l => l.Id == bet.Id
                        && l.BetDate == bet.BetDate
                        && l.LotId == bet.LotId)),
                Times.Once);
        }

        [Test]
        public async Task BetRepository_DeleteByIdAsync_DeletesBet()
        {
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Bet>(UnitTestsHelper.GetTestBets());
            var mockContext = GetMockContext(mockDbSet);
            var betRepo = new BetRepository(mockContext.Object);
            var bet = new Bet
            {
                Id = 1,
                BetDate = new DateTime(2020, 11, 04),
                LotId = 1
            };

            await betRepo.DeleteByIdAsync(1);

            mockDbSet.Verify(
                m => m.Remove(It.Is<Bet>(
                    l => l.Id == bet.Id
                        && l.BetDate == bet.BetDate
                        && l.LotId == bet.LotId)),
                Times.Once);
        }

        [Test]
        public void BetRepository_FindAll_ReturnsAllBets()
        {
            var bets = UnitTestsHelper.GetTestBets().ToList();
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Bet>(bets.AsQueryable());
            var mockContext = GetMockContext(mockDbSet);
            var betRepo = new BetRepository(mockContext.Object);

            var result = betRepo.FindAll().ToList();

            Assert.AreEqual(bets.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(bets[i].Id, result[i].Id);
                Assert.AreEqual(bets[i].BetDate, result[i].BetDate);
                Assert.AreEqual(bets[i].LotId, result[i].LotId);
            }
        }

        [Test]
        public void BetRepository_FindAllWithDetails_ReturnsAllBetsWithDetails()
        {
            var bets = UnitTestsHelper.GetTestBets().ToList();
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Bet>(bets.AsQueryable());
            var mockContext = GetMockContext(mockDbSet);
            var betRepo = new BetRepository(mockContext.Object);

            var result = betRepo.FindAllWithDetails().ToList();

            Assert.AreEqual(bets.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(bets[i].Id, result[i].Id);
                Assert.AreEqual(bets[i].BetDate, result[i].BetDate);
                Assert.AreEqual(bets[i].Lot?.Id, result[i].Lot?.Id);
            }
        }

        [Test]
        public async Task BetRepository_GetByIdAsync_ReturnsProperBet()
        {
            var bet = UnitTestsHelper.GetTestBets().First();
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Bet>(UnitTestsHelper.GetTestBets());
            var mockContext = GetMockContext(mockDbSet);
            var betRepo = new BetRepository(mockContext.Object);

            var result = await betRepo.GetByIdAsync(bet.Id);

            Assert.AreEqual(bet.Id, result.Id);
            Assert.AreEqual(bet.BetDate, result.BetDate);
            Assert.AreEqual(bet.LotId, result.LotId);
        }

        [Test]
        public async Task BetRepository_GetByIdWithDetailsAsync_ReturnsProperBetWithDetails()
        {
            var bet = UnitTestsHelper.GetTestBets().First();
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Bet>(UnitTestsHelper.GetTestBets());
            var mockContext = GetMockContext(mockDbSet);
            var betRepo = new BetRepository(mockContext.Object);

            var result = await betRepo.GetByIdWithDetailsAsync(bet.Id);

            Assert.AreEqual(bet.Id, result.Id);
            Assert.AreEqual(bet.BetDate, result.BetDate);
            Assert.AreEqual(bet.Lot?.Id, result.Lot?.Id);
        }

        [Test]
        public void BetRepository_Update_UpdatesBet()
        {
            var mockDbSet = UnitTestsHelper.GetMockDbSet<Bet>(UnitTestsHelper.GetTestBets());
            var mockContext = GetMockContext(mockDbSet);
            var betRepo = new BetRepository(mockContext.Object);
            var bet = new Bet
            {
                Id = 1,
                BetDate = new DateTime(2020, 11, 15),
                LotId = 1
            };

            betRepo.Update(bet);

            mockDbSet.Verify(
                m => m.Attach(It.Is<Bet>(
                    l => l.Id == bet.Id
                        && l.BetDate == bet.BetDate
                        && l.LotId == bet.LotId)),
                Times.Once);
        }
    }
}
