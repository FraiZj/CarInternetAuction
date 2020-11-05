using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Identity;
using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace InternetAuction.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool disposed;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ApplicationUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>());
            ApplicationRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>());
            CarRepository = new CarRepository(context);
            LotRepository = new LotRepository(context);
            BetRepository = new BetRepository(context);
        }

        public ApplicationUserManager ApplicationUserManager { get;  }

        public ApplicationRoleManager ApplicationRoleManager { get; }

        public ICarRepository CarRepository { get; }

        public ILotRepository LotRepository { get; }

        public IBetRepository BetRepository { get; }

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
