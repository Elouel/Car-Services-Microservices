namespace CarServices.Garage.Features.Employees
{
    using CarServices.Garage.Data;
    using CarServices.Garage.Data.Models;
    using CarServices.Garage.Features.Employees.Models;
    using CarServices.Services.Employee;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class EmployeeService : IEmployeeService
    {
        private readonly GarageDbContext dbContext;

        public EmployeeService(GarageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<EmployeeResponseModel> GetEmployeeByUser(string userId)
        {
            var employee = await this.dbContext
                .Employees
                .Include(x => x.Garage)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if(employee == null)
            {
                return null;
            }

            return  new EmployeeResponseModel()
            {
                Name = employee.Name,
                EmployeeId = employee.Id,
                UserId = employee.UserId,
                GarageId = employee.Garage.Id,
                Role = (int)employee.Role,
                RoleDisplayName = employee.Role.ToString()
            };
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var employee = await this.dbContext
                .Employees
                .Include(x => x.Garage)
                .FirstOrDefaultAsync(x => x.Id == id);

            return employee;
        }

        public async Task<Employee> UpdateEmployee(int id, EmployeeUpdateModel model)
        {
            var employee = await this.dbContext
                .Employees
                .Include(x => x.Garage)
                .FirstOrDefaultAsync(x => x.Id == id);

            employee.Name = model.Name;

            await this.dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<int> CreateEmployee(EmployeeCreateModel model)
        {
            var garage = await this.dbContext.Garages.FirstOrDefaultAsync(x => x.Id == model.GarageId);

            var employee = new Employee()
            {
                UserId = model.UserId,
                Name = model.UserName,
                Role = model.Role,
                Garage = garage
            };

            var departmet = await this.dbContext.Departments.FirstOrDefaultAsync(d => d.Id == model.DepartmentId);

            departmet.Employees.Add(employee);
            garage.Employees.Add(employee);

            this.dbContext.Update(garage);
            this.dbContext.Update(departmet);
            this.dbContext.Add(employee);

            await this.dbContext.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<GarageRoleType> GetEmployeeRoleByUserId(string userId)
        {
            return await this.dbContext
                .Employees
                .Where(e => e.UserId == userId)
                .Select(x => x.Role)
                .FirstOrDefaultAsync();
        }
    }
}
