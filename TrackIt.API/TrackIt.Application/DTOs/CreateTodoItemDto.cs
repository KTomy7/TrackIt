using TrackIt.Domain.Enums;

namespace TrackIt.Application.DTOs
{
    public class CreateTodoItemDto
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public PriorityEnum Priority { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
