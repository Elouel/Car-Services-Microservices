

namespace CarServices.Garage.Features.Garages
{
    using CarServices.Garage.Data;
    using CarServices.Garage.Data.Models;
    using CarServices.Garage.Features.Employees.Models;
    using CarServices.Garage.Features.Garages.Models;
    using CarServices.Services.Employee;

    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GarageService : IGarageService
    {
        private GarageDbContext dbContext;

        public GarageService(GarageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<GarageResponseModel> CreateGarage(GarageCreateRequestModel model, string userId)
        {

            var departmet = new Department()
            {
                Name = "Valet Bay",
            };

            var newEmployee = new Employee()
            {
                Name = model.AdminName,
                Role = GarageRoleType.Admin,
                UserId = userId,
            };


            var newOrg = new Garage()
            {
                Name = model.GarageName
            };

            departmet.Employees.Add(newEmployee);
            newOrg.Employees.Add(newEmployee);
            newOrg.Departments.Add(departmet);

            this.dbContext.Add(newOrg);

            await this.dbContext.SaveChangesAsync();

            return new GarageResponseModel()
            {
                Id = newOrg.Id,
                Departments = newOrg.Departments.Select(x => new DepartmentListResponseModel() { Id = x.Id, Name = x.Name }).ToList(),
                Employees = newOrg.Employees.Select(x => new EmployeeResponseModel()
                {
                    EmployeeId = x.Id,
                    Name = x.Name,
                    Role = (int)x.Role,
                    RoleDisplayName = x.Role.ToString()
                }).ToList(),
                GarageName = newOrg.Name,
            };
        }


        public async Task<Garage> GetGarage(int garageId)
        {
            var garage = await
                this.dbContext
                .Garages
                .AsNoTracking()
                .Include(g => g.Departments)
                .Include(g => g.Employees)
                .Include(g => g.Vehicles)
                .FirstOrDefaultAsync(x => x.Id == garageId);

            return garage;
        }

        public async Task<Garage> GetDefaultGarage(string userId)
        {
            var garage = await
                this.dbContext
                .Garages
                .AsNoTracking()
                .Include(o => o.Departments)
                .Include(o => o.Employees)
                .FirstOrDefaultAsync(x => x.Employees.Any(e => e.UserId == userId));

            return garage;
        }

        public async Task<IList<Department>> GetGarageDepartments(int garageId)
        {
            var garage = await
                this.dbContext
                .Garages
                .AsNoTracking()
                .Include(o => o.Departments)
                .Where(o => o.Id == garageId)
                 .Select(o => o.Departments)
                .FirstOrDefaultAsync();

            return garage;
        }
    }
}
