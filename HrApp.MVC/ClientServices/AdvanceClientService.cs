using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Advance;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HrApp.MVC.ClientServices
{
    public class AdvanceClientService
    {
        private readonly HttpClient _httpClient;
        public AdvanceClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }
        public async Task<List<ReadAdvanceViewModel>> GetAdvances()
        {
            var response = await _httpClient.GetAsync("Advance");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<ReadAdvanceViewModel>>>();
            return result.Data;
        }
        public async Task<List<ReadAdvanceViewModel>> GetAdvance(int id)
        {
            var response = await _httpClient.GetAsync($"advance/{id}");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<ReadAdvanceViewModel>>>();
            return result.Data;
        }

        public async Task<List<AdvanceTypeViewModel>> GetAdvanceTypes()
        {
            var response = await _httpClient.GetAsync($"Advance/Types");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<AdvanceTypeViewModel>>>();
            return result.Data;
        }

        public async Task<JsonResponse<int>> CreateAdvance(CreateAdvanceViewModel createAdvanceViewModel)
        {
            var response = await _httpClient.PostAsync("Advance", new StringContent(JsonConvert.SerializeObject(createAdvanceViewModel),Encoding.UTF8,"application/json"));
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }
        public async Task<JsonResponse<int>> UpdateAdvance(UpdateAdvanceViewModel updateAdvanceViewModel)
        {
            var response = await _httpClient.PutAsync("advance", new StringContent(JsonConvert.SerializeObject(updateAdvanceViewModel),Encoding.UTF8,"application/json"));
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }
        public async Task<JsonResponse<int>> DeleteAdvance(int id)
        {
            var response = await _httpClient.DeleteAsync($"advance/{id}");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }
    }
}
