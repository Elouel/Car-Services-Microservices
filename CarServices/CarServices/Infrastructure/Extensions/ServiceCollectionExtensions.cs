

namespace CarServices.Garage.Infrastructure.Extensions
{

    using CarServices.Garage.Data;
    using CarServices.Garage.Features.Departments;
    using CarServices.Garage.Features.Employees;
    using CarServices.Garage.Features.Garages;
    using CarServices.Garage.Infrastructure.Middlewares;
    using CarServices.Services.Employee;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    public static class ServiceCollectionExtensions
    {
        public static ApplicationSettings GetApplicationSettings(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<ApplicationSettings>(applicationSettingsConfiguration);
            return applicationSettingsConfiguration.Get<ApplicationSettings>();
        }

        public static IServiceCollection AddDatabase(
           this IServiceCollection services,
           IConfiguration configuration)
           => services
               .AddDbContext<GarageDbContext>(options => options
                   .UseSqlServer(configuration.GetDefaultConnectionString()));
        
        public static IServiceCollection AddJwtAuthentication(
           this IServiceCollection services,
           ApplicationSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
                .AddScoped<IAuthorizationHandler, AdminRequiredHandler>()
                .AddTransient<IGarageService, GarageService>()
                .AddTransient<CurrentEmployeeMiddleware>()
                .AddTransient<ICurrentEmployeeService, CurrentEmployeeService>()
                .AddTransient<IEmployeeService, EmployeeService>()
                .AddTransient<IDepartmentService, DepartmentService>();
    }
}
