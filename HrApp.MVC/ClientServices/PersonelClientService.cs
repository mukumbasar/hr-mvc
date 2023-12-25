using System.Net.Http;

namespace HrApp.MVC;

public class PersonelClientService
{
    private readonly HttpClient _httpClient;
    public PersonelClientService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("api");
    }

}
