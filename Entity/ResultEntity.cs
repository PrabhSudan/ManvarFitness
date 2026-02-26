using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManvarFitness.Entity
{
    public class ResultEntity : BaseEntity
    {
        [Key]
        public int ResultId { get; set; }

        // Foreign key to User
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        // Foreign key to ConcernCategory
        public int ConcernCategoryId { get; set; }    
        [ForeignKey("ConcernCategoryId")]
        public Concerns? ConcernCategory { get; set; }

        // Foreign key to SubConcern
        public int SubConcernId { get; set; }
        [ForeignKey("SubConcernId")]
        public SubConcerns? SubConcern { get; set; }

        public string? BeforeImage { get; set; }
        public string? AfterImage { get; set; }
        public string? Video { get; set; }
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}