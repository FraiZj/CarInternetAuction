using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces.Base;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        IQueryable<Car> FindAllWithTechnicalPassport();
        Task<Car> GetByIdWithTechnicalPassportAsync(int id);
        void AddTechnicalPassport(TechnicalPassport technicalPassport);
        void UpdateTechnicalPassport(TechnicalPassport technicalPassport);
    }
}
