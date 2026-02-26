namespace ManvarFitness.Entity
{
    public class HerbCategory : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
