using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Expense;
using Newtonsoft.Json;
using System.Text;

namespace HrApp.MVC.ClientServices
{
    public class ExpenseClientService 
    {

        private readonly HttpClient _httpClient;
        public ExpenseClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }


        public async Task<List<ExpenseTypeViewModel>> GetExpenseTypes()
        {
            var response = await _httpClient.GetAsync("expense/Types");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<ExpenseTypeViewModel>>>();
            return result.Data;
        }
        public async Task<List<ReadExpenseViewModel>> GetExpenses()
        {
            var response = await _httpClient.GetAsync("expense");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<ReadExpenseViewModel>>>();
            return result.Data;
        }
        public async Task<JsonResponse<UpdateExpenseViewModel>> GetExpense(int id)
        {
            var response = await _httpClient.GetAsync($"expense/{id}");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<UpdateExpenseViewModel>>();
            return result;
        }

        public async Task<JsonResponse<int>> CreateExpense(CreateExpenseViewModel model)
        {
            var response = await _httpClient.PostAsync("expense", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }

        public async Task<JsonResponse<int>> UpdateExpense(UpdateExpenseViewModel model)
        {
            var response = await _httpClient.PutAsync("expense", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }

        public async Task<JsonResponse<int>> DeleteExpense(int id)
        {
            var response = await _httpClient.DeleteAsync($"expense/{id}");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }

    }
}
