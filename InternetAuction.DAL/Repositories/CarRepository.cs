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

        public void AddTechnicalPassport(TechnicalPassport technicalPassport)
        {
            _context.TechnicalPassports.Add(technicalPassport);
        }

        public override void Delete(Car entity)
        {
            var technicalPassport = _context.TechnicalPassports.FirstOrDefault(p => p.CarId == entity.Id);

            _context.TechnicalPassports.Remove(technicalPassport);
            _entities.Remove(entity);
        }

        public override async Task DeleteByIdAsync(int id)
        {
            var car = await _entities.FirstOrDefaultAsync(p => p.Id == id);
            var technicalPassport = await _context.TechnicalPassports.FirstOrDefaultAsync(p => p.CarId == id);

            _context.TechnicalPassports.Remove(technicalPassport);
            _entities.Remove(car);
        }

        public override IQueryable<Car> FindAll()
        {
            return _entities
                .Include(e => e.BodyType)
                .Include(e => e.CarImages)
                .Include(e => e.DriveUnit)
                .Include(e => e.Transmission)
                .Include(e => e.TechnicalPassport)
                .AsQueryable();
        }

        public IQueryable<Car> FindAllWithTechnicalPassport()
        {
            return _entities
                .Include(e => e.BodyType)
                .Include(e => e.CarImages)
                .Include(e => e.DriveUnit)
                .Include(e => e.Transmission)
                .Include(e => e.TechnicalPassport)
                .AsQueryable();
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

        public void UpdateTechnicalPassport(TechnicalPassport technicalPassport)
        {
            _context.TechnicalPassports.Attach(technicalPassport);
        }
    }
}
