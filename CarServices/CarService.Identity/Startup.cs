namespace CarService.Identity
{
    using CarService.Infrastructure;
    using CarServices.Identity.Data;
    using CarServices.Identity.Infrastructure.Extensions;
    using CarServices.Infrastructure;
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
                .AddWebService<IdentityDbContext>(this.Configuration)
                .AddUserStorage()
                .AddApplicationServices()
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebService(env);
        }
    }
}
