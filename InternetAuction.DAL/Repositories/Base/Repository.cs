using InternetAuction.DAL.Entities.Base;
using InternetAuction.DAL.Interfaces.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity != null)
                _entities.Remove(entity);
        }

        public async Task<IQueryable<TEntity>> FindAllAsync()
        {
            return await _entities.ToListAsync() as IQueryable<TEntity>;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _entities.FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(TEntity entity)
        {
            _entities.Attach(entity);
        }
    }
}
