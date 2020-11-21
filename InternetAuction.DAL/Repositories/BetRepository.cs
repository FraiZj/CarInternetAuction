using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Repositories
{
    /// <summary>
    /// Represents a bet repository class
    /// </summary>
    public class BetRepository : Repository<Bet>, IBetRepository
    {
        /// <summary>
        /// Initializes an instance of the bet repository with context
        /// </summary>
        /// <param name="context"></param>
        public BetRepository(ApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Returns all bets with details
        /// </summary>
        /// <returns></returns>
        public IQueryable<Bet> FindAllWithDetails()
        {
            return _entities
                .Include(e => e.User)
                .Include(e => e.Lot)
                .AsQueryable();
        }

        /// <summary>
        /// Returns bet with details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Bet> GetByIdWithDetailsAsync(int id)
        {
            return await _entities
                .Include(e => e.User)
                .Include(e => e.Lot)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
