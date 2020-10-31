using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces.Base;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<IQueryable<Car>> FindAllWithTechnicalPassportAsync();
        Task<Car> GetByIdWithTechnicalPassportAsync(int id);
    }
}
