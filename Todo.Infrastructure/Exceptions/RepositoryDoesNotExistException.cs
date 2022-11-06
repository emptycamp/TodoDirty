namespace Todo.Infrastructure.Exceptions;

public class RepositoryDoesNotExistException: Exception
{
    public RepositoryDoesNotExistException(string message) : base(message)
    {
    }
}