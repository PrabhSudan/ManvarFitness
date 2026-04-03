using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Entity
{
    public class CustomField
    {
        [Key]
        public int CustomFieldId { get; set; }
        public int CustomFormId { get; set; }
        [Required]
        public string FieldName { get; set; } = default!;
        [Required]
        public string FieldType { get; set; } = default!;
        public bool IsIncluded { get; set; }
        public bool IsRequired { get; set; }
        public string? Options { get; set; }
        public string? ValidationData { get; set; }
        public bool IsDeleted { get; set; }
        public CustomForm CustomForm { get; set; }
    }


}
