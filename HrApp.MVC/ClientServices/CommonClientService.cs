using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Common;
using HrApp.MVC.Models.Expense;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrApp.MVC.ClientServices
{
    public class CommonClientService
    {

        private readonly HttpClient _httpClient;
        private readonly ValidationService validationService;

        public CommonClientService(IHttpClientFactory httpClientFactory, ValidationService validationService)
        {
            _httpClient = httpClientFactory.CreateClient("api");
            this.validationService = validationService;
        }

        public async Task<List<SelectListItem>> GetCurrencies() =>
             validationService.ProcessResponse<JsonResponse<List<CurrencyViewModel>>>(await _httpClient.GetAsync("Common/Currency")).Result.Data.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

    }
}
