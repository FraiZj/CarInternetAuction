using InternetAuction.BLL.Infrastructure;
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

        /// <summary>
        /// Returns active lot with details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LotModel> GetActiveLotByIdWIthDetailsAsync(int id);

        /// <summary>
        /// Makes the lot inactive
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<OperationDetails> AddToArchiveAsync(LotModel model);

        /// <summary>
        /// Marks the lot as sold
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<OperationDetails> SellLotAsync(LotModel model, int userId);
    }
}
