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
