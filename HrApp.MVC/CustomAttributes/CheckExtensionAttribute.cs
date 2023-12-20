using System.ComponentModel.DataAnnotations;

namespace HrApp.MVC.CustomAttributes
{
    public class CheckExtensionAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public CheckExtensionAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);

                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage(extension));
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage(string name)
        {
            return $"{name} extension is not allowed!";
        }
    }
}
