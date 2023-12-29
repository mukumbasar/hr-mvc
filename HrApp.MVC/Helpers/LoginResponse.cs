namespace HrApp.MVC;

public class LoginResponse : IResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
}
