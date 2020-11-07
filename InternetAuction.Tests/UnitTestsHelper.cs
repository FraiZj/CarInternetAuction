using InternetAuction.DAL.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using InternetAuction.DAL.Enums;
using AutoMapper;
using InternetAuction.BLL;
using InternetAuction.Tests.DAL.Tests;

namespace InternetAuction.Tests
{
    /// <summary>
    /// Represents class with helper methods for unit testing
    /// </summary>
    public static class UnitTestHelper
    {
        /// <summary>
        /// Creates mapper
        /// </summary>
        /// <returns></returns>
        public static Mapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
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
