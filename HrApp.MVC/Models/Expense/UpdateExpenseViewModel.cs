using HrApp.MVC.CustomAttributes;

namespace HrApp.MVC.Models.Expense
{
    public class UpdateExpenseViewModel
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int ExpenseTypeId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
        [CheckExtension(new string[] { ".png", ".jpeg", ".jpg", ".pdf" })]
        public IFormFile File { get; set; }

        public byte[]? Document { get; set; }
    }
}
