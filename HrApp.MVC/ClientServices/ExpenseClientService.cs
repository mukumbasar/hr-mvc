using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Expense;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HrApp.MVC.ClientServices
{
    public class ExpenseClientService
    {

        private readonly HttpClient _httpClient;
        private readonly UpdateExpenseViewModelValidator updateValidator;
        private readonly ValidationService validationService;
        private readonly CreateExpenseViewModelValidator createValidator;

        public ExpenseClientService(IHttpClientFactory httpClientFactory, UpdateExpenseViewModelValidator updateValidator, ValidationService validationService, CreateExpenseViewModelValidator createValidator)
        {
            _httpClient = httpClientFactory.CreateClient("api");
            this.updateValidator = updateValidator;
            this.validationService = validationService;
            this.createValidator = createValidator;
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

        public async Task<JsonResponse<UpdateExpenseViewModel>> GetExpense(int id)
        {
            var response = await _httpClient.GetAsync($"Expense/{id}");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<UpdateExpenseViewModel>>();
            return result;
        }

        public async Task<JsonResponse<int>> CreateExpense(CreateExpenseViewModel model, ModelStateDictionary modelState)
        {
            var validationResult = validationService.Validate(model, createValidator, modelState);
            if (!validationResult.IsSuccess)
                return JsonResponse<int>.Failure(validationResult.Message);
            var response = await _httpClient.PostAsJsonAsync("Expense", model);
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<int>>();
            return result;
        }

        public async Task<JsonResponse<int>> UpdateExpense(UpdateExpenseViewModel model, ModelStateDictionary modelState)
        {
            var validationResult = validationService.Validate(model, updateValidator, modelState);
            if (!validationResult.IsSuccess)
                return JsonResponse<int>.Failure(validationResult.Message);
            model.Document = await ImageConversions.ConvertToByteArrayAsync(model.File);
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
