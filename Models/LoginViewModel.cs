using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email or username is required.")]
        public string? EmailUsername { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
