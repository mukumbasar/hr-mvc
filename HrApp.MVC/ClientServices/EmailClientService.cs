using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Personnel;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HrApp.MVC.ClientServices
{
    public class EmailClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ForgetPassViewModelValidator forgetPassViewModelValidator;
        private readonly ValidationService _validationService;

        public EmailClientService(IHttpClientFactory httpClientFactory, ValidationService validationService)
        {
            _httpClient = httpClientFactory.CreateClient("api");
            _validationService = validationService;
        }

        public async Task<JsonResponse<string>> SendEmail(ForgetPassViewModel forgetPassViewModel, ModelStateDictionary ModelState)
        {
            return await _validationService.ExecuteValidatedRequestAsync<ForgetPassViewModel, string>(
                forgetPassViewModel,
                forgetPassViewModelValidator,
                ModelState,
                async () =>
                {
                    return await _httpClient.GetAsync($"User/PasswordEmail/{forgetPassViewModel.Email}");
                });
        }
    }
}
