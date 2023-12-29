using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Expense;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

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


        public async Task<JsonResponse<List<ReadExpenseViewModel>>> GetExpenses() =>
            await validationService.ProcessResponse<JsonResponse<List<ReadExpenseViewModel>>>(await _httpClient.GetAsync("Expense"));


        public async Task<List<SelectListItem>> GetExpenseTypes() =>
            validationService.ProcessResponse<JsonResponse<List<ExpenseTypeViewModel>>>(await _httpClient.GetAsync("Expense/Types")).Result.Data.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();


        public async Task<JsonResponse<UpdateExpenseViewModel>> GetExpense(int id) =>
            await validationService.ProcessResponse<JsonResponse<UpdateExpenseViewModel>>(await _httpClient.GetAsync($"Expense/{id}"));


        public async Task<JsonResponse<int>> CreateExpense(CreateExpenseViewModel model, ModelStateDictionary modelState)
        {
            var validationResult = validationService.ModelValidator(model, createValidator, modelState);
            if (!validationResult.IsSuccess)
                return JsonResponse<int>.Failure(validationResult.Message);
            var response = await _httpClient.PostAsJsonAsync("Expense", model);
            return await validationService.ProcessResponse<JsonResponse<int>>(response);
        }

        public async Task<JsonResponse<int>> UpdateExpense(UpdateExpenseViewModel model, ModelStateDictionary modelState)
        {
            var validationResult = validationService.ModelValidator(model, updateValidator, modelState);
            if (!validationResult.IsSuccess)
                return JsonResponse<int>.Failure(validationResult.Message);
            model.Document = await ImageConversions.ConvertToByteArrayAsync(model.File);
            var response = await _httpClient.PutAsJsonAsync("Expense", model);
            return await validationService.ProcessResponse<JsonResponse<int>>(response);
        }

        public async Task<JsonResponse<int>> DeleteExpense(int id) =>
            await validationService.ProcessResponse<JsonResponse<int>>(await _httpClient.DeleteAsync($"Expense/{id}"));


    }
}
