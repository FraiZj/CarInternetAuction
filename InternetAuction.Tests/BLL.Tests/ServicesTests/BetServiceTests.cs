using InternetAuction.BLL.Models;
using InternetAuction.BLL.Services;
using InternetAuction.DAL.Entities;
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
    public class BetServiceTests
    {
        private IQueryable<BetModel> GetTestBetsModels()
        {
            return new List<BetModel>
            {
                new BetModel
                {
                    Id = 1,
                    LotId = 1,
                    UserId = "1",
                    BetDate = DateTime.UtcNow.AddDays(-2)
                },
                new BetModel
                {
                    Id = 2,
                    LotId = 2,
                    UserId = "2",
                    BetDate = DateTime.UtcNow
                },
                new BetModel
                {
                    Id = 3,
                    LotId = 3,
                    UserId = "3",
                    BetDate = DateTime.UtcNow.AddDays(2)
                },
            }.AsQueryable();
        }

        public IQueryable<Bet> GetTestBets()
        {
            return new List<Bet>
            {
                new Bet
                {
                    Id = 1,
                    LotId = 1,
                    UserId = "1",
                    BetDate = DateTime.UtcNow.AddDays(-2)
                },
                new Bet
                {
                    Id = 2,
                    LotId = 2,
                    UserId = "2",
                    BetDate = DateTime.UtcNow
                },
                new Bet
                {
                    Id = 3,
                    LotId = 3,
                    UserId = "3",
                    BetDate = DateTime.UtcNow.AddDays(2)
                },
            }.AsQueryable();
        }

        [Test]
        public async Task BetService_AddAsync_AddsBet()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.BetRepository.Add(It.IsAny<Bet>()));
            mockUnitOfWork
                .Setup(m => m.LotRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(
                new Lot
                {
                    Id = 1,
                    Bets = new List<Bet>(),
                });
            var betService = new BetService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var bet = new BetModel
            {
                Id = 1,
                LotId = 1,
                UserId = "3",
                BetDate = DateTime.UtcNow.AddDays(4),
                Sum = 100
            };

            var result = await betService.AddAsync(bet);

            Assert.IsTrue(result.Succedeed);
            mockUnitOfWork.Verify(
                m => m.BetRepository.Add(It.Is<Bet>(
                    b => b.LotId == bet.LotId
                    && b.UserId == bet.UserId
                    && b.BetDate == bet.BetDate
                    && b.Sum == bet.Sum)),
                Times.Once);
            mockUnitOfWork.Verify(
               m => m.SaveAsync(),
               Times.Once);
        }

        [Test]
        public async Task BetService_AddAsync_WithInvalidModelReturnsErrors()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.BetRepository.Add(It.IsAny<Bet>()));
            mockUnitOfWork
                .Setup(m => m.LotRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(
                new Lot
                {
                    Id = 1,
                    Bets = new List<Bet>() { new Bet { Id = 1, Sum = 100 } },
                });
            var betService = new BetService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var bet = new BetModel
            {
                Id = 1,
                LotId = 1,
                UserId = "3",
                BetDate = DateTime.UtcNow.AddDays(4),
                Sum = 50
            };

            var result = await betService.AddAsync(bet);

            Assert.IsFalse(result.Succedeed);
            mockUnitOfWork.Verify(
                m => m.BetRepository.Add(It.IsAny<Bet>()),
                Times.Never);
            mockUnitOfWork.Verify(
               m => m.SaveAsync(),
               Times.Never);
        }

        [Test]
        public async Task BetService_DeleteByIdAsync_DeletesBet()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.BetRepository.DeleteByIdAsync(It.IsAny<int>()));
            var betService = new BetService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var id = 1;

            var result = await betService.DeleteByIdAsync(id);

            Assert.IsTrue(result.Succedeed);
            mockUnitOfWork.Verify(
                m => m.BetRepository.DeleteByIdAsync(id), Times.Once);
            mockUnitOfWork.Verify(
               m => m.SaveAsync(),
               Times.Once);
        }

        [Test]
        public async Task BetService_UpdateAsync_UpdatesBet()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.BetRepository.Update(It.IsAny<Bet>()));
            mockUnitOfWork
                .Setup(m => m.LotRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(
                new Lot
                {
                    Id = 1,
                    Bets = new List<Bet>(),
                });
            var betService = new BetService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var bet = new BetModel
            {
                Id = 1,
                LotId = 1,
                UserId = "3",
                BetDate = DateTime.UtcNow.AddDays(4),
                Sum = 100
            };

            var result = await betService.UpdateAsync(bet);

            Assert.IsTrue(result.Succedeed);
            mockUnitOfWork.Verify(
                m => m.BetRepository.Update(It.Is<Bet>(
                    b => b.LotId == bet.LotId
                    && b.UserId == bet.UserId
                    && b.BetDate == bet.BetDate
                    && b.Sum == bet.Sum)),
                Times.Once);
            mockUnitOfWork.Verify(
               m => m.SaveAsync(),
               Times.Once);
        }

        [Test]
        public async Task BetService_UpdateAsync_WithInvalidModelReturnsErrors()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.BetRepository.Update(It.IsAny<Bet>()));
            mockUnitOfWork
                .Setup(m => m.LotRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(
                new Lot
                {
                    Id = 1,
                    Bets = new List<Bet>() { new Bet { Id = 1, Sum = 100 } },
                });
            var betService = new BetService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var bet = new BetModel
            {
                Id = 1,
                LotId = 1,
                UserId = "3",
                BetDate = DateTime.UtcNow.AddDays(4),
                Sum = 50
            };

            var result = await betService.UpdateAsync(bet);

            Assert.IsTrue(result.Succedeed);
            mockUnitOfWork.Verify(
                m => m.BetRepository.Update(It.Is<Bet>(
                    b => b.LotId == bet.LotId
                    && b.UserId == bet.UserId
                    && b.BetDate == bet.BetDate
                    && b.Sum == bet.Sum)),
                Times.Once);
            mockUnitOfWork.Verify(
               m => m.SaveAsync(),
               Times.Once);
        }

        [Test]
        public void BetService_GetAll_ReturnsAll()
        {
            var expected = GetTestBetsModels().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.BetRepository.FindAllWithDetails()).Returns(GetTestBets());
            var betService = new BetService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = betService.GetAll().ToList();

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Sum, actual[i].Sum);
                Assert.AreEqual(expected[i].BetDate.Date, actual[i].BetDate.Date);
            }
        }

        [Test]
        public async Task BetService_GetByIdWithDetails_ReturnsProperBet()
        {
            var expected = GetTestBetsModels().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.BetRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestBets().First());
            var betService = new BetService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = await betService.GetByIdAsync(expected.Id);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Sum, actual.Sum);
            Assert.AreEqual(expected.BetDate.Date, actual.BetDate.Date);
        }
    }
}
