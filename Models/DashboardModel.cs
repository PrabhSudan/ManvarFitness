using ManvarFitness.Entity;

namespace ManvarFitness.Models
{
    public class DashboardModel
    {
        public int Customers { get; set; }
        public int DietPlans { get; set; }
        public int WorkoutVideos { get; set; }
        public int Results { get; set; }
        public List<User> AllCustomers { get; set; }
    }
}
