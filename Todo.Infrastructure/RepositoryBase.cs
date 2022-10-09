using Microsoft.EntityFrameworkCore;
using Todo.Core;
using Todo.Core.Interfaces;

namespace Todo.Infrastructure
{
    public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity, int> where TEntity : class
    {
        protected RepositoryBase(ApplicationDbContext context) : base(context)
        {
        }
    }

    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _table;

        protected RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> FindById(TKey id)
        {
            return await _table.FindAsync(id);
        }

        public virtual async Task<ICollection<TEntity>> GetAll(int limit = 1000)
        {
            return await _table.Take(limit).ToListAsync();
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task Delete(TKey id)
        {
            var entity = await FindById(id);
            if (entity != null)
            {
                _table.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}