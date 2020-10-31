using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces.Base;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<IQueryable<Car>> FindAllWithDetailsAsync();
        Task<Car> GetByIdWithDetailsAsync(int id);
    }
}
