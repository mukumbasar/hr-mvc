using System.Net;
using System.Text;
using AspNetCoreHero.ToastNotification.Notyf;

namespace HrApp.MVC;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.ContentType = "application/json";
            context.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes("Not Found"));
            context.Response.Redirect("/", permanent: false);
        }
    }
}
