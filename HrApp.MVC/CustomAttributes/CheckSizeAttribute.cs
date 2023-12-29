using HrApp.MVC.Helpers;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;

namespace HrApp.MVC.CustomAttributes
{
    public class CheckSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public CheckSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var fileSize = ImageConversions.ConvertToByteArrayAsync(file).Result.Length;

                if(fileSize <= _maxFileSize)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(GetErrorMessage(_maxFileSize.ToString()));
        }

        public string GetErrorMessage(string message)
        {
            return base.FormatErrorMessage(message);
        }
    }
}
