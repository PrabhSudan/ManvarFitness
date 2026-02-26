using ManvarFitness.Enum;
using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Models
{
    public class WorkoutVideoModel : IValidatableObject
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Difficulty { get; set; }
        public string Status => IsActive ? "Active" : "Inactive";
        public bool IsActive { get; set; }

        public IFormFile? VideoFile { get; set; }

        [Url]
        public string? VideoUrl { get; set; }

        public WorkoutCategory Category { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var maxFileSize = 10 * 1024 * 1024; // 10 MB
            var allowedExtensions = new[] { ".mp4", ".mov", ".avi", ".wmv" };

            if (VideoFile != null)
            {
                // Check size
                if (VideoFile.Length > maxFileSize)
                    yield return new ValidationResult($"Video {VideoFile.FileName} is too large (Max 10 MB).", new[] { nameof(VideoFile) });

                // Check extension
                var ext = Path.GetExtension(VideoFile.FileName)?.ToLower();
                if (ext == null || !allowedExtensions.Contains(ext))
                    yield return new ValidationResult($"Video {VideoFile.FileName} has invalid extension.", new[] { nameof(VideoFile) });
            }
        }
    }
}
