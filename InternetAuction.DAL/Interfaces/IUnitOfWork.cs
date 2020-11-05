using InternetAuction.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager ApplicationUserManager { get; }
        ApplicationRoleManager ApplicationRoleManager { get; }
        ICarRepository CarRepository { get; }
        ILotRepository LotRepository { get; }
        IBetRepository BetRepository { get; }
        Task SaveAsync();
    }
}
