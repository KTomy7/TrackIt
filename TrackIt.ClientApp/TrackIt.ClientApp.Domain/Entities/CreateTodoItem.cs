using TrackIt.ClientApp.Domain.Enums;

namespace TrackIt.ClientApp.Domain.Entities
{
    public class CreateTodoItem
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public PriorityEnum Priority { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
