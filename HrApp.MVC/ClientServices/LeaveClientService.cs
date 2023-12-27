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

        public async Task<IEnumerable<ReadLeaveViewModel>> GetLeaves()
        {
            var response = await _httpClient.GetAsync("leave");
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<ReadLeaveViewModel>>();
            return result;
        }

        public async Task<ReadLeaveViewModel> GetLeave(int id)
        {
            var response = await _httpClient.GetAsync($"leave/{id}");
            var result = await response.Content.ReadFromJsonAsync<ReadLeaveViewModel>();
            return result;
        }

        public async Task<bool> CreateLeave(CreateLeaveViewModel model)
        {
            var response = await _httpClient.PostAsync("leave", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateLeave(UpdateLeaveViewModel model)
        {
            var response = await _httpClient.PutAsync("leave", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteLeave(int id)
        {
            var response = await _httpClient.DeleteAsync($"leave/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
