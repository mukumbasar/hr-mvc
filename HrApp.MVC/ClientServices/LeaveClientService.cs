using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Expense;
using HrApp.MVC.Models.Leave;
using Newtonsoft.Json;
using System.Text;

namespace HrApp.MVC.ClientServices
{
    public class LeaveClientService
    {

        private readonly HttpClient _httpClient;
        public LeaveClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task<List<ReadLeaveViewModel>> GetLeaves()
        {
            var response = await _httpClient.GetAsync("leave");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<ReadLeaveViewModel>>>();
            return result.Data;
        }

        public async Task<List<LeaveTypeViewModel>> GetLeaveTypes()
        {
            var response = await _httpClient.GetAsync("leave/Types");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<LeaveTypeViewModel>>>();
            return result.Data;
        }

        public async Task<JsonResponse<ReadLeaveViewModel>> GetLeave(int id)
        {
            var response = await _httpClient.GetAsync($"leave/{id}");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<ReadLeaveViewModel>>();
            return result;
        }

        public async Task<JsonResponse<int>> CreateLeave(CreateLeaveViewModel model)
        {
            var response = await _httpClient.PostAsync("leave", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }

        public async Task<JsonResponse<int>> UpdateLeave(UpdateLeaveViewModel model)
        {
            var response = await _httpClient.PutAsync("leave", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }

        public async Task<JsonResponse<int>> DeleteLeave(int id)
        {
            var response = await _httpClient.DeleteAsync($"leave/{id}");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }
    }
}
