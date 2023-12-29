using System.Net.Http;
using System.Threading.Tasks;
using HrApp.MVC;
using HrApp.MVC.Helpers;
using HrApp.MVC.Models;
using Newtonsoft.Json;

public class LoginClientService
{
    private readonly HttpClient _httpClient;
    private readonly ValidationService validationService;

    public LoginClientService(IHttpClientFactory httpClientFactory, ValidationService validationService)
    {
        _httpClient = httpClientFactory.CreateClient("api");
        this.validationService = validationService;
    }

    public async Task<JsonResponse<bool>> LoginAsync(LoginViewModel loginViewModel, HttpContext httpContext)
    {
        var response = await _httpClient.PostAsJsonAsync("User/login", loginViewModel);
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            await LoginHelper.LoginAsync(jsonResponse.Token, httpContext);
            return JsonResponse<bool>.Success(true, "Login successfull");
        }
        else
        {
            return JsonResponse<bool>.Failure("Login failed");
        }
    }
    public async Task<JsonResponse<bool>> LogoutAsync(HttpContext httpContext)
    {
        await LoginHelper.LogoutAsync(httpContext);
        return JsonResponse<bool>.Success(true, "Logout successfull");
    }
}
