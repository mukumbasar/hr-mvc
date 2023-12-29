using FluentValidation;
using HrApp.MVC.Models.Advance;

namespace HrApp.MVC.Validator
{
    public class UpdateAdvanceViewModelValidator : AbstractValidator<UpdateAdvanceViewModel>
    {
        public UpdateAdvanceViewModelValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount is required").GreaterThanOrEqualTo(1).WithMessage("Amount must be greater than 0.");
            RuleFor(x => x.AdvanceTypeId).NotEmpty().WithMessage("Advance type is required.");
            RuleFor(x => x.CurrencyId).NotEmpty().WithMessage("Currency is required.");
            RuleFor(x => x.RequestDate).NotEmpty().WithMessage("Request date is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.").MaximumLength(100).WithMessage("Description must be maximum of 100 characters.").MinimumLength(5).WithMessage("Description must be minimum of 5 characters.");
        }
    }
}
