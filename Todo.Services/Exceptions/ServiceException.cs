namespace Todo.Services.Exceptions
{
    public class ServiceException : Exception
    {
        public string ParamName { get; } = "";

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string paramName, string message) : base(message)
        {
            ParamName = paramName;
        }
    }
}
