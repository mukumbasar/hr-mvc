using HrApp.MVC.CustomAttributes;

namespace HrApp.MVC.Models.Expense
{
    public class ReadExpenseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public string? SecondSurname { get; set; }
        public string ExpenseTypeName { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ApprovalStatus { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string AppUserId { get; set; }
    }
}
