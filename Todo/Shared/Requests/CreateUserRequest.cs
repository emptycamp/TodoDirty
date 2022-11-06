using System.ComponentModel.DataAnnotations;

namespace Todo.Shared.Requests
{
    public class CreateUserRequest
    {
        /// <summary>
        /// Email address
        /// </summary>
        /// <example>user@example.com</example>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// User name
        /// </summary>
        /// <example>user</example>
        [Required]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Password
        /// </summary>
        /// <example>example</example>
        [Required]
        public string Password { get; set; } = null!;
    }
}
