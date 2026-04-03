using System.ComponentModel.DataAnnotations.Schema;

namespace ManvarFitness.Entity
{
    public class CustomForm 
    {
        public int CustomFormId { get; set; }

        [ForeignKey("Concern")]
        public int ConcernId { get; set; }
        public Concerns? Concern { get; set; }

        [ForeignKey("SubConcern")]
        public int? SubConcernId { get; set; }
        public SubConcerns? SubConcern { get; set; }
        public string? CustomFieldData { get; set; }
        public bool IsDeleted { get; set; } = true;
    }
}
