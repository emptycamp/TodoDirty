namespace Todo.Services.Interfaces;

public interface ICreateImpersonate<in TRequest, TResponse>
{
    Task<TResponse> CreateImpersonatedEntity(TRequest entityDto);
}