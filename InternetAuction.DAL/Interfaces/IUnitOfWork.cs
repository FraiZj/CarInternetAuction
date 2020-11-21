using InternetAuction.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Interface that exposes a unit of work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Exposes an application user manager
        /// </summary>
        ApplicationUserManager ApplicationUserManager { get; }

        /// <summary>
        /// Exposes an application role manager
        /// </summary>
        ApplicationRoleManager ApplicationRoleManager { get; }

        /// <summary>
        /// Exposes a car repository
        /// </summary>
        ICarRepository CarRepository { get; }

        /// <summary>
        /// Exposes a lot repository
        /// </summary>
        ILotRepository LotRepository { get; }

        /// <summary>
        /// Exposes a bet repository
        /// </summary>
        IBetRepository BetRepository { get; }

        /// <summary>
        /// Exposes a logger
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// Saves database context
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
