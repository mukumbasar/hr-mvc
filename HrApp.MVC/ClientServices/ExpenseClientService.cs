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
            var result = await response.Content.ReadFromJsonAsync<List<ReadExpenseViewModel>>();
            return result;
        }

        public async Task<ReadExpenseViewModel> GetExpense(int id)
        {
            var response = await _httpClient.GetAsync($"Expense/{id}");
            var result = await response.Content.ReadFromJsonAsync<ReadExpenseViewModel>();
            return result;
        }

        public async Task<bool> CreateExpense(CreateExpenseViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Expense", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateExpense(UpdateExpenseViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync("Expense", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteExpense(int id)
        {
            var response = await _httpClient.DeleteAsync($"Expense/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
