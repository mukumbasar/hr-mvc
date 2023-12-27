using FluentValidation;
using HrApp.MVC.Models.Expense;

namespace HrApp.MVC.Validator
{
    public class UpdateExpenseViewModelValidator : AbstractValidator<UpdateExpenseViewModel>
    {

        public UpdateExpenseViewModelValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount is required").LessThan(100000).WithMessage("Amount must be less then 1000000.").GreaterThanOrEqualTo(1).WithMessage("Amount must be greater then 0.");
            RuleFor(x => x.CurrencyId).NotEmpty().WithMessage("Currency is required");
            RuleFor(x => x.ExpenseTypeId).NotEmpty().WithMessage("Expense Type is required");
            RuleFor(x => x.File).NotEmpty().WithMessage("File is required");
        }
    }
}
