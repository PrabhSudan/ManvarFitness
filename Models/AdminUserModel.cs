using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Models
{
    public class AdminUserModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Email or username is required.")]
        public string? EmailUsername { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        // Filled AFTER login (profile update)
        public string CountryCode { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;
    }
}
