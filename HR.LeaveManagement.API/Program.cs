using HR.LeaveManagement.Application;
using HR.LeaveManagement.Infrastructure;
using HR.LeaveManagement.Persistence;
using Microsoft.OpenApi.Models;

WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.

applicationBuilder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
applicationBuilder.Services.AddEndpointsApiExplorer();
applicationBuilder.Services.AddSwaggerGen(key =>
{
    key.SwaggerDoc("v1", new OpenApiInfo { Title = "HR LeaveManagement API", Version = "v1" });
});

applicationBuilder.Services.ConfigureApplicationServices();
applicationBuilder.Services.ConfigurePersistenceServices(applicationBuilder.Configuration);
applicationBuilder.Services.ConfigureInfrastructureServices(applicationBuilder.Configuration);

applicationBuilder.Services.AddCors(key =>
{
    key.AddPolicy("CorsPolicy",
        applicationBuilder => applicationBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

WebApplication webApplication = applicationBuilder.Build();

// Configure the HTTP request pipeline.
if (webApplication.Environment.IsDevelopment())
{
    webApplication.UseDeveloperExceptionPage();
}

webApplication.UseSwagger();
webApplication.UseSwaggerUI(key => key.SwaggerEndpoint("/swagger/v1/swagger.json", "HR LeaveManagement API"));

webApplication.UseHttpsRedirection();

webApplication.UseRouting();

webApplication.UseAuthorization();

webApplication.UseCors("CorsPolicy");

webApplication.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

webApplication.Run();