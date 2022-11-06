using Microsoft.EntityFrameworkCore;
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
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TEntity> Table;

        protected RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
            Table = Context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> FindById(TKey id)
        {
            return await Table.FindAsync(id);
        }

        public virtual async Task<ICollection<TEntity>> GetAll(int limit = 1000)
        {
            return await Table.Take(limit).ToListAsync();
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            await Table.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            Table.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task Delete(TKey id)
        {
            var entity = await FindById(id);
            if (entity != null)
            {
                Table.Remove(entity);
                await Context.SaveChangesAsync();
            }
        }
    }
}