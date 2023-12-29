using AspNetCoreHero.ToastNotification.Extensions;
using HrApp.MVC;
using HrApp.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMVCDependencies();
builder.Services.AddControllersWithViews();
GlobalOptions.Initialize(builder.Configuration);
var app = builder.Build();
app.Use(async (context, next) =>
{
    var env = app.Environment;
    if (env.IsDevelopment())
    {
        // Eğer geliştirme ortamındaysa, özel bir mesaj gönder
        await context.Response.WriteAsync("Bu uygulama Development ortamında çalışıyor.");
    }
    else
    {
        // Değilse, sonraki middleware'e devam et
        await next();
    }
});
var isDevelopment = app.Configuration["ASPNETCORE_URLS"]?.Contains("localhost") == true;
var deneme = app.Environment.IsDevelopment();
app.UseNotyf();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
