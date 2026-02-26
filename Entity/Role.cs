using System.ComponentModel.DataAnnotations.Schema;

namespace ManvarFitness.Entity
{

    public class Role :BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public bool IsActive { get; set; } = true;
    }
}
