
namespace CarServices.Garage.Features.Departments
{
    using System.Threading.Tasks;
    using CarServices.Garage.Data.Models;
    using CarServices.Garage.Features.Departments.Models;

    public interface IDepartmentService
    {
        Task<int> CreateDepartment(CreateDepartmentRequestModel model, string userId);
        Task<Department> GetDepartmentDetails(int id);
    }
}
