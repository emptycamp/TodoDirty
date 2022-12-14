using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Todo.Core.Exceptions;
using Todo.Core.Interfaces;
using Todo.Core.Models;

namespace Todo.Infrastructure
{
    public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity, int> where TEntity : EntityBase
    {
        protected RepositoryBase(ApplicationDbContext context) : base(context)
        {
        }
    }

    public abstract class RepositoryBaseWithUser<TEntity> : IRepository<TEntity, int> where TEntity : EntityWithUser
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TEntity> Table;

        protected RepositoryBaseWithUser(ApplicationDbContext context)
        {
            Context = context;
            Table = Context.Set<TEntity>();
        }

        public virtual async Task<TEntity> FindByIdOrThrow(int id)
        {
            return await Table.AsNoTracking().Include(x => x.User).FirstOrDefaultAsync(x => x.Id!.Equals(id)) ?? throw new NotFoundException(typeof(TEntity).Name);
        }

        protected async Task<TEntity> FindByIdOrThrowTracked(int id)
        {
            return await Table.Include(x => x.User).FirstAsync(x => x.Id == id) ?? throw new NotFoundException(typeof(TEntity).Name);
        }

        public virtual async Task<ICollection<TEntity>> GetAll(int limit = 1000, int offset = 0)
        {
            return await Table.AsNoTracking().Include(x => x.User).Skip(offset).Take(limit).ToListAsync();
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            await Table.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            var exists = Table.Any(x => x.Id!.Equals(entity.Id));
            if (!exists)
            {
                throw new NotFoundException(typeof(TEntity).Name);
            }

            Table.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task Delete(TEntity entity)
        {
            Table.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }

    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TEntity> Table;

        protected RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
            Table = Context.Set<TEntity>();
        }

        public virtual async Task<TEntity> FindByIdOrThrow(TKey id)
        {
            return await Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id!.Equals(id)) ?? throw new NotFoundException(typeof(TEntity).Name);
        }

        protected async Task<TEntity> FindByIdOrThrowTracked(TKey id)
        {
            return await Table.FindAsync(id) ?? throw new NotFoundException(typeof(TEntity).Name);
        }

        public virtual async Task<ICollection<TEntity>> GetAll(int limit = 1000, int offset = 0)
        {
            return await Table.AsNoTracking().Skip(offset).Take(limit).ToListAsync();
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            await Table.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            var exists = Table.Any(x => x.Id!.Equals(entity.Id));
            if (!exists)
            {
                throw new NotFoundException(typeof(TEntity).Name);
            }

            Table.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task Delete(TEntity entity)
        {
            Table.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
