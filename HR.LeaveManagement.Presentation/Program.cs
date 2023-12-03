using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Services;
using HR.LeaveManagement.Presentation.Services.Base;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.
applicationBuilder.Services.AddHttpClient<IClient, Client>(key => key.BaseAddress = new Uri("https://localhost:7271"));
//applicationBuilder.Services.AddSingleton<IClient, Client>();
applicationBuilder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
applicationBuilder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();

applicationBuilder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
applicationBuilder.Services.AddControllersWithViews();

WebApplication webApplication = applicationBuilder.Build();

// Configure the HTTP request pipeline.
if (!webApplication.Environment.IsDevelopment())
{
    webApplication.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    webApplication.UseHsts();
}

webApplication.UseHttpsRedirection();
webApplication.UseStaticFiles();

webApplication.UseRouting();

webApplication.UseAuthorization();

webApplication.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

webApplication.Run();