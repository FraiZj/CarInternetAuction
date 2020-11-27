using InternetAuction.BLL.Infrastructure;
using InternetAuction.BLL.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Interfaces
{
    /// <summary>
    /// Represents an interface that exposes CRUD methods
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface ICrud<TModel> where TModel : BaseModel 
    {
        /// <summary>
        /// Returns all models
        /// </summary>
        /// <returns></returns>
        IEnumerable<TModel> GetAll();

        /// <summary>
        /// Returns model by id
        /// </summary>
        /// <returns></returns>
        Task<TModel> GetByIdAsync(int id);

        /// <summary>
        /// Creates the model
        /// </summary>
        /// <returns></returns>
        Task<OperationDetails> AddAsync(TModel model);

        /// <summary>
        /// Updates the model
        /// </summary>
        /// <returns></returns>
        Task<OperationDetails> UpdateAsync(TModel model);

        /// <summary>
        /// Deletes the model by id
        /// </summary>
        /// <returns></returns>
        Task<OperationDetails> DeleteByIdAsync(int id);
    }
}
