namespace TrackIt.Application.DTOs
{
    public class TodoItemDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public int Priority { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDone { get; set; }
    }
}
