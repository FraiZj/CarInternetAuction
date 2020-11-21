using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Repositories
{
    /// <summary>
    /// Represents a lot repository class
    /// </summary>
    public class LotRepository : Repository<Lot>, ILotRepository
    {
        /// <summary>
        /// Initializes an instance of the lot repository with context
        /// </summary>
        /// <param name="context"></param>
        public LotRepository(ApplicationDbContext context)
            : base(context)
        {}

        /// <summary>
        /// Returns all lots
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Lot> FindAll()
        {
            return _entities
                .Include(e => e.Car)
                .AsQueryable();
        }

        public IQueryable<Lot> FindAllWithDetails()
        {
            return _entities
                .Include(e => e.Car)
                .Include(e => e.Seller)
                .Include(e => e.Bets)
                .Include(e => e.Buyer)
                .AsQueryable();
        }

        /// <summary>
        /// Returns a lot with details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Lot> GetByIdAsync(int id)
        {
            return await _entities
                .Include(e => e.Car)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Returns lot with details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Lot> GetByIdWithDetailsAsync(int id)
        {
            return await _entities
                .Include(e => e.Car)
                .Include(e => e.Seller)
                .Include(e => e.Bets)
                .Include(e => e.Buyer)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Updates lot
        /// </summary>
        /// <param name="entity"></param>
        public override void Update(Lot entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity.Car).State = EntityState.Modified;
            _context.Entry(entity.Car.TechnicalPassport).State = EntityState.Modified;
        }
    }
}
