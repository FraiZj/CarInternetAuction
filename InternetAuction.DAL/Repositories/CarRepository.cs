using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Repositories
{
    /// <summary>
    /// Represents a car repository class
    /// </summary>
    public class CarRepository : Repository<Car>, ICarRepository
    {
        /// <summary>
        /// Initializes an instance of the car repository with context
        /// </summary>
        /// <param name="context"></param>
        public CarRepository(ApplicationDbContext context)
            : base(context)
        { }

        public void AddTechnicalPassport(TechnicalPassport technicalPassport)
        {
            _context.TechnicalPassports.Add(technicalPassport);
        }

        /// <summary>
        /// Deletes a car and its technical passport
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(Car entity)
        {
            var technicalPassport = _context.TechnicalPassports.FirstOrDefault(p => p.CarId == entity.Id);

            _context.TechnicalPassports.Remove(technicalPassport);
            _entities.Remove(entity);
        }

        /// <summary>
        /// Deletes a car and its technical passport by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task DeleteByIdAsync(int id)
        {
            var car = await _entities.FirstOrDefaultAsync(p => p.Id == id);
            var technicalPassport = await _context.TechnicalPassports.FirstOrDefaultAsync(p => p.CarId == id);

            _context.TechnicalPassports.Remove(technicalPassport);
            _entities.Remove(car);
        }

        /// <summary>
        /// Returns all cars
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Car> FindAll()
        {
            return _entities
                .Include(e => e.CarImages)
                .AsQueryable();
        }

        public IQueryable<Car> FindAllWithDetails()
        {
            return _entities
                .Include(e => e.CarImages)
                .Include(e => e.TechnicalPassport)
                .AsQueryable();
        }

        /// <summary>
        /// Returns a car by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Car> GetByIdAsync(int id)
        {
            return await _entities
                .Include(e => e.CarImages)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Car> GetByIdWithDetails(int id)
        {
            return await _entities
                .Include(e => e.CarImages)
                .Include(e => e.TechnicalPassport)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void UpdateTechnicalPassport(TechnicalPassport technicalPassport)
        {
            _context.TechnicalPassports.Attach(technicalPassport);
        }
    }
}
