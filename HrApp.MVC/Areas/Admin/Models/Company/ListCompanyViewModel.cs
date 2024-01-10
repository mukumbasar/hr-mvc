namespace HrApp.MVC.Areas.Admin.Models.Company
{
    public class ListCompanyViewModel
    {
        public string Name { get; set; }
        public string MersisNo { get; set; }
        public string TaxNo { get; set; }
        public string TaxOffice { get; set; }
        public byte[]? ImageData { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public int EmployeeCount { get; set; }
        public DateTime FoundationYear { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public bool IsActive { get; set; }
        public string CompanyTypeName { get; set; }
    }
}
