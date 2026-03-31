namespace ManvarFitness.Models
{
    public class UserDietPlanModel
    {
        public int UserDietPlanId { get; set; }
        public Guid UserId { get; set; }
        public int ConcernId { get; set; }
        public string? DietPlanName { get; set; }
        public string? DietPlanData { get; set; }
        public int Version { get; set; }
        public bool IsLatest { get; set; }
        public string? PdfUrl { get; set; }
        public DateOnly ValidTill { get; set; }
        public bool IsActive { get; set; }
    }
}
