using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Repositories
{
    public class BetRepository : Repository<Bet>, IBetRepository
    {
        public BetRepository(ApplicationDbContext context)
            : base(context)
        { }

        public IQueryable<Bet> FindAllWithDetails()
        {
            return _entities
                .Include(e => e.User)
                .Include(e => e.Lot)
                .AsQueryable();
        }

        public async Task<Bet> GetByIdWithDetailsAsync(int id)
        {
            return await _entities
                .Include(e => e.User)
                .Include(e => e.Lot)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
