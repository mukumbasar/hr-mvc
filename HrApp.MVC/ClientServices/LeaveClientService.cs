using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Expense;
using HrApp.MVC.Models.Leave;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Text;

namespace HrApp.MVC.ClientServices
{
    public class LeaveClientService
    {

        private readonly HttpClient _httpClient;
        private readonly CreateLeaveViewModelValidator createValidator;
        private readonly ValidationService validationService;

        public LeaveClientService(IHttpClientFactory httpClientFactory, CreateLeaveViewModelValidator createValidator, ValidationService validationService)
        {
            _httpClient = httpClientFactory.CreateClient("api");
            this.createValidator = createValidator;
            this.validationService = validationService;
        }

        public async Task<List<ReadLeaveViewModel>> GetLeaves()
        {
            var response = await _httpClient.GetAsync("leave");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<ReadLeaveViewModel>>>();
            return result.Data;
        }

        public async Task<List<LeaveTypeViewModel>> GetLeaveTypes()
        {
            var response = await _httpClient.GetAsync("leave/Types");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<LeaveTypeViewModel>>>();
            return result.Data;
        }

        public async Task<JsonResponse<ReadLeaveViewModel>> GetLeave(int id)
        {
            var response = await _httpClient.GetAsync($"leave/{id}");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<ReadLeaveViewModel>>();
            return result;
        }

        public async Task<JsonResponse<int>> CreateLeave(CreateLeaveViewModel model, ModelStateDictionary modelState)
        {
            var validationResult = validationService.ModelValidator(model, createValidator, modelState);
            if (!validationResult.IsSuccess)
                return JsonResponse<int>.Failure(validationResult.Message);
            var response = await _httpClient.PostAsJsonAsync("leave", model);
            return await validationService.ProcessResponse<JsonResponse<int>>(response);
        }

        public async Task<JsonResponse<int>> UpdateLeave(UpdateLeaveViewModel model)
        {
            var response = await _httpClient.PutAsync("leave", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            return await validationService.ProcessResponse<JsonResponse<int>>(response);
        }

        public async Task<JsonResponse<int>> DeleteLeave(int id)
        {
            var response = await _httpClient.DeleteAsync($"leave/{id}");
            return await validationService.ProcessResponse<JsonResponse<int>>(response);
        }
    }
}
