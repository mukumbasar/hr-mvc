using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Expense;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

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


        public async Task<JsonResponse<List<ReadExpenseViewModel>>> GetExpenses(string id = "") =>
            await validationService.ProcessResponse<JsonResponse<List<ReadExpenseViewModel>>>(await _httpClient.GetAsync($"Expense{(string.IsNullOrEmpty(id) ? "" : $"?id={id}")}"));



        public async Task<List<SelectListItem>> GetExpenseTypes() =>
            validationService.ProcessResponse<JsonResponse<List<ExpenseTypeViewModel>>>(await _httpClient.GetAsync("Expense/Types")).Result.Data.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

        public async Task<JsonResponse<UpdateExpenseViewModel>> GetExpense(int id) =>
            await validationService.ProcessResponse<JsonResponse<UpdateExpenseViewModel>>(await _httpClient.GetAsync($"Expense/{id}"));

        public async Task<JsonResponse<ReadExpenseFileViewModel>> GetExpenseFile(int id) =>
            await validationService.ProcessResponse<JsonResponse<ReadExpenseFileViewModel>>(await _httpClient.GetAsync($"Expense/{id}"));

        public async Task<JsonResponse<int>> CreateExpense(CreateExpenseViewModel model, ModelStateDictionary modelState) =>
            await validationService.ExecuteValidatedRequestAsync<CreateExpenseViewModel, int>(
                model,
                createValidator,
                modelState,
                async () =>
                {
                    model.Document = await ImageConversions.ConvertToByteArrayAsync(model.File);
                    return await _httpClient.PostAsJsonAsync("Expense", model);
                });


        public async Task<JsonResponse<int>> UpdateExpense(UpdateExpenseViewModel model, ModelStateDictionary modelState) =>
            await validationService.ExecuteValidatedRequestAsync<UpdateExpenseViewModel, int>(
                model,
                updateValidator,
                modelState,
                async () =>
                {
                    model.Document = await ImageConversions.ConvertToByteArrayAsync(model.File);
                    return await _httpClient.PutAsJsonAsync("Expense", model);
                });

        public async Task<JsonResponse<int>> DeleteExpense(int id) =>
            await validationService.ProcessResponse<JsonResponse<int>>(await _httpClient.DeleteAsync($"Expense/{id}"));


    }
}
