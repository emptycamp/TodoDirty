using System.ComponentModel;

namespace Todo.Shared.Queries
{
    public class LimitQuery
    {
        [DefaultValue(1000)]
        public int Limit { get; set; } = 1000;

        [DefaultValue(0)]
        public int Offset { get; set; } = 0;
    }
}
