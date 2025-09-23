using System.ComponentModel.DataAnnotations;
using TrackIt.Domain.Enums;

namespace TrackIt.Application.DTOs
{
    public class CreateTodoItemDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(64, ErrorMessage = "Name cannot exceed 64 characters.")]
        public required string Name { get; set; }

        [MaxLength(256, ErrorMessage = "Description cannot exceed 256 characters.")]
        public string? Description { get; set; }

        public PriorityEnum Priority { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
