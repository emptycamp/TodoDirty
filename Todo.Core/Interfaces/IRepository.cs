namespace Todo.Core.Interfaces
{
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class
    {
    }

    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        public Task<TEntity> FindByIdOrThrow(TKey id);
        public Task<ICollection<TEntity>> GetAll(int limit = 1000, int offset = 0);
        public Task<TEntity> Create(TEntity entity);
        public Task<TEntity> Update(TEntity entity);
        public Task Delete(TEntity entity);
    }
}