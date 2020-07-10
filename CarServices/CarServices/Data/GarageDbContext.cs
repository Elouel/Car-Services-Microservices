namespace CarServices.Garage.Data
{
    using CarServices.Garage.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    public class GarageDbContext : DbContext
    {
        public GarageDbContext(DbContextOptions<GarageDbContext> options)
            : base(options)
        {
        }

        public DbSet<Garage> Garages { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
                .Property(e => e.Role)
                .IsRequired();

            builder.Entity<Employee>()
                .Property("UserId")
                .IsRequired();


            builder.Entity<Garage>()
                .HasMany(o => o.Employees)
                    .WithOne(e => e.Garage)
                .HasForeignKey("GarageId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Garage>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasAnnotation(nameof(MinLengthAttribute), 5);

            builder.Entity<Garage>()
                .HasMany(o => o.Departments)
                .WithOne()
                .HasForeignKey("GarageId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Department>()
              .Property(e => e.Name)
              .IsRequired()
              .HasMaxLength(50);

            builder.Entity<Department>()
                .HasMany(d => d.Services)
                    .WithOne()
                .HasForeignKey("DepartmentId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Department>()
                .HasMany(d => d.Employees)
                    .WithOne()
                .HasForeignKey("DepartmentId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Service>()
                .Property(s => s.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Service>()
               .Property(s => s.Name)
               .HasMaxLength(50);

            builder.Entity<Vehicle>()
                .HasOne(v => v.Garage)
                    .WithMany(o => o.Vehicles)
                .HasForeignKey("GarageId");

   


            base.OnModelCreating(builder);
        }
    }
}
