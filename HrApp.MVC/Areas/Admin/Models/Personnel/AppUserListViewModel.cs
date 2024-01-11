namespace HrApp.MVC.Areas.Admin.Models.Personnel
{
    public class AppUserListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public string? SecondSurname { get; set; }
        public DateTime BirthYear { get; set; }
        public string BirthPlace { get; set; }
        public string TurkishIdentificationNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get { return EndDate == null ? true : false; } }
        public string Department { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public decimal Salary { get; set; }
        public byte[]? ImageData { get; set; }
        public int YearlyLeaveDaysLeft { get; set; }
        public decimal YearlyAdvanceAmountLeft { get; set; }
        public int GenderId { get; set; }
        public string Gender { get; set; }
    }
}
