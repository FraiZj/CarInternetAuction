using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces.Base;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Interface that exposes bet repository methods
    /// </summary>
    public interface IBetRepository : IRepository<Bet>
    {
        /// <summary>
        /// Returns all bets with details
        /// </summary>
        /// <returns></returns>
        IQueryable<Bet> FindAllWithDetails();

        /// <summary>
        /// Returns a bet with details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Bet> GetByIdWithDetailsAsync(int id);
    }
}
