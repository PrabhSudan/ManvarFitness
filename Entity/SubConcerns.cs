using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManvarFitness.Entity
{
    public class SubConcerns:BaseEntity
    {
        [Key]
        public int SubConcernId { get; set; }  

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [ForeignKey("Concern")]
        public int ConcernId { get; set; }

        public Concerns? Concern { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
