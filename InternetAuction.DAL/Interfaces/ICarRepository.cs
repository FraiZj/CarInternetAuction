using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces.Base;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Interface that exposes car repository methods
    /// </summary>
    public interface ICarRepository : IRepository<Car>
    {
        /// <summary>
        /// Returns all cars with details
        /// </summary>
        /// <returns></returns>
        IQueryable<Car> FindAllWithDetails();

        /// <summary>
        /// Returns a car with details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Car> GetByIdWithDetails(int id);

        /// <summary>
        /// Creates a technical passport
        /// </summary>
        /// <param name="technicalPassport"></param>
        void AddTechnicalPassport(TechnicalPassport technicalPassport);

        /// <summary>
        /// Updates a technical passport
        /// </summary>
        /// <param name="technicalPassport"></param>
        void UpdateTechnicalPassport(TechnicalPassport technicalPassport);
    }
}
