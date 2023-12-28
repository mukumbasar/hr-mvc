using AspNetCoreHero.ToastNotification;
using FluentValidation;
using FluentValidation.AspNetCore;
using HrApp.MVC.ClientServices;
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
            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopCenter; });
            services.AddHttpClient("api", c => // "api" is the name of the HttpClient
            {
                c.BaseAddress = new Uri("https://ank14hr.azurewebsites.net/api/");
            });

            services.AddScoped<LoginClientService>();
            services.AddScoped<PersonelClientService>();
            services.AddScoped<CommonClientService>();
            services.AddScoped<AdvanceClientService>();
            services.AddScoped<ExpenseClientService>();
            services.AddScoped<LeaveClientService>();

            services.AddScoped<ResponseHandler>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.LoginPath = "/Home/Login"; // Giriş sayfasının yolu
                opt.Cookie.Name = "HrAppCookie"; // Cookie adı
                opt.Cookie.HttpOnly = false;
            });
        }
    }
}
