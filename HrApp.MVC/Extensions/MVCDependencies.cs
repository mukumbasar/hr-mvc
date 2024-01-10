using AspNetCoreHero.ToastNotification;
using FluentValidation;
using FluentValidation.AspNetCore;
using HrApp.MVC.ClientServices;
using HrApp.MVC.CustomMiddlewares;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;

namespace HrApp.MVC.Extensions
{
    public static class MVCDependencies
    {
        public static void AddMVCDependencies(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AppUserUpdateViewModelValidator>();
            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
            services.AddHttpClient("api", c => // "api" is the name of the HttpClient
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                    c.BaseAddress = new Uri("https://localhost:7213/api/");
                else
                    c.BaseAddress = new Uri("https://ank14hr.azurewebsites.net/api/");
            });
            services.AddScoped<LoginClientService>();
            services.AddScoped<PersonelClientService>();
            services.AddScoped<CommonClientService>();
            services.AddScoped<AdvanceClientService>();
            services.AddScoped<ExpenseClientService>();
            services.AddScoped<LeaveClientService>();
            services.AddScoped<ValidationService>();
            services.AddScoped<EmailClientService>();
            services.AddScoped<CompanyClientService>();
            services.AddScoped<ResponseHandler>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.LoginPath = "/Home/Login"; // Giriş sayfasının yolu
                opt.Cookie.Name = "HrAppCookie"; // Cookie adı
                opt.Cookie.HttpOnly = false;
            });


        }

        public static IApplicationBuilder UseNotFoundErrorHandlingMiddleware(
        this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NotFoundErrorHandlingMiddleware>();
        }
    }
}
