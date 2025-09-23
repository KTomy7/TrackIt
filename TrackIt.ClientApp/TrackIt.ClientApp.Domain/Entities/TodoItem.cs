using TrackIt.ClientApp.Domain.Enums;

namespace TrackIt.ClientApp.Domain.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public PriorityEnum Priority { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDone { get; set; }
    }
}
