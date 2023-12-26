using System.Data;
using FluentValidation;
using HrApp.MVC.Models;

namespace HrApp.MVC.Validator
{
    public class AppUserUpdateViewModelValidator : AbstractValidator<AppUserUpdateViewModel>
    {
        public AppUserUpdateViewModelValidator()
        {
            RuleFor(y => y.Address).NotEmpty().WithMessage("Please enter your address.").MaximumLength(200).WithMessage("Address must be maximum of 200 characters.").MinimumLength(15).WithMessage("Address must be minimum of 15 characters.");
            RuleFor(y => y.MobileNumber).NotEmpty().WithMessage("Please enter your mobile phone number.").Length(11, 13).WithMessage("Please enter a valid phone number.");
            RuleFor(y => y.NewImage).NotEmpty().WithMessage("Please choose a photo.");
        }
    }
}
