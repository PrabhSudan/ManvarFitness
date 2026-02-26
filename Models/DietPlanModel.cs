using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Models
{
    public class DietPlanModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<string> DayName { get; set; } = new();
        public List<string> EmptyStomach { get; set; } = new();
        public List<string> EarlyMorningSnack { get; set; } = new();
        public List<string> Exercise { get; set; } = new();
        public List<string> Breakfast { get; set; } = new();
        public List<string> MidMorningSnack { get; set; } = new();
        public List<string> Lunch { get; set; } = new();
        public List<string> EveningSnack { get; set; } = new();
        public List<string> Dinner { get; set; } = new();
        public List<string> Bedtime { get; set; } = new();
        public string? Description { get; set; } = null;
        public bool IsActive { get; set; } = true;
    }
}
