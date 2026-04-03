using ManvarFitness.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManvarFitness.Models
{
    public class CustomFormModel
    {
        public int CustomFormId { get; set; }
        public int ConcernId { get; set; }
        public Concerns? Concern { get; set; }
        public int? SubConcernId { get; set; }
        public SubConcerns? SubConcern { get; set; }
        public string? CustomFieldData { get; set; }
        public bool IsDeleted { get; set; } = true;
    }

    public class CustomFieldModel
    {
        public int CustomFieldId { get; set; }
        public int CustomFormId { get; set; }

        [Required]
        public string FieldName { get; set; } = default!;

        [Required]
        public string FieldType { get; set; } = default!;
        public bool IsIncluded { get; set; }
        public bool IsRequired { get; set; }
        public List<string>? Options { get; set; }

        public int? Min { get; set; }
        public int? Max { get; set; }
        public int? MaxLength { get; set; }

        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int? MaxFileSize { get; set; }
        public bool IsDeleted { get; set; }

        public CustomForm?CustomForm { get; set; }
    }

}
