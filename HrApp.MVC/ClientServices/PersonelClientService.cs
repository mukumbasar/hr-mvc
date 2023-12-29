using System.Net.Http;
using System.Text;
using HrApp.MVC.Helpers;
using HrApp.MVC.Models;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace HrApp.MVC;

public class PersonelClientService
{
    private readonly AppUserUpdateViewModelValidator updateValidator;
    private readonly ValidationService validationService;
    private readonly HttpClient _httpClient;
    public PersonelClientService(IHttpClientFactory httpClientFactory, AppUserUpdateViewModelValidator updateValidator, ValidationService validationService)
    {
        _httpClient = httpClientFactory.CreateClient("api");
        this.updateValidator = updateValidator;
        this.validationService = validationService;
    }
    public async Task<JsonResponse<AppUserHomeViewModel>> GetAppUserHomeViewModelAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"User/{userId}");

        return await validationService.ProcessResponse<JsonResponse<AppUserHomeViewModel>>(response);
    }
    public async Task<JsonResponse<AppUserDetailViewModel>> GetAppUserDetailViewModelAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"User/details/{userId}");

        return await validationService.ProcessResponse<JsonResponse<AppUserDetailViewModel>>(response);
    }
    public async Task<JsonResponse<AppUserUpdateViewModel>> GetAppUserUpdateAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"User/details/{userId}");

        return await validationService.ProcessResponse<JsonResponse<AppUserUpdateViewModel>>(response);
    }
    public async Task<JsonResponse<string>> UpdateAppUserUpdateViewModelAsync(AppUserUpdateViewModel appUserUpdateViewModel, ModelStateDictionary ModelState)
    {
        var validationResult = validationService.ModelValidator(appUserUpdateViewModel, updateValidator, ModelState);
        if (!validationResult.IsSuccess)
            return JsonResponse<string>.Failure(validationResult.Message);

        var bytes = await ImageConversions.ConvertToByteArrayAsync(appUserUpdateViewModel.NewImage);

        appUserUpdateViewModel.UpdatedImage = bytes;

        var response = await _httpClient.PutAsJsonAsync($"User", appUserUpdateViewModel);
        return await validationService.ProcessResponse<JsonResponse<string>>(response);
    }
}
