using InternetAuction.BLL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Interfaces
{
    /// <summary>
    /// Represents an lot service interface
    /// </summary>
    public interface ILotService : ICrud<LotModel>
    {
        /// <summary>
        /// Returns all lots with details
        /// </summary>
        /// <returns></returns>
        IQueryable<LotModel> GetAllWithDetails();

        /// <summary>
        /// Returns all active lots
        /// </summary>
        /// <returns></returns>
        IQueryable<LotModel> GetAllActiveLots();

        /// <summary>
        /// Returns all active lots with details 
        /// </summary>
        /// <returns></returns>
        IQueryable<LotModel> GetAllActiveLotsWithDetails();

        /// <summary>
        /// Returns lot with details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LotModel> GetByIdWithDetailsAsync(int id);

        IQueryable<LotModel> SearchLotModels(SearchModel model);
    }
}
