
namespace CarServices.Garage.Features.Employees
{
    using CarServices.Garage.Data.Models;
    using CarServices.Garage.Features.Employees.Models;
    using CarServices.Services.Employee;
    using System.Threading.Tasks;

    public interface IEmployeeService
    {
        Task<int> CreateEmployee(EmployeeCreateModel model);
        Task<EmployeeResponseModel> GetEmployeeByUser(string userId);

        Task<GarageRoleType> GetEmployeeRoleByUserId(string userId);

        Task<Employee> GetEmployee(int id);

        Task<Employee> UpdateEmployee(int id, EmployeeUpdateModel model);
    }
}
