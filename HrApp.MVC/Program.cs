using AspNetCoreHero.ToastNotification.Extensions;
using HrApp.MVC;
using HrApp.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddMVCDependencies();
builder.Services.AddControllersWithViews();
GlobalOptions.Initialize(builder.Configuration);
var app = builder.Build();
app.UseNotyf();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment() && app.Environment.ApplicationName != "Staging")
{
    app.UseExceptionHandler("/Error/Exception");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseNotFoundErrorHandlingMiddleware();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.UseMiddleware<AdminRedirectMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Personnel}/{action=List}/{id?}"
    );
    endpoints.MapDefaultControllerRoute();
});

app.Run();
