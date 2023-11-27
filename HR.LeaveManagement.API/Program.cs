using HR.LeaveManagement.Application;
using HR.LeaveManagement.Infrastructure;
using HR.LeaveManagement.Persistence;

WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.

applicationBuilder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
applicationBuilder.Services.AddEndpointsApiExplorer();
applicationBuilder.Services.AddSwaggerGen();

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
    webApplication.UseSwagger();
    webApplication.UseSwaggerUI();
}

webApplication.UseHttpsRedirection();

webApplication.UseAuthorization();

webApplication.UseCors("CorsPolicy");

webApplication.MapControllers();

webApplication.Run();