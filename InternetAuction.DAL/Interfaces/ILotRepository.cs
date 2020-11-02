using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces.Base;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    public interface ILotRepository : IRepository<Lot>
    {
        IQueryable<Lot> FindAllWithDetails();
        Task<Lot> GetByIdWithDetailsAsync(int id);
    }
}
