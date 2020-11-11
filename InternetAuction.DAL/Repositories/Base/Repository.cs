using InternetAuction.DAL.Entities.Base;
using InternetAuction.DAL.Interfaces.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Repositories.Base
{
    /// <summary>
    /// Abstract class that implements basic repository methods
    /// </summary>
    /// <typeparam name="TEntity">Type of repository elements</typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _entities;

        /// <summary>
        /// Initializes an instance of the generic repository with context
        /// </summary>
        /// <param name="context"></param>
        protected Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public virtual async Task DeleteByIdAsync(int id)
        {
            var entity = await _entities.FirstOrDefaultAsync(e => e.Id == id);
            _entities.Remove(entity);
        }

        public virtual IQueryable<TEntity> FindAll()
        {
            return _entities.AsQueryable();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _entities.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual void Update(TEntity entity)
        {
            _entities.Attach(entity);
        }
    }
}
