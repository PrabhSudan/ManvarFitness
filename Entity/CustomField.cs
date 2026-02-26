using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Entity
{
    public class CustomField
    {
        [Key]
        public int CustomFieldId { get; set; }
        public string? Label { get; set; }
        public string? FieldType { get; set; } // FieldType : text, number, select, checkbox
        public bool IsActive { get; set; }
        public bool IsRequired { get; set; }
        public string? Options { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public int? MaxLength { get; set; }
        public int? CustomFormId { get; set; }
        public CustomForm? CustomForm { get; set; }
    }

    public class FormSubmission
    {
        [Key]
        public int FormSubId { get; set; }
        // Default fields
        [MaxLength(100)]
        public string? Name { get; set; }

        [Range(1,90)]
        public int? Age { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; }

        [Range(0,9)]
        public int? HeightFeet { get; set; }

        [Range(0,11)]
        public int? HeightInch { get; set; }
        [Range(1, 200)]
        public decimal? Weight { get; set; }
        public int CustomFormId { get; set; }
        public CustomForm? CustomForm { get; set; }

        public ICollection<FormAnswer>? Answers { get; set; }
    }
    
    public class FormAnswer
    {
        [Key]
        public int FormAnsId { get; set; }
        public int CustomFieldId { get; set; }
        public CustomField? CustomField { get; set; }

        public string? Value { get; set; }

        public int FormSubId { get; set; }
        public FormSubmission? FormSubmission { get; set; }
    }
}
