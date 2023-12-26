using System.ComponentModel.DataAnnotations;
using HrApp.MVC.CustomAttributes;

namespace HrApp.MVC.Models
{
    public class AppUserUpdateViewModel
    {
        public string Id { get; set; }
        [MaxLength(200, ErrorMessage = "Address must be maximum of 200 characters.")]
        [MinLength(15, ErrorMessage = "Address must be minimum of 15 characters.")]
        public string Address { get; set; }
        [MaxLength(11, ErrorMessage = "Mobile number must be 11 digits")]
        public string MobileNumber { get; set; }

        [CheckExtension(new string[] { ".png", ".jpeg", ".jpg" })]
        public IFormFile NewImage { get; set; }

        public byte[]? UpdatedImage { get; set; }
    }
}
