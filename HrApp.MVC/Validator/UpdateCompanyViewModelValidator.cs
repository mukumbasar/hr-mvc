using FluentValidation;
using HrApp.MVC.Areas.Admin.Models.Company;
using System.Reflection;

namespace HrApp.MVC.Validator
{
    public class UpdateCompanyViewModelValidator : AbstractValidator<UpdateCompanyViewModel>
    {
        public UpdateCompanyViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Company Name is required.");
            RuleFor(x => x.TaxNo).NotEmpty().WithMessage("Tax Number is required.");
            RuleFor(x => x.TaxOffice).NotEmpty().WithMessage("Tax Office is required.");
            RuleFor(x => x.File).NotEmpty().WithMessage("Please choose a file.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number is required.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.")
                    .MaximumLength(200).WithMessage("Address must be a maximum of 200 characters.")
                    .MinimumLength(15).WithMessage("Address must be a minimum of 15 characters.");
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email Address is required.")
                    .EmailAddress().WithMessage("Invalid email address.");
            //RuleFor(x => x.EmployeeCount).NotEmpty().WithMessage("Employee Count is required.")
            //    .GreaterThan(0).WithMessage("Employee Count must be greater than 0.");
            RuleFor(x => x.FoundationYear).NotEmpty().WithMessage("Foundation Year is required.")
                    .LessThan(DateTime.Now).WithMessage("Foundation Year must be in the past.");
            RuleFor(x => x.ContractStartDate).NotEmpty().WithMessage("Contract Start Date is required.");
            RuleFor(x => x.ContractEndDate).NotEmpty().WithMessage("Contract End Date is required.")
                    .GreaterThan(x => x.ContractStartDate).WithMessage("Contract End Date must be greater than Contract Start Date.");
            RuleFor(x => x.CompanyTypeId).NotEmpty().WithMessage("Company Type is required.");
            RuleFor(x => x.MersisNo).NotEmpty().WithMessage("Mersis Number is required.").Must((model, mersisNo) => IsAValidMersisNo(mersisNo, model.CompanyTypeId, model.TaxNo)).WithMessage("Invalid MERSİS Number.");

        }

        private bool IsAValidMersisNo(string mersisNo, int ctypeid, string taxno)
        {
            if (string.IsNullOrEmpty(mersisNo) || mersisNo.Length != 16)
            {
                return false;
            }

            if (ctypeid == 4 || ctypeid == 5 || ctypeid == 6) // şahıslar
            {
                return mersisNo.EndsWith("00015") || mersisNo.EndsWith("00019");
            }
            else if (ctypeid == 1 || ctypeid == 2) // anonim ve limited
            {
                return mersisNo.StartsWith($"0{taxno}") &&
                       (mersisNo.EndsWith("00015") || mersisNo.EndsWith("00016") || mersisNo.EndsWith("00017"));
            }
            else
            {
                return true;
            }
        }

    }
}
