using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Todo.Core.Models;

[Index(nameof(UserId))]
[Index(nameof(Token), IsUnique = true)]
public class RefreshToken: EntityBase
{
    public required string Token { get; set; }
    public bool IsRevoked { get; set; }
    public required DateTime ExpiryDate { get; set; }

    [ForeignKey(nameof(UserId))]
    public required Guid UserId { get; set; }
    public required User User { get; set; }
}