using System.Net.Http;
using System.Threading.Tasks;
using HrApp.MVC;
using HrApp.MVC.Models;
using Newtonsoft.Json;

public class LoginClientService
{
    private readonly HttpClient _httpClient;
    public LoginClientService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<bool> LoginAsync(LoginViewModel loginViewModel, HttpContext httpContext)
    {
        var response = await _httpClient.PostAsJsonAsync("User/login", loginViewModel);
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            await LoginHelper.LoginAsync(jsonResponse, httpContext);
            return true;
        }
        else
        {
            return false;
        }
    }
}
