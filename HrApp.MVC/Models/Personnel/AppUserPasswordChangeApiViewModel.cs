using System.ComponentModel.DataAnnotations;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace HrApp.MVC;

public class AppUserPasswordChangeApiViewModel
{
    public string Id { get; set; }
    public string Token { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Password must be equal")]
    public string ConfirmPassword { get; set; }
}
