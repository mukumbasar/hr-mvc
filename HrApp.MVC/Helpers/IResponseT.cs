namespace HrApp.MVC;

public interface IResponseT<T> : IResponse
{
    T Data { get; }
}
