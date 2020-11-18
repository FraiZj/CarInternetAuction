using InternetAuction.BLL.Infrastructure;
using InternetAuction.BLL.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<OperationDetails> Register(UserModel model, string role = Roles.Client);

        /// <summary>
        /// Returns user claims
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ClaimsIdentity> Login(UserModel model);

        /// <summary>
        /// Returns user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserModel> GetUserModelByIdAsync(string id);

        /// <summary>
        /// Returns all user
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserModel> GetAll();

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<OperationDetails> Update(UserModel model);

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationDetails> Delete(string id);
    }
}
