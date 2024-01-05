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
    public IActionResult HandleResponse<TModel>(IResponseT<TModel>? response, string successView, string errorView, Controller controller)
    {
        string actionName = controller.ControllerContext.ActionDescriptor.ActionName.ToLower();
        string controllerName = controller.ControllerContext.ActionDescriptor.ControllerName.ToLower();
        // Response başarılı ise, successView dön
        if (response.IsSuccess)
        {
            if (response.Message != "")
                _notyfService.Success(response.Message);
            if (successView.ToLower().Contains("partial"))
                return controller.PartialView(successView, response.Data);
            return successView.ToLower() == actionName ? controller.View(response.Data ?? default) : controller.RedirectToAction(successView);
        }

        // Response başarısız ise, errorView dön
        else
        {
            _notyfService.Error(response.Message);
            return errorView.ToLower() == actionName ? controller.View(response.Data) : controller.RedirectToAction(errorView, controllerName);
        }
    }
}
