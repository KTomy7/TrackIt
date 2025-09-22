namespace TrackIt.Application.DTOs
{
    public class TodoItemCreateDto
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public int Priority { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
