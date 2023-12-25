namespace HrApp.MVC.Models.Advance
{
    public class CreateAdvanceViewModel
    {
        public string AppUserId { get; set; }
        public int AdvanceTypeId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public DateTime RequestDate { get; set; }
        public string Description { get; set; }
    }
}
