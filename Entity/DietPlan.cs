using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Entity
{
    public class DietPlan : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DayName { get; set; }
        public string? EmptyStomach { get; set; }
        public string? EarlyMorningSnack { get; set; }
        public string? Exercise { get; set; }
        public string? Breakfast { get; set; }
        public string? MidMorningSnack { get; set; }
        public string? Lunch { get; set; }
        public string? EveningSnack { get; set; }
        public string? Dinner { get; set; }
        public int? DurantionInMinutes { get; set; }
        public string? Description { get; set; }
        public string? Bedtime { get; set; }
        public bool IsActive { get; set; } 
    }
}
