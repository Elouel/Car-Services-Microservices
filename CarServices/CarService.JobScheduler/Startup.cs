
namespace CarService.JobScheduler
{
    using CarService.Infrastructure;
    using CarService.JobScheduler.Data;
    using CarServices.Infrastructure;
    using CarServices.Services.Employee;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
                .AddWebService<JobSchedulerDbContext>(this.Configuration)
                 .AddTransient<ICurrentEmployeeService, CurrentEmployeeService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebService(env);
        }
    }
}
