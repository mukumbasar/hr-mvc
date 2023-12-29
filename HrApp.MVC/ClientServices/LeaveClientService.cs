using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Expense;
using HrApp.MVC.Models.Leave;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<List<ReadLeaveViewModel>> GetLeaves() =>
             validationService.ProcessResponse<JsonResponse<List<ReadLeaveViewModel>>>(await _httpClient.GetAsync("leave")).Result.Data;


        public async Task<List<SelectListItem>> GetLeaveTypes() =>
             validationService.ProcessResponse<JsonResponse<List<LeaveTypeViewModel>>>(await _httpClient.GetAsync("leave/Types")).Result.Data.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();


        public async Task<JsonResponse<ReadLeaveViewModel>> GetLeave(int id) =>
             await validationService.ProcessResponse<JsonResponse<ReadLeaveViewModel>>(await _httpClient.GetAsync($"leave/{id}"));


        public async Task<JsonResponse<int>> CreateLeave(CreateLeaveViewModel model, ModelStateDictionary modelState) =>
             await validationService.ExecuteValidatedRequestAsync<CreateLeaveViewModel, int>(
                model,
                createValidator,
                modelState,
                async () =>
                {
                    return await _httpClient.PostAsJsonAsync("leave", model);
                });


        public async Task<JsonResponse<int>> UpdateLeave(UpdateLeaveViewModel model) =>
             await validationService.ProcessResponse<JsonResponse<int>>(await _httpClient.PutAsJsonAsync("leave", model));


        public async Task<JsonResponse<int>> DeleteLeave(int id) =>
             await validationService.ProcessResponse<JsonResponse<int>>(await _httpClient.DeleteAsync($"leave/{id}"));

    }
}
