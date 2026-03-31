namespace ManvarFitness.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
        public string? Mobile { get; set; }
        public DateOnly DOB { get; set; }
        public string? Gender { get; set; }
        public int HeightFeet { get; set; }
        public int HeightInch { get; set; }
        public double WeightKg { get; set; }
        public bool IsActive { get; set; }
        public long? SubscriptionId { get; set; }   
        public string? PlanName { get; set; }
        public decimal? Amount { get; set; }
        public string? Currency { get; set; }
        public string? PaymentGateway { get; set; }
        public int UserDietPlanId { get; set; }
    }
}
