namespace ManvarFitness.Entity
{
    public class UserConcern
    {
        public long UserConcernId { get; set; }
        public Guid UserId { get; set; }
        public int ConcernId { get; set; }
        public int? SubConcernId { get; set; }
        public string FormData { get; set; } = string.Empty;
        public long PaymentId { get; set; }
        public string? Status { get; set; } 
        public bool IsActive { get; set; } = true;
    }
}
