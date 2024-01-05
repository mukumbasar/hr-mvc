using FluentValidation;
using HrApp.MVC.Models.Personnel;

namespace HrApp.MVC.Validator
{
    public class ForgetPassViewModelValidator : AbstractValidator<ForgetPassViewModel>
    {
        public ForgetPassViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Enter a valid email.");
        }
    }
}
