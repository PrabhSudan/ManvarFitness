using ManvarFitness.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManvarFitness.Entity
{

    public class WorkoutVideo : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]  // Ensures Title cannot be null in database
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Difficulty { get; set; }

        public bool IsActive { get; set; } = true;

        [Url]
        [MaxLength(500)]
        public string VideoUrl { get; set; }  // Can be a file path or external URL

        [NotMapped]
        public string Status => IsActive ? "Active" : "Inactive";

        public WorkoutCategory Category { get; set; }
    }
}
