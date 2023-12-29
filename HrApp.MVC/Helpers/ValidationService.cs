using FluentValidation;
using HrApp.MVC.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HrApp.MVC;

public class ValidationService
{
    public JsonResponse<T> Validate<T>(T model, IValidator<T> validator, ModelStateDictionary? modelState)
    {
        var validationResult = validator.Validate(model);
        if (validationResult.IsValid)
            return JsonResponse<T>.Success(model);
        foreach (var error in validationResult.Errors)
        {
            modelState?.AddModelError(error.PropertyName, error.ErrorMessage);
        }
        var tempError = string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage));
        return JsonResponse<T>.Failure(tempError);
    }
}
