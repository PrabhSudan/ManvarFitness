using ManvarFitness.Entity;
using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Models
{
    public class CustomFormModel
    {
        public int? CustomFormId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        public int ConcernId { get; set; }
        public int? SubConcernId { get; set; }

        //Default fields toggles
        public bool IncludeName { get; set; } = true;
        public bool IncludeAge { get; set; } = true;
        public bool IncludeGender { get; set; } = true;
        public bool IncludeHeight { get; set; } = true;
        public bool IncludeWeight { get; set; } = true;
        public List<CustomFieldModel>? Fields { get; set; }
    }

    public class FillFormModel
    {
        public int CustomFormId { get; set; }
        // Default fields
        [MaxLength(100)]
        public string? Name { get; set; }
        [Range(1,90)]
        public int? Age { get; set; }
        [MaxLength(10)]
        public string? Gender { get; set; }
        [Range(0,9)]
        public int? HeightFeet { get; set; }
        [Range(0, 11)]
        public int? HeightInch { get; set; }
        [Range(1,200)]
        public decimal? Weight { get; set; }

        public Dictionary<int, string>? CustomFieldValues { get; set; }
    }

    public class CustomFieldModel
    {
        public int CustomFormId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Label { get; set; }

        [Required]
        public string? FieldType { get; set; }
        public string? Options { get; set; }
        public bool IsRequired { get; set; }
        public bool IsActive { get; set; }

        // Validation/Limits
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public int? MaxLength { get; set; }
    }

    public class FormAnswerModel
    {
        public int CustomFieldId { get; set; }
        public string? Value { get; set; }
        public string? Label { get; set; }
        public string? FieldType { get; set; }
        public bool IsRequired { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public int? MaxLength { get; set; }
    }

    public class FieldDisplayModel
    {
        public int? CustomFormId { get; set; }
        public int? CustomFieldId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string Range { get; set; }
        public bool IsActive { get; set; }
        public bool IsRequired { get; set; }
    }
}
