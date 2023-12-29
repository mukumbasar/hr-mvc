using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Advance;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Text;

namespace HrApp.MVC.ClientServices
{
    public class AdvanceClientService
    {
        private readonly HttpClient _httpClient;
        private readonly UpdateAdvanceViewModelValidator updateValidator;
        private readonly ValidationService validationService;
        private readonly CreateAdvanceViewModelValidator createValidator;
        public AdvanceClientService(IHttpClientFactory httpClientFactory, UpdateAdvanceViewModelValidator updateValidator, ValidationService validationService, CreateAdvanceViewModelValidator createValidator)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }
        public async Task<List<ReadAdvanceViewModel>> GetAdvances()
        {
            var response = await _httpClient.GetAsync("Advance");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<ReadAdvanceViewModel>>>();
            return result.Data;
        }
        public async Task<JsonResponse<UpdateAdvanceViewModel>> GetAdvance(int id)
        {
            var response = await _httpClient.GetAsync($"Advance/{id}");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<UpdateAdvanceViewModel>>();
            return result;
        }

        public async Task<List<AdvanceTypeViewModel>> GetAdvanceTypes()
        {
            var response = await _httpClient.GetAsync($"Advance/Types");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<List<AdvanceTypeViewModel>>>();
            return result.Data;
        }

        public async Task<JsonResponse<decimal>> CreateAdvance(CreateAdvanceViewModel createAdvanceViewModel, ModelStateDictionary modelState)
        {
            var validationResult = validationService.Validate(createAdvanceViewModel, createValidator, modelState);
            if (!validationResult.IsSuccess)
                return JsonResponse<decimal>.Failure(validationResult.Message);
            var response = await _httpClient.PostAsync("Advance", new StringContent(JsonConvert.SerializeObject(createAdvanceViewModel), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<decimal>>();
            return result;
        }
        public async Task<JsonResponse<decimal>> UpdateAdvance(UpdateAdvanceViewModel updateAdvanceViewModel, ModelStateDictionary modelState)
        {
            var validationResult = validationService.Validate(updateAdvanceViewModel, updateValidator, modelState);
            if (!validationResult.IsSuccess)
                return JsonResponse<decimal>.Failure(validationResult.Message);
            var response = await _httpClient.PutAsync("advance", new StringContent(JsonConvert.SerializeObject(updateAdvanceViewModel), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<decimal>>();
            return result;
        }
        public async Task<JsonResponse<decimal>> DeleteAdvance(int id)
        {
            var response = await _httpClient.DeleteAsync($"advance/{id}");
            var result = await response.Content.ReadFromJsonAsync<JsonResponse<decimal>>();
            return result;
        }
    }
}
