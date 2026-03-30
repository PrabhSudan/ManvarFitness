namespace ManvarFitness.Models
{
    public class SubscriptionPlanModel
    {
        public int PlanId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Currency { get; set; }
        public int DurationDays { get; set; }
        public int? TrialDays { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
