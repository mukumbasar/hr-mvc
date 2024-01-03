using System.ComponentModel.DataAnnotations;

namespace HrApp.MVC.Models.Personnel
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 20 characters")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
