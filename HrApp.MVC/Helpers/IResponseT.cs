namespace HrApp.MVC;

public interface IResponseT<T> : IResponse
{
    T Data { get; }
}

public interface IResponse
{
    bool IsSuccess { get; }
    string Message { get; }
}