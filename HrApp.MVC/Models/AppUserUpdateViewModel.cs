using HrApp.MVC.CustomAttributes;

namespace HrApp.MVC.Models
{
    public class AppUserUpdateViewModel
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }

        [CheckExtension(new string[] { ".png", ".jpeg", ".jpg" })]
        public IFormFile NewImage { get; set; }

        public byte[]? UpdatedImage { get; set; }
    }
}
