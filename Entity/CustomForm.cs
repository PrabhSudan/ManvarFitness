using System.ComponentModel.DataAnnotations.Schema;

namespace ManvarFitness.Entity
{
    public class CustomForm 
    {
        public int CustomFormId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsRequired { get; set; } = false;

        [ForeignKey("Concern")]
        public int ConcernId { get; set; }
        public Concerns? Concern { get; set; }

        [ForeignKey("SubConcern")]
        public int? SubConcernId { get; set; }
        public SubConcerns? SubConcern { get; set; }


        // Default Fields Toggle
        public bool IncludeName { get; set; } = true;
        public bool IncludeAge { get; set; } = true;
        public bool IncludeGender { get; set; } = true;
        public bool IncludeHeight{ get; set; } = true;
        public bool IncludeWeight { get; set; } = true;
        public ICollection<CustomField>? CustomFields { get; set; }
    }
}
