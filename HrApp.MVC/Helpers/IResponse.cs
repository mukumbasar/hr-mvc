namespace HrApp.MVC;

public interface IResponse
{
    bool IsSuccess { get; }
    string Message { get; }
}