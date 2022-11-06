namespace Todo.Services.Exceptions
{
    public class AuthenticationException: Exception
    {
        public AuthenticationException() : base("Invalid credentials") { }
    }
}
