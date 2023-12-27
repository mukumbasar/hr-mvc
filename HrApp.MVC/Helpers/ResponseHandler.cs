using AspNetCoreHero.ToastNotification.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace HrApp.MVC;

public class ResponseHandler
{
    private readonly INotyfService _notyfService;

    public ResponseHandler(INotyfService notyfService)
    {
        _notyfService = notyfService;
    }
    /// <summary>
    /// HandleResponse metodu, bir response nesnesini işler ve sonuca göre ilgili view'a yönlendirme yapar. 
    /// Eğer response başarılıysa, belirtilen successView'a yönlendirir. 
    /// Başarısız ise, errorView'a yönlendirir.
    /// Ayrıca, yönlendirme işlemi, mevcut action adı ile belirtilen view adı eşleşiyorsa direkt view dönüşü yapar,
    /// eşleşmiyorsa RedirectToAction ile ilgili view'a yönlendirme yapar.
    /// </summary>
    /// <typeparam name="TModel">Response nesnesinin taşıyacağı veri tipi.</typeparam>
    /// <param name="response">İşlenecek response nesnesi.</param>
    /// <param name="successView">Response başarılı olduğunda yönlendirilecek view adı.</param>
    /// <param name="errorView">Response başarısız olduğunda yönlendirilecek view adı.</param>
    /// <param name="controller">Mevcut controller nesnesi, yönlendirme ve view dönüşleri için kullanılır.</param>
    /// <returns>View veya RedirectToAction sonucu döndürür. Bu sonucu girilen successView ve errorView'e göre kendisi belirler.</returns>
    public IActionResult HandleResponse<TModel>(Response<TModel> response, string successView, string errorView, Controller controller)
    {
        string actionName = controller.ControllerContext.ActionDescriptor.ActionName;
        // Response başarılı ise, successView dön
        if (response.IsSuccess)
        {
            _notyfService.Success(response.Message);
            return successView.ToLower() == actionName.ToLower() ? controller.View(response.Data) : controller.RedirectToAction(successView);
        }

        // Response başarısız ise, errorView dön
        else
        {
            _notyfService.Error(response.Message);
            return errorView.ToLower() == actionName.ToLower() ? controller.View(response.Data) : controller.RedirectToAction(errorView);
        }
    }

}


public class Response<T>
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
