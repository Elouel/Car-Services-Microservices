

namespace CarService.Gateway
{
    using CarRentalSystem.Infrastructure;
    using CarService.Infrastructure.Middlewares;
    using CarService.Gateway.Services;
    using CarService.Gateway.Services.Garages;
    using CarService.Services.Identity;
    using CarServices.Garage.Infrastructure;
    using CarServices.Gateway.Infrastructure;
    using CarServices.Infrastructure;
    using CarServices.Services.Employee;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Refit;

    using static CarService.Infrastructure.InfrastructureConstants;
    using CarService.Gateway.Services.JobSchedulers;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var serviceEndpoints = this.Configuration
              .GetSection(nameof(ServiceEndpoints))
              .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services
                .AddTokenAuthentication(this.Configuration)
                 .AddAuthorization(config =>
                 {
                     config.AddPolicy(GarageAdminPolicyName, policybuilder => policybuilder.Requirements.Add(new AdminRequired()));
                     config.AddPolicy(GarageSalesmanPolicyName, policybuilder => policybuilder.Requirements.Add(new SalesmanRequired()));
                 })
                .AddScoped<ICurrentTokenService, CurrentTokenService>()
                .AddTransient<JwtHeaderAuthenticationMiddleware>()
                .AddTransient<CurrentEmployeeMiddleware>()
                .AddTransient<ICurrentEmployeeService, CurrentEmployeeService>()
                .AddScoped<IAuthorizationHandler, AdminRequiredHandler>()
                .AddScoped<IAuthorizationHandler, SalesmanRequiredHandler>()
                .AddControllers();

            services
              .AddRefitClient<IGarageService>()
              .WithConfiguration(serviceEndpoints.Garages);

            services
            .AddRefitClient<IJobScheduler>()
            .WithConfiguration(serviceEndpoints.JobScheduler);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseJwtHeaderAuthentication();
            app.UseMiddleware<CurrentEmployeeMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
