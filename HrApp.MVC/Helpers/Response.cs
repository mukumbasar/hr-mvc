namespace HrApp.MVC;

public class Response<T> : IResponse<T>
{
    public T Data { get; private set; }
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }

    private Response(T data, bool isSuccess, string message)
    {
        Data = data;
        IsSuccess = isSuccess;
        Message = message;
    }

    public static Response<T> Success(T data, string message = "Process completed successfully")
    {
        return new Response<T>(data, true, message);
    }

    public static Response<T> Failure(string message = "Process failed")
    {
        return new Response<T>(default(T), false, message);
    }
}
