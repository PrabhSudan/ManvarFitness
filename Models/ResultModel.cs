using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Models
{
    public class ResultModel : IValidatableObject
    {
        public int ResultId { get; set; }
        // User selection
        [Required(ErrorMessage = "Please Select a User")]
        public int UserId { get; set; }
        // Concern Category selection
        [Required(ErrorMessage = "Please Select a Concern")]
        public int ConcernCategoryId { get; set; }
        // Sub Concern selection
        [Required(ErrorMessage = "Please Select a Concern")]
        public int SubConcernId { get; set; }
        // Before Image upload
        [Display(Name = "Before Image")]
        public List<IFormFile>? BeforeImageFile { get; set; }

        // After Image upload
        [Display(Name = "After Image")]
        public List<IFormFile>? AfterImageFile { get; set; }
        // Video upload
        [Display(Name = "Videos")]
        public List<IFormFile>? Videos { get; set; }

        // Description
        [StringLength(500)]
        public string? Description { get; set; }

        // Active/Inactive
        public bool IsActive { get; set; } = true;
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var maxFileSize = 10 * 1024 * 1024; // 10 MB
            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".doc", ".docx", ".pdf" };

            IEnumerable<ValidationResult> CheckFiles(List<IFormFile>? files, string fieldName)
            {
                if (files == null) yield break;

                foreach (var file in files)
                {
                    if (file.Length > maxFileSize)
                        yield return new ValidationResult($"File {file.FileName} is too large (Max 10 MB).", new[] { fieldName });

                    var ext = Path.GetExtension(file.FileName)?.ToLower();
                    if (ext == null || !allowedExtensions.Contains(ext))
                        yield return new ValidationResult($"File {file.FileName} has invalid extension.", new[] { fieldName });
                }
            }

            foreach (var error in CheckFiles(BeforeImageFile, "BeforeImageFile"))
                yield return error;

            foreach (var error in CheckFiles(AfterImageFile, "AfterImageFile"))
                yield return error;
        }
    }
}
