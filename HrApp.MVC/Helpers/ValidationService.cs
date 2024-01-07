using FluentValidation;
using HrApp.MVC.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HrApp.MVC;

public class ValidationService
{
    public JsonResponse<T> ModelValidator<T>(T model, IValidator<T> validator, ModelStateDictionary modelState)
    {
        var validationResult = validator.Validate(model);
        if (validationResult.IsValid && modelState.IsValid)
            return JsonResponse<T>.Success(model);
        foreach (var error in validationResult.Errors)
        {
            modelState?.AddModelError(error.PropertyName, error.ErrorMessage);
        }
        var tempError = string.Join(" ", modelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
        return JsonResponse<T>.Failure(tempError);
    }
    public async Task<T> ProcessResponse<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            // Read the error response and include it in the failure message
            var errorContent = await response.Content.ReadFromJsonAsync<T>();
            return errorContent;
        }

        // Deserialize the response content to the specified type
        var content = await response.Content.ReadFromJsonAsync<T>();
        return content;
    }
    public async Task<JsonResponse<TResult>> ExecuteValidatedRequestAsync<TModel, TResult>(
    TModel model,
    IValidator<TModel>? validator,
    ModelStateDictionary? modelState,
    Func<Task<HttpResponseMessage>> requestFunc)
    {
        // First, perform model validation
        if (validator != null && modelState != null)
        {
            var validationResponse = ModelValidator(model, validator, modelState);
            if (!validationResponse.IsSuccess)
            {
                return JsonResponse<TResult>.Failure(validationResponse.Message);
            }
        }
        Thread.Sleep(1000);
        var httpResponse = await requestFunc();
        var responseContent = await ProcessResponse<JsonResponse<TResult>>(httpResponse);

        return responseContent;
    }

}
