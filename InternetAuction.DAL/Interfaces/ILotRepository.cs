using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces.Base;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Interface that exposes lot repository methods
    /// </summary>
    public interface ILotRepository : IRepository<Lot>
    {
        /// <summary>
        /// Returns all lots with details
        /// </summary>
        /// <returns></returns>
        IQueryable<Lot> FindAllWithDetails();

        /// <summary>
        /// Returns a lot with details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Lot> GetByIdWithDetailsAsync(int id);
    }
}
