using HrApp.MVC.Areas.Admin.Models.Personnel;
using HrApp.MVC.Helpers;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HrApp.MVC.ClientServices
{
    public class CompanyManagerClientService
    {
        private readonly AppUserUpdateViewModelValidator updateValidator;
        private readonly AppUserAddViewModelValidator addValidator;
        private readonly ChangePasswordViewModelValidator ChangePasswordValidator;
        private readonly ValidationService validationService;
        private readonly HttpClient _httpClient;

        public CompanyManagerClientService(IHttpClientFactory httpClientFactory, AppUserUpdateViewModelValidator updateValidator, ValidationService validationService, AppUserAddViewModelValidator addValidator, ChangePasswordViewModelValidator ChangePasswordValidator)
        {
            _httpClient = httpClientFactory.CreateClient("api");
            this.updateValidator = updateValidator;
            this.validationService = validationService;
            this.addValidator = addValidator;
            this.ChangePasswordValidator = ChangePasswordValidator;
        }
        public async Task<JsonResponse<List<AppUserListViewModel>>> GetCompanyManagerAsync() =>
                await validationService.ProcessResponse<JsonResponse<List<AppUserListViewModel>>>(await _httpClient.GetAsync("Admin/list"));
    
    public async Task<JsonResponse<string>> AddCompanyManagerAsync(AppUserAddViewModel appUserAddViewModel, ModelStateDictionary ModelState, bool isAdmin)
    {
        return await validationService.ExecuteValidatedRequestAsync<AppUserAddViewModel, string>(
            appUserAddViewModel,
            addValidator,
            ModelState,
            async () =>
            {
                appUserAddViewModel.IsAdmin = isAdmin;
                appUserAddViewModel.ImageData = await
                ImageConversions.ConvertToByteArrayAsync
                (appUserAddViewModel.NewImage);
                return await _httpClient.PostAsJsonAsync("Admin", appUserAddViewModel);
            });
    }
    public async Task<JsonResponse<string>> ChangePasswordCompanyManagerAsync(AppUserPasswordChangeApiViewModel viewModel, ModelStateDictionary ModelState) =>
     await validationService.ExecuteValidatedRequestAsync<AppUserPasswordChangeApiViewModel, string>(
        viewModel,
        ChangePasswordValidator,
        ModelState,
        async () =>
        {
            return await _httpClient.PutAsJsonAsync("Admin/PasswordChange", viewModel);
        });
    }
}
