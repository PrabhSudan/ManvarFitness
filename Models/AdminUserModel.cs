using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Models
{
    public class AdminUserModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string? Email{ get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        // Filled AFTER login (profile update)
        public string CountryCode { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;
    }
}
