using HR.LeaveManagement.API.Middleware;
using HR.LeaveManagement.Application;
using HR.LeaveManagement.Identity;
using HR.LeaveManagement.Infrastructure;
using HR.LeaveManagement.Persistence;
using Microsoft.OpenApi.Models;

WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.

applicationBuilder.Services.AddHttpContextAccessor();
AddSwaggerDock(applicationBuilder.Services);
applicationBuilder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
applicationBuilder.Services.AddEndpointsApiExplorer();

applicationBuilder.Services.ConfigureApplicationServices();
applicationBuilder.Services.ConfigurePersistenceServices(applicationBuilder.Configuration);
applicationBuilder.Services.ConfigureInfrastructureServices(applicationBuilder.Configuration);
applicationBuilder.Services.ConfigureIdentityServices(applicationBuilder.Configuration);

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

webApplication.UseMiddleware<ExceptionMiddleware>();
webApplication.UseAuthentication();
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

static void AddSwaggerDock(IServiceCollection services)
{
    services.AddSwaggerGen(key =>
    {
        key.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Scheme",
        });

        key.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },

                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },

                new List<string>()
            }
        });

        key.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "HR Leave Management API",
        });
    });
}