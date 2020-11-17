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
        Task<OperationDetails> Register(UserModel model, string role = Roles.Client);

        Task<ClaimsIdentity> Login(UserModel model);

        Task<UserModel> GetUserModelByIdAsync(string id);

        IEnumerable<UserModel> GetAll();

        Task<OperationDetails> Update(UserModel model);

        Task<OperationDetails> Delete(string id);
    }
}
