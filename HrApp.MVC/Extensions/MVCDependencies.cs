using AspNetCoreHero.ToastNotification;
using FluentValidation;
using FluentValidation.AspNetCore;
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
        }
    }
}
