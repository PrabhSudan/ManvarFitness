using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManvarFitness.Entity
{
    public class Concerns : BaseEntity
    {
        [Key]
        public int ConcernId { get; set; }  // Primary Key

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!; // Not nullable

        public bool IsActive { get; set; } = true;
    }
}