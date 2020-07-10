

namespace CarServices.Garage.Features.Departments
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CarServices.Garage.Data;
    using CarServices.Garage.Data.Models;
    using CarServices.Garage.Features.Departments.Models;
    using Microsoft.EntityFrameworkCore;

    public class DepartmentService : IDepartmentService
    {
        private GarageDbContext dbContext;

        public DepartmentService(GarageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Department> GetDepartmentDetails(int id)
        {
            Department department = await this.dbContext.Departments
                .Include(d => d.Services)
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(x => x.Id == id);

            return department;
        }

        public async Task<int> CreateDepartment(CreateDepartmentRequestModel model, string userId)
        {
            Garage garage = await this.dbContext.Employees
                .Where(x => x.UserId == userId && x.Garage.Id == model.GarageId)
                .Select(x => x.Garage)
                .FirstAsync();

            var department = new Department()
            {
                Name = model.DepartmentName,
            };

            garage.Departments.Add(department);

            this.dbContext.Update(garage);

            await this.dbContext.SaveChangesAsync();

            return department.Id;
        }
    }
}
