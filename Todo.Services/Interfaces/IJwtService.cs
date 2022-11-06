using Todo.Core.Models;

namespace Todo.Services.Interfaces;

public interface IJwtService
{
    Task<string> CreateTokenAsync(User user);
}