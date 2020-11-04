using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Repositories
{
    public class LotRepository : Repository<Lot>, ILotRepository
    {
        public LotRepository(ApplicationDbContext context)
            : base(context)
        {}

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

        public override async Task<Lot> GetByIdAsync(int id)
        {
            return await _entities
                .Include(e => e.Car)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Lot> GetByIdWithDetailsAsync(int id)
        {
            return await _entities
                .Include(e => e.Car)
                .Include(e => e.Seller)
                .Include(e => e.Bets)
                .Include(e => e.Buyer)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
