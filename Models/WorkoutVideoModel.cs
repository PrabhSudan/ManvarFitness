using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Models
{
    public class WorkoutVideoModel
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Difficulty { get; set; }
        public string Status => IsActive ? "Active" : "Inactive";
        public bool IsActive { get; set; }

        public IFormFile? VideoFile { get; set; }

        [Url]
        public string? VideoUrl { get; set; }
    }
}
