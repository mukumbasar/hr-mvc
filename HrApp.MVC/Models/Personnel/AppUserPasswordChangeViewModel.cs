using System.ComponentModel.DataAnnotations;

namespace HrApp.MVC.Models.Personnel;

public class AppUserPasswordChangeViewModel
{
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
