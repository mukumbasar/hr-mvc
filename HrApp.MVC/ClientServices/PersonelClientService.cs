using System.Net.Http;
using System.Text;
using HrApp.MVC.Helpers;
using HrApp.MVC.Models;
using Newtonsoft.Json;

namespace HrApp.MVC;

public class PersonelClientService
{
    private readonly HttpClient _httpClient;
    public PersonelClientService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("api");
    }
    public async Task<AppUserHomeViewModel> GetAppUserHomeViewModelAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"User/{userId}");

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var responseData = response.Content.ReadAsStringAsync().Result;

            var userHomeModel = JsonConvert.DeserializeObject<AppUserHomeViewModel>(responseData);

            return userHomeModel;
        }

        return null;
    }
    public async Task<AppUserDetailViewModel> GetAppUserDetailViewModelAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"User/details/{userId}");

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var responseData = response.Content.ReadAsStringAsync().Result;

            var userDetailModel = JsonConvert.DeserializeObject<AppUserDetailViewModel>(responseData);

            return userDetailModel;
        }

        return null;
    }
    public async Task<AppUserUpdateViewModel> GetAppUserUpdateAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"User/details/{userId}");

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var responseData = response.Content.ReadAsStringAsync().Result;

            var userDetailModel = JsonConvert.DeserializeObject<AppUserUpdateViewModel>(responseData);

            return userDetailModel;
        }

        return null;
    }
    public async Task<bool> UpdateAppUserUpdateViewModelAsync(AppUserUpdateViewModel appUserUpdateViewModel)
    {
        var bytes = await ImageConversions.ConvertToByteArrayAsync(appUserUpdateViewModel.NewImage);

        appUserUpdateViewModel.UpdatedImage = bytes;

        var response = await _httpClient.PutAsJsonAsync($"User", appUserUpdateViewModel);

        if (response.IsSuccessStatusCode)
        {

            return true;
        }

        return false;
    }
}
