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
    public IActionResult HandleResponse<TModel>(IResponse<TModel> response, string successView, string errorView, Controller controller)
    {
        string actionName = controller.ControllerContext.ActionDescriptor.ActionName;
        string controllerName = controller.ControllerContext.ActionDescriptor.ControllerName;
        // Response başarılı ise, successView dön
        if (response.IsSuccess)
        {
            if (actionName != "Index" || controllerName != "Home")
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
public interface IResponse<T>
{
    T Data { get; }
    bool IsSuccess { get; }
    string Message { get; }
}
