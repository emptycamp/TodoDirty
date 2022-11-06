namespace Todo.Core.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string field) : base(GenerateMessage(field)) { }

        private static string GenerateMessage(string field)
        {
            return $"{field} was not found";
        }
    }
}
