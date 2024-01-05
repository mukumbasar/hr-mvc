using System.Net;
using System.Text;

namespace HrApp.MVC.CustomMiddlewares
{
    public class NotFoundErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
            {
                //Re-execute the request so the user gets the error page
                string originalPath = context.Request.Path.Value;
                context.Items["originalPath"] = originalPath;
                context.Request.Path = "/error/404";
                await _next(context);
            }
        }
    }
}
