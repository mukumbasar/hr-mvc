using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Advance;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            this.updateValidator = updateValidator;
            this.validationService = validationService;
            this.createValidator = createValidator;
        }
        public async Task<JsonResponse<List<ReadAdvanceViewModel>>> GetAdvances() =>
            await validationService.ProcessResponse<JsonResponse<List<ReadAdvanceViewModel>>>(await _httpClient.GetAsync("Advance"));
        public async Task<JsonResponse<UpdateAdvanceViewModel>> GetAdvance(int id) =>
            await validationService.ProcessResponse<JsonResponse<UpdateAdvanceViewModel>>(await _httpClient.GetAsync($"Advance/{id}"));

        public async Task<List<SelectListItem>> GetAdvanceTypes() =>
            validationService.ProcessResponse<JsonResponse<List<AdvanceTypeViewModel>>>(await _httpClient.GetAsync("Advance/Types")).Result.Data.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

        public async Task<JsonResponse<decimal>> CreateAdvance(CreateAdvanceViewModel createAdvanceViewModel, ModelStateDictionary modelState)
        {
            var validationResult = validationService.ModelValidator(createAdvanceViewModel, createValidator, modelState);
            if (!validationResult.IsSuccess)
                return JsonResponse<decimal>.Failure(validationResult.Message);
            return await validationService.ProcessResponse<JsonResponse<decimal>>(await _httpClient.PostAsJsonAsync("Advance", createAdvanceViewModel));
        }
        public async Task<JsonResponse<decimal>> UpdateAdvance(UpdateAdvanceViewModel updateAdvanceViewModel, ModelStateDictionary modelState)
        {
            var validationResult = validationService.ModelValidator(updateAdvanceViewModel, updateValidator, modelState);
            if (!validationResult.IsSuccess)
                return JsonResponse<decimal>.Failure(validationResult.Message);
            return await validationService.ProcessResponse<JsonResponse<decimal>>(await _httpClient.PutAsJsonAsync("Advance", updateAdvanceViewModel));
        }
        public async Task<JsonResponse<decimal>> DeleteAdvance(int id) =>
            await validationService.ProcessResponse<JsonResponse<decimal>>(await _httpClient.DeleteAsync($"advance/{id}"));

    }
}
