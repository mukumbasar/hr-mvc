using FluentValidation;
using HrApp.MVC.Models;

namespace HrApp.MVC.Validator
{
    public class AppUserAddViewModelValidator:AbstractValidator<AppUserAddViewModel>
    {
        public AppUserAddViewModelValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x=>x.Surname).NotEmpty().WithMessage("Surname is required.");
            RuleFor(x=>x.BirthYear).NotEmpty().WithMessage("Birth year is required.");
            RuleFor(x=>x.BirthPlace).NotEmpty().WithMessage("Birth place is required.");
            RuleFor(x=>x.TurkishIdentificationNumber).NotEmpty().WithMessage("Turkish identification number is required.").Length(11).WithMessage("Turkish identification number must be 11 character.");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required.");
            RuleFor(x => x.Department).NotEmpty().WithMessage("Department is required.");
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company name is required.");
            RuleFor(x => x.Occupation).NotEmpty().WithMessage("Occupation is required.");
            RuleFor(y => y.Address).NotEmpty().WithMessage("Please enter your address.").MaximumLength(200).WithMessage("Address must be maximum of 200 characters.").MinimumLength(15).WithMessage("Address must be minimum of 15 characters.");
            RuleFor(y => y.MobileNumber).NotEmpty().WithMessage("Please enter your mobile phone number.").Length(9, 11).WithMessage("Please enter a valid phone number. Like 5325323232");
            RuleFor(x => x.Salary).NotEmpty().WithMessage("Salary is required.");
            RuleFor(y => y.NewImage).NotEmpty().WithMessage("Please choose a photo.");
            RuleFor(y => y.GenderId).NotEmpty().WithMessage("Gender is required.");
        }
    }
}
