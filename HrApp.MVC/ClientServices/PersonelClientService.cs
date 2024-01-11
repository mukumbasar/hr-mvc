using HrApp.MVC.Areas.Admin.Models.Personnel;
using HrApp.MVC.Helpers;
using HrApp.MVC.Models;
using HrApp.MVC.Models.Advance;
using HrApp.MVC.Models.Personnel;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HrApp.MVC;

public class PersonelClientService
{
    private readonly AppUserUpdateViewModelValidator updateValidator;
    private readonly AppUserAddViewModelValidator addValidator;
    private readonly ChangePasswordViewModelValidator ChangePasswordValidator;
    private readonly ValidationService validationService;
    private readonly HttpClient _httpClient;
    public PersonelClientService(IHttpClientFactory httpClientFactory, AppUserUpdateViewModelValidator updateValidator, ValidationService validationService, AppUserAddViewModelValidator addValidator, ChangePasswordViewModelValidator ChangePasswordValidator)
    {
        _httpClient = httpClientFactory.CreateClient("api");
        this.updateValidator = updateValidator;
        this.validationService = validationService;
        this.addValidator = addValidator;
        this.ChangePasswordValidator = ChangePasswordValidator;
    }
    public async Task<JsonResponse<List<AppUserListViewModel>>> GetAppUserAsync() =>
            await validationService.ProcessResponse<JsonResponse<List<AppUserListViewModel>>>(await _httpClient.GetAsync("User/list"));
    public async Task<JsonResponse<AppUserListViewModel>> GetAppUserAsync(string userId) =>
        await validationService.ProcessResponse<JsonResponse<AppUserListViewModel>>(await _httpClient.GetAsync($"User/list/{userId}"));
    public async Task<JsonResponse<AppUserViewModel>> GetAppUserHomeViewModelAsync(string userId) =>
        await validationService.ProcessResponse<JsonResponse<AppUserViewModel>>(await _httpClient.GetAsync($"User/{userId}"));

    public async Task<JsonResponse<AppUserDetailViewModel>> GetAppUserDetailViewModelAsync(string userId) =>
        await validationService.ProcessResponse<JsonResponse<AppUserDetailViewModel>>(await _httpClient.GetAsync($"User/details/{userId}"));

    public async Task<JsonResponse<AppUserUpdateViewModel>> GetAppUserUpdateAsync(string userId) =>
        await validationService.ProcessResponse<JsonResponse<AppUserUpdateViewModel>>(await _httpClient.GetAsync($"User/details/{userId}"));
    public async Task<JsonResponse<string>> UpdateAppUserUpdateViewModelAsync(AppUserUpdateViewModel appUserUpdateViewModel, ModelStateDictionary ModelState)
    {
        return await validationService.ExecuteValidatedRequestAsync<AppUserUpdateViewModel, string>(
            appUserUpdateViewModel,
            updateValidator,
            ModelState,
            async () =>
            {
                appUserUpdateViewModel.UpdatedImage = await ImageConversions.ConvertToByteArrayAsync(appUserUpdateViewModel.NewImage);
                return await _httpClient.PutAsJsonAsync("User", appUserUpdateViewModel);
            });
    }

    public async Task<JsonResponse<string>> AddAppUserAddViewModelAsync(AppUserAddViewModel appUserAddViewModel, ModelStateDictionary ModelState, bool isAdmin)
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
            return await _httpClient.PostAsJsonAsync("User", appUserAddViewModel);
        });
    }
    public async Task<JsonResponse<string>> ChangePasswordAppUserViewModelAsync(AppUserPasswordChangeApiViewModel viewModel, ModelStateDictionary ModelState) =>
     await validationService.ExecuteValidatedRequestAsync<AppUserPasswordChangeApiViewModel, string>(
        viewModel,
        ChangePasswordValidator,
        ModelState,
        async () =>
        {
            return await _httpClient.PutAsJsonAsync("User/PasswordChange", viewModel);
        });
}
