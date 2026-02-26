namespace ManvarFitness.Models
{
    public class UserModel
    {
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
        public string? Mobile { get; set; }
        public DateOnly DOB { get; set; }
        public string? Gender { get; set; }
        public int HeightFeet { get; set; }
        public int HeightInch { get; set; }
        public double WeightKg { get; set; }
    }
}
