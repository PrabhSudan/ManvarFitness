using ManvarFitness.Entity;
using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Models
{
    public class DietPlanModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; } = true;
        public int ConcernId { get; set; }
        public Concerns? Concern { get; set; }
        public int SubConcernId { get; set; }
        public SubConcerns? SubConcern { get; set; }
        public string? PdfUrl { get; set; }
        public List<DietDayModel> Days { get; set; } = new();
    }

    public class DietDayModel
    {
        public string DayName { get; set; } = "";
        public List<string> EmptyStomach { get; set; } = new();
        public List<string> EarlyMorningSnack { get; set; } = new();
        public List<string> Exercise { get; set; } = new();
        public List<string> Breakfast { get; set; } = new();
        public List<string> MidMorningSnack { get; set; } = new();
        public List<string> Lunch { get; set; } = new();
        public List<string> EveningSnack { get; set; } = new();
        public List<string> Dinner { get; set; } = new();
        public List<string> Bedtime { get; set; } = new();
    }
}
