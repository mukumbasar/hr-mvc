using FluentValidation;
using HrApp.MVC.Areas.Admin.Models.Personnel;

namespace HrApp.MVC.Validator
{
    public class AppUserAddViewModelValidator : AbstractValidator<AppUserAddViewModel>
    {
        public AppUserAddViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required.");
            RuleFor(x => x.BirthYear).NotEmpty().WithMessage("Birth year is required.");
            RuleFor(x => x.BirthPlace).NotEmpty().WithMessage("Birth place is required.");
            RuleFor(x => x.TurkishIdentificationNumber).NotEmpty().WithMessage("Turkish identification number is required.").Length(11).WithMessage("Turkish identification number must be 11 character.").Must(BeAValidTCKN).WithMessage("Geçersiz TC Kimlik Numarası."); ;
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
        private bool BeAValidTCKN(string tckn)
        {
            if (string.IsNullOrWhiteSpace(tckn))
                return false;

            if (tckn.Length != 11)
                return false;

            if (!tckn.All(char.IsDigit))
                return false;

            var digits = tckn.Select(digit => int.Parse(digit.ToString())).ToArray();

            int sumOfFirst10Digits = digits.Take(10).Sum();
            int sumOfFirst9Digits = digits.Take(9).Sum();
            int sumOfEvenDigits = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            int sumOfOddDigits = digits[1] + digits[3] + digits[5] + digits[7];

            bool isValidTenthDigit = ((sumOfEvenDigits * 7) - sumOfOddDigits) % 10 == digits[9];
            bool isValidEleventhDigit = sumOfFirst10Digits % 10 == digits[10];

            return isValidTenthDigit && isValidEleventhDigit;
        }
    }
}
