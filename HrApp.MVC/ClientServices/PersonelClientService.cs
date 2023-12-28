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
    private readonly HttpClient _httpClient;
    public PersonelClientService(IHttpClientFactory httpClientFactory, AppUserUpdateViewModelValidator updateValidator)
    {
        _httpClient = httpClientFactory.CreateClient("api");
        this.updateValidator = updateValidator;
    }
    public async Task<Response<AppUserHomeViewModel>> GetAppUserHomeViewModelAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"User/{userId}");

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var responseData = response.Content.ReadAsStringAsync().Result;

            var userHomeModel = JsonConvert.DeserializeObject<AppUserHomeViewModel>(responseData);

            return Response<AppUserHomeViewModel>.Success(userHomeModel, "");
        }

        return Response<AppUserHomeViewModel>.Failure("User not found");
    }
    public async Task<Response<AppUserDetailViewModel>> GetAppUserDetailViewModelAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"User/details/{userId}");

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var responseData = response.Content.ReadAsStringAsync().Result;

            var userDetailModel = JsonConvert.DeserializeObject<AppUserDetailViewModel>(responseData);

            return Response<AppUserDetailViewModel>.Success(userDetailModel, "");
        }

        return Response<AppUserDetailViewModel>.Failure("User not found");
    }
    public async Task<Response<AppUserUpdateViewModel>> GetAppUserUpdateAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"User/details/{userId}");

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var responseData = response.Content.ReadAsStringAsync().Result;

            var userDetailModel = JsonConvert.DeserializeObject<AppUserUpdateViewModel>(responseData);

            return Response<AppUserUpdateViewModel>.Success(userDetailModel, "");
        }

        return Response<AppUserUpdateViewModel>.Failure("User not found");
    }
    public async Task<Response<bool>> UpdateAppUserUpdateViewModelAsync(AppUserUpdateViewModel appUserUpdateViewModel, ModelStateDictionary ModelState)
    {
        if (updateValidator.Validate(appUserUpdateViewModel).IsValid == false)
        {
            PostValidationErrors.AddToModelState(updateValidator.Validate(appUserUpdateViewModel), ModelState);
            return Response<bool>.Failure("Validation error");
        }
        var bytes = await ImageConversions.ConvertToByteArrayAsync(appUserUpdateViewModel.NewImage);

        appUserUpdateViewModel.UpdatedImage = bytes;

        var response = await _httpClient.PutAsJsonAsync($"User", appUserUpdateViewModel);

        if (response.IsSuccessStatusCode)
        {
            return Response<bool>.Success(true);
        }

        return Response<bool>.Failure();
    }
}
