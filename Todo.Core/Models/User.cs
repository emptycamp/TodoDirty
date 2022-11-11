using Microsoft.AspNetCore.Identity;

namespace Todo.Core.Models
{
    public class User: IdentityUser<Guid>
    {
        public override required string UserName { get; set; }
    }
}
