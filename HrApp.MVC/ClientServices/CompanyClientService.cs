using HrApp.MVC.Areas.Admin.Models.Company;
using HrApp.MVC.Areas.Admin.Models.Personnel;
using HrApp.MVC.Helpers;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrApp.MVC.ClientServices
{
    public class CompanyClientService
    {

        private readonly HttpClient _httpClient;
        private readonly ValidationService validationService;
        private readonly AddCompanyViewModelValidator addvalidator;

        public CompanyClientService(IHttpClientFactory httpClientFactory, ValidationService validationService, AddCompanyViewModelValidator addvalidator)
        {
            _httpClient = httpClientFactory.CreateClient("api");
            this.validationService = validationService;
            this.addvalidator = addvalidator;
        }

        public async Task<JsonResponse<List<ListCompanyViewModel>>> GetCompanies() =>
            await validationService.ProcessResponse<JsonResponse<List<ListCompanyViewModel>>>(await _httpClient.GetAsync("Company"));

        public async Task<List<SelectListItem>> GetCompanyTypes() =>
            validationService.ProcessResponse<JsonResponse<List<CompanyTypeViewModel>>>(await _httpClient.GetAsync("Company/Types")).Result.Data.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

        public async Task<JsonResponse<string>> CreateCompany(AddCompanyViewModel addCompanyViewModel, ModelStateDictionary modelState) =>
            await validationService.ExecuteValidatedRequestAsync<AddCompanyViewModel, string>(
                addCompanyViewModel,
                addvalidator,
                modelState,
        async () =>
        {
            addCompanyViewModel.ImageData = await
    ImageConversions.ConvertToByteArrayAsync
    (addCompanyViewModel.File);
            return await _httpClient.PostAsJsonAsync("Company", addCompanyViewModel);
        });



    }
}
