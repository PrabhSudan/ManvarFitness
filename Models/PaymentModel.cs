namespace ManvarFitness.Models
{
    public class PaymentModel
    {
        public long PaymentId { get; set; }
        public Guid UserId { get; set; }
        public long SubscriptionId { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public string? PaymentGateway { get; set; }
        public string? TransactionId { get; set; }
        public bool Status { get; set; }
        public DateTime PaidOn { get; set; }
    }
}
