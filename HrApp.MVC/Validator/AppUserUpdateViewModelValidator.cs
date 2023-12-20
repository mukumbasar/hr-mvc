using FluentValidation;
using HrApp.MVC.Models;

namespace HrApp.MVC.Validator
{
    public class AppUserUpdateViewModelValidator : AbstractValidator<AppUserUpdateViewModel>
    {
        public AppUserUpdateViewModelValidator()
        {
            RuleFor(y => y.Address).NotEmpty().WithMessage("Please enter your address.");
            RuleFor(y => y.MobileNumber).NotEmpty().WithMessage("Please enter your mobile phone number.");
            RuleFor(y => y.UpdatedImage).NotEmpty().WithMessage("Please choose a photo.");
        }
    }
}
