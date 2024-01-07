using HrApp.MVC.CustomAttributes;

namespace HrApp.MVC.Areas.Admin.Models.Personnel
{
    public class AppUserAddViewModel
    {
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public string? SecondSurname { get; set; }
        public DateTime BirthYear { get; set; }
        public string BirthPlace { get; set; }
        public string TurkishIdentificationNumber { get; set; }
        public DateTime StartDate { get; set; }
        public string Department { get; set; }
        public string CompanyName { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public decimal Salary { get; set; }
        [CheckExtension(new string[] { ".png", ".jpeg", ".jpg" })]
        [CheckSize(1 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is {0} bytes")]
        public IFormFile NewImage { get; set; }
        public byte[]? ImageData { get; set; }
        public int GenderId { get; set; }
    }
}
