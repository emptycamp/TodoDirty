namespace Todo.Infrastructure.Exceptions;

public class RepositoryDoesntExistException: Exception
{
    public RepositoryDoesntExistException(string message) : base(message)
    {
    }
}