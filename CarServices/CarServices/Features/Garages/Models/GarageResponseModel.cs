
namespace CarServices.Garage.Features.Garages.Models
{
    using CarServices.Garage.Features.Employees.Models;
    using CarServices.Garage.Features.Vehicles.Models;
    using System.Collections.Generic;

    public class GarageResponseModel
    {
        public int Id { get; set; }

        public string GarageName { get; set; }

        public IList<DepartmentListResponseModel> Departments { get; set; }
        public IList<EmployeeResponseModel> Employees { get; set; }
        public IList<VehicleListModel> Vehicles { get; set; }

    }
}
