
namespace CarServices.Garage
{
    using CarService.Infrastructure;
    using CarServices.Garage.Data;
    using CarServices.Garage.Infrastructure;
    using CarServices.Garage.Infrastructure.Extensions;
    using CarServices.Garage.Infrastructure.Middlewares;
    using CarServices.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using static CarService.Infrastructure.InfrastructureConstants;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddWebService<GarageDbContext>(this.Configuration)
                .AddAuthorization(config =>
                {
                    config.AddPolicy(GarageAdminPolicyName, policybuilder => policybuilder.Requirements.Add(new AdminRequired()));
                })
                .AddApplicationServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .Initialize();
            }

            app.UseHttpsRedirection()
                .UseRouting()
                .UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod())
                .UseAuthentication()
                .UseMiddleware<CurrentEmployeeMiddleware>()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints
                    .MapControllers());

        }
    }
}
