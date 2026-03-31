namespace ManvarFitness.Models
{
    public class DietPlanPdfModel
    {
        public string UserName { get; set; }
        public string PlanName { get; set; }
        public int ConcernId { get; set; }
        public List<DietDayModel> Days { get; set; }
    }
}
