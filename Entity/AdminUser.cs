using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManvarFitness.Entity
{

    public class AdminUser:BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        // Filled AFTER login (profile update)
        public string? Role  {get; set; }
        [Required]
        public string? CountryCode { get; set; }
        [Required]
        public string? Mobile { get; set; } 
        public bool IsActive { get; set; } 
    }
}
