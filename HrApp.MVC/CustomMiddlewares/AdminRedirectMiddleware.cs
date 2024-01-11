using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

public class AdminRedirectMiddleware
{
    private readonly RequestDelegate _next;

    public AdminRedirectMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Kullanıcı giriş yapmışsa
        if (context.User.Identity.IsAuthenticated)
        {
            var temp = context.GetRouteData();

            var tempController = temp.Values["controller"].ToString().ToLower();
            var tempAction = temp.Values["action"].ToString().ToLower();
            // Kullanıcı 'Admin' rolünde mi kontrol et
            if (context.User.FindFirstValue("role").ToLower().Contains("website") && tempAction != "logout" && tempController != "error")
            {
                // Eğer kullanıcı zaten Admin alanında değilse, Admin alanına yönlendir
                if (!context.Request.Path.StartsWithSegments("/Admin"))
                {
                    context.Response.Redirect("/Admin/CompanyManager/Index");
                    return;
                }
            }
        }

        // Sonraki middleware'e geç
        await _next(context);
    }
}
