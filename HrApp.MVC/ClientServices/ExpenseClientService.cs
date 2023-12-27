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


        public async Task<Response<List<ReadExpenseViewModel>>> GetExpenses()
        {
            var response = await _httpClient.GetAsync("Expense");
            var result = await response.Content.ReadFromJsonAsync<Response<List<ReadExpenseViewModel>>>();
            return result;
        }

        public async Task<Response<ReadExpenseViewModel>> GetExpense(int id)
        {
            var response = await _httpClient.GetAsync($"Expense/{id}");
            var result = await response.Content.ReadFromJsonAsync<Response<ReadExpenseViewModel>>();
            return result;
        }

        public async Task<Response<int>> CreateExpense(CreateExpenseViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Expense", model);
            var result = await response.Content.ReadFromJsonAsync<Response<int>>();
            return result;
        }

        public async Task<Response<int>> UpdateExpense(UpdateExpenseViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync("Expense", model);
            var result = await response.Content.ReadFromJsonAsync<Response<int>>();
            return result;
        }

        public async Task<Response<int>> DeleteExpense(int id)
        {
            var response = await _httpClient.DeleteAsync($"Expense/{id}");
            var result = await response.Content.ReadFromJsonAsync<Response<int>>();
            return result;
        }

    }
}
