namespace HrApp.MVC.Models.Expense
{
    public class CreateExpenseViewModel
    {
        public string AppUserId { get; set; }
        public int ExpenseTypeId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public IFormFile File { get; set; }
    }
}
