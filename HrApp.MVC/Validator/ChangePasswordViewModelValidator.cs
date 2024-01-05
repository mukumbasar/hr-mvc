using FluentValidation;

namespace HrApp.MVC;

public class ChangePasswordViewModelValidator : AbstractValidator<AppUserPasswordChangeApiViewModel>
{
    public ChangePasswordViewModelValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("User not found");
        RuleFor(x => x.Token).NotEmpty().WithMessage("Token not found");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password can't be empty");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm Password can't be empty");

    }
}
