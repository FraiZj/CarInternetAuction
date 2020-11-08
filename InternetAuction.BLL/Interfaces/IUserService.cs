using InternetAuction.BLL.Infrastructure;
using InternetAuction.BLL.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Register(UserModel model);

        Task<ClaimsIdentity> Login(UserModel model);

        Task<UserModel> GetUserModelByIdAsync(string id);
    }
}
