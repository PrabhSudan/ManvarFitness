using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManvarFitness.Entity
{

    public class AdminUser:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? EmailUsername { get; set; }
        public string? Password { get; set; }

        // Filled AFTER login (profile update)
        public string? Role  {get; set; }
        public string CountryCode { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;
        public bool IsActive { get; set; } 
    }
}
