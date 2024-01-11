
using HrApp.MVC.Helpers;

namespace HrApp.MVC;

public class ApprovalClientService
{
    private HttpClient _httpClient;
    private ValidationService validationService;

    public ApprovalClientService(IHttpClientFactory httpClientFactory, ValidationService validationService)
    {
        _httpClient = httpClientFactory.CreateClient("api");
        this.validationService = validationService;
    }
    public async Task<JsonResponse<int>> Approve(string id, string data, bool isApproved) =>
        await validationService.ProcessResponse<JsonResponse<int>>(await _httpClient.GetAsync($"{data}/approve?id={id}&isApproved={isApproved}"));
}
