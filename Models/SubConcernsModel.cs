using ManvarFitness.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManvarFitness.Models
{
    public class SubConcernsModel
    {
        public int SubConcernId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }
        
        public int ConcernId { get; set; }
        public Concerns? MainConcern { get; set; }
    }
}
