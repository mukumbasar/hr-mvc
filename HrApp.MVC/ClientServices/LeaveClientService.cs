using FluentValidation;
using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Expense;
using HrApp.MVC.Models.Leave;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;

namespace HrApp.MVC.ClientServices
{
    public class LeaveClientService
    {

        private readonly HttpClient _httpClient;
        private readonly CreateLeaveViewModelValidator createValidator;
        private readonly ValidationService validationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LeaveClientService(IHttpClientFactory httpClientFactory, CreateLeaveViewModelValidator createValidator, ValidationService validationService, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("api");
            this.createValidator = createValidator;
            this.validationService = validationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ReadLeaveViewModel>> GetLeaves(string id = "") =>
            validationService.ProcessResponse<JsonResponse<List<ReadLeaveViewModel>>>(await _httpClient.GetAsync($"leave{(string.IsNullOrEmpty(id) ? "" : $"?id={id}")}")).Result.Data;



        public async Task<List<SelectListItem>> GetLeaveTypes()
        {
            return validationService.ProcessResponse<JsonResponse<List<LeaveTypeViewModel>>>(await _httpClient.GetAsync($"leave/Types/")).Result.Data.Where(x => x.LeaveTypeFocusId == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("gender")) || x.LeaveTypeFocusId == 3 && x.Id != 1).Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
        }



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
