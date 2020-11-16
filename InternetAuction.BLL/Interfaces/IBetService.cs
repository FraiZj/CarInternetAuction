using InternetAuction.BLL.Models;
using System;

namespace InternetAuction.BLL.Interfaces
{
    public interface IBetService : ICrud<BetModel>, IDisposable
    {
    }
}
