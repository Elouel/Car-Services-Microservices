namespace CarServices.Garage.Infrastructure.Extensions
{
    using CarServices.Garage.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<GarageDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
