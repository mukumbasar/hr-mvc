using AspNetCoreHero.ToastNotification;
using FluentValidation;
using FluentValidation.AspNetCore;
using HrApp.MVC.ClientServices;
using HrApp.MVC.Validator;
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
            services.AddScoped<ExpenseClientService>();
            services.AddScoped<LeaveClientService>();
        }
    }
}
