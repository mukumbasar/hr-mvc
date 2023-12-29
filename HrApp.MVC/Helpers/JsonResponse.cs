namespace HrApp.MVC.Helpers
{
    public class JsonResponse<T> : IResponseT<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public JsonResponse(T data, bool IsSuccess, string message)
        {
            Data = data;
            this.IsSuccess = IsSuccess;
            Message = message;
        }

        public static JsonResponse<T> Success(T data, string message = "")
        {
            return new JsonResponse<T>(data, true, message);
        }

        public static JsonResponse<T> Failure(string message = "")
        {
            return new JsonResponse<T>(default(T), false, message);
        }
        public static bool IsValid(JsonResponse<T> response)
        {
            return response.IsSuccess;
        }
    }
}
