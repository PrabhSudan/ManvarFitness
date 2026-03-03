namespace ManvarFitness.Entity
{
    public class BaseEntity
    {
         public DateTime CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }      // <-- change to Guid?
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }      // <-- change to Guid?
        public bool IsDeleted { get; set; }
    }
}
