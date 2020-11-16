using InternetAuction.BLL.Infrastructure;
using InternetAuction.BLL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Interfaces
{
    /// <summary>
    /// Represents an lot service interface
    /// </summary>
    public interface ILotService : ICrud<LotModel>, IDisposable
    {
        /// <summary>
        /// Returns all lots with details
        /// </summary>
        /// <returns></returns>
        IQueryable<LotModel> GetAllWithDetails();

        /// <summary>
        /// Returns lot with details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LotModel> GetByIdWithDetailsAsync(int id);

        /// <summary>
        /// Returns lots by search model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IQueryable<LotModel> SearchLotModels(SearchModel model);

        /// <summary>
        /// Marks the lot as sold
        /// </summary>
        /// <param name="lotId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<OperationDetails> SellLot(int lotId, string userId);
    }
}
