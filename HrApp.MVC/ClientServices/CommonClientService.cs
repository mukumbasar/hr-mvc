using HrApp.MVC.Models.Common;
using HrApp.MVC.Models.Expense;

namespace HrApp.MVC.ClientServices
{
    public class CommonClientService
    {

        private readonly HttpClient _httpClient;
        public CommonClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task<List<CurrencyViewModel>> GetCurrencies()
        {
            var response = await _httpClient.GetAsync("Common/Currency");
            var result = await response.Content.ReadFromJsonAsync<Response<List<CurrencyViewModel>>>();
            return result.Data;
        }
    }
}
