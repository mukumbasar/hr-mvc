using HrApp.MVC.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HrApp.MVC.Areas.Admin.Models.Company
{
    public class AddCompanyViewModel
    {

        public string Name { get; set; }
        [MinLength(16), MaxLength(16)]
        public string MersisNo { get; set; }
        [MaxLength(10)]
        public string TaxNo { get; set; }
        public string TaxOffice { get; set; }
        [CheckExtension(new string[] { ".png", ".jpeg", ".jpg" })]
        [CheckSize(1 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is {0} bytes")]
        public IFormFile File { get; set; }
        public byte[]? ImageData { get; set; }
        [MinLength(11)]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public int EmployeeCount { get; set; }
        public DateTime FoundationYear { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public int CompanyTypeId { get; set; }
    }
}
