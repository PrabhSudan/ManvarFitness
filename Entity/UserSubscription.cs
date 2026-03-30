using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ManvarFitness.Entity
{
    public class UserSubscription
    {
        [Key]
        public long SubscriptionId { get; set; }
        public Guid UserId { get; set; }
        public int PlanId { get; set; }
        public long PaymentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsTrial { get; set; }
        public string? Status { get; set; } 
        public bool AutoRenew { get; set; }
    }
}
