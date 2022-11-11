using System.ComponentModel.DataAnnotations;

namespace Todo.Shared.Requests
{
    public class AuthenticateUserRequest
    {
        /// <summary>
        /// Email address
        /// </summary>
        /// <example>user@example.com</example>
        [EmailAddress]
        public required string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        /// <example>example</example>
        public required string Password { get; set; }
    }
}
