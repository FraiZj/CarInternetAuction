using InternetAuction.BLL.Models;
using System;

namespace InternetAuction.BLL.Interfaces
{
    /// <summary>
    /// Interface that exposes bet service methods
    /// </summary>
    public interface IBetService : ICrud<BetModel>, IDisposable
    {
    }
}
