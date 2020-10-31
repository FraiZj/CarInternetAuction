using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(ApplicationDbContext context)
            : base(context)
        { }

        public async Task<IQueryable<Car>> FindAllWithDetailsAsync()
        {
            return await _entities
                .Include(e => e.BodyType)
                .Include(e => e.CarImages)
                .Include(e => e.DriveUnit)
                .Include(e => e.TechnicalPassport)
                .Include(e => e.Transmission)
                .ToListAsync() as IQueryable<Car>;
        }

        public async Task<Car> GetByIdWithDetailsAsync(int id)
        {
            return await _entities
                .Include(e => e.BodyType)
                .Include(e => e.CarImages)
                .Include(e => e.DriveUnit)
                .Include(e => e.TechnicalPassport)
                .Include(e => e.Transmission)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
