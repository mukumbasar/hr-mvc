using AspNetCoreHero.ToastNotification;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace HrApp.MVC.Extensions
{
    public static class MVCDependencies
    {
        public static void AddMVCDependencies(this IServiceCollection services)
        {
            //services.AddValidatorsFromAssemblyContaining<PersonValidator>();
            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
        }
    }
}
