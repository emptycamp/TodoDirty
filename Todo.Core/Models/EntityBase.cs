namespace Todo.Core.Models
{
    public class EntityWithUser : EntityBase<int>
    {
        public required User User { get; set; }
    }

    public class EntityBase : EntityBase<int>
    {
    }

    public class EntityBase<TKey>
    {
        public TKey Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
