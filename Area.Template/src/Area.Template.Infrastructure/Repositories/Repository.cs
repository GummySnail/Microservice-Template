using Area.Template.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Area.Template.Infrastructure.Repositories
{
    internal abstract class Repository<TEntity, TEntityId>(ApplicationDbContext dbContext)
        where TEntity : Entity<TEntityId>
        where TEntityId : class
    {
        protected readonly ApplicationDbContext DbContext = dbContext;

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public virtual void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }
    }
}
