using System.ComponentModel.DataAnnotations;

namespace ManvarFitness.Models
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email or username is required.")]
        public string? EmailUsername { get; set; }
        [Required(ErrorMessage = "New password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [MinLength(6, ErrorMessage = "Confirm password must be at least 6 characters long.")]
        public string? ConfirmPassword { get; set; }

    }
}
