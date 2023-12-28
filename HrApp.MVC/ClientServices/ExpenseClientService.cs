using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Expense;

namespace HrApp.MVC.ClientServices
{
    public class ExpenseClientService 
    {

        private readonly HttpClient _httpClient;
        public ExpenseClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }


        public async Task<List<ReadExpenseViewModel>> GetExpenses()
        {
            var response = await _httpClient.GetAsync("Expense");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<ReadExpenseViewModel>>>();
            return result.Data;
        }

        public async Task<List<ExpenseTypeViewModel>> GetExpenseTypes()
        {
            var response = await _httpClient.GetAsync("Expense/Types");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<ExpenseTypeViewModel>>>();
            return result.Data;
        }

        public async Task<JsonResponse<ReadExpenseViewModel>> GetExpense(int id)
        {
            var response = await _httpClient.GetAsync($"Expense/{id}");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<ReadExpenseViewModel>>();
            return result;
        }

        public async Task<JsonResponse<int>> CreateExpense(CreateExpenseViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Expense", model);
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }

        public async Task<JsonResponse<int>> UpdateExpense(UpdateExpenseViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync("Expense", model);
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }

        public async Task<JsonResponse<int>> DeleteExpense(int id)
        {
            var response = await _httpClient.DeleteAsync($"Expense/{id}");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }

    }
}
