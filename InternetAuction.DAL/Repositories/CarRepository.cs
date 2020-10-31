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

        public override async Task<IQueryable<Car>> FindAllAsync()
        {
            return await _entities
                .Include(e => e.BodyType)
                .Include(e => e.CarImages)
                .Include(e => e.DriveUnit)
                .Include(e => e.Transmission)
                .ToListAsync() as IQueryable<Car>;
        }

        public async Task<IQueryable<Car>> FindAllWithTechnicalPassportAsync()
        {
            return await _entities
                .Include(e => e.BodyType)
                .Include(e => e.CarImages)
                .Include(e => e.DriveUnit)
                .Include(e => e.Transmission)
                .Include(e => e.TechnicalPassport)
                .ToListAsync() as IQueryable<Car>;
        }

        public override async Task<Car> GetByIdAsync(int id)
        {
            return await _entities
                .Include(e => e.BodyType)
                .Include(e => e.CarImages)
                .Include(e => e.DriveUnit)
                .Include(e => e.Transmission)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Car> GetByIdWithTechnicalPassportAsync(int id)
        {
            return await _entities
                .Include(e => e.BodyType)
                .Include(e => e.CarImages)
                .Include(e => e.DriveUnit)
                .Include(e => e.Transmission)
                .Include(e => e.TechnicalPassport)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
