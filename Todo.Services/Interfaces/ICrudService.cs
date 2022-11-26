namespace Todo.Services.Interfaces;

public interface ICrudService<in TRequest, TResponse> : ICrudService<TRequest, TResponse, int>
{
}

public interface ICrudService<in TRequest, TResponse, in TKey>
{
    Task<ICollection<TResponse>> GetAllEntities(int limit, int offset);
    Task<TResponse?> GetEntity(TKey id);
    Task<TResponse> CreateEntity(TRequest entityDto, Guid userId);
    Task<TResponse> UpdateEntity(TKey id, TRequest entityDto, Guid? userId);
    Task DeleteEntity(TKey id, Guid? userId);
}