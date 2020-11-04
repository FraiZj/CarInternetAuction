using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces.Base;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    public interface IBetRepository : IRepository<Bet>
    {
        IQueryable<Bet> FindAllWithDetails();
        Task<Bet> GetByIdWithDetailsAsync(int id);
    }
}
