using System.ComponentModel.DataAnnotations;

namespace Todo.Shared.Requests
{
    public class AuthenticateUserRequest
    {
        /// <summary>
        /// Email address
        /// </summary>
        /// <example>user@example.com</example>
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        /// <example>example</example>
        [Required]
        public string? Password { get; set; }
    }
}
