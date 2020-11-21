using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Identity;
using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace InternetAuction.DAL
{
    /// <summary>
    /// Represents a unit of work class
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool disposed;

        /// <summary>
        /// Initializes the instance of the unit of work with context
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ApplicationUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            ApplicationRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            CarRepository = new CarRepository(context);
            LotRepository = new LotRepository(context);
            BetRepository = new BetRepository(context);
            Logger = new Logger(context);
        }

        /// <summary>
        /// Exposess application user manager
        /// </summary>
        public ApplicationUserManager ApplicationUserManager { get;  }

        /// <summary>
        /// Exposess application role manager
        /// </summary>
        public ApplicationRoleManager ApplicationRoleManager { get; }

        /// <summary>
        /// Exposess car repository
        /// </summary>
        public ICarRepository CarRepository { get; }

        /// <summary>
        /// Exposess lot repository
        /// </summary>
        public ILotRepository LotRepository { get; }

        /// <summary>
        /// Exposess bet repository
        /// </summary>
        public IBetRepository BetRepository { get; }

        /// <summary>
        /// Exposess logger
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// Saves changes
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
