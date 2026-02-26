namespace ManvarFitness.Entity
{
    public class User :BaseEntity
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
        public string? Mobile { get; set; }
        public DateOnly? DOB { get; set; }
        public string? Gender { get; set; }
        public int? HeightFeet { get; set; }
        public int? HeightInch { get; set; }
        public double? WeightKg { get; set; }
        public bool IsActive { get; set; }
    }
}
