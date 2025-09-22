using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrackIt.Domain.Enums;

namespace TrackIt.Domain.Entities
{
    [Table("TodoItems")]
    public class TodoItemEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public required string Name { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }

        [Required]
        public PriorityEnum Priority { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDone { get; set; } = false;
    }
}
