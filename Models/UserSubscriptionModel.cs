
namespace ManvarFitness.Models
{
    public class UserSubscriptionModel
    {
        public long SubscriptionId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int PlanId { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public string? PaymentGateway { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsTrial { get; set; }
        public string? Status { get; set; }
        public bool AutoRenew { get; set; }
    }
}
