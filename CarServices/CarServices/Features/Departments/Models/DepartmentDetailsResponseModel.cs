using CarServices.Garage.Features.Garages.Models;
using System.Collections.Generic;

namespace CarServices.Garage.Features.Departments.Models
{
    public class DepartmentDetailsResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<ServiceListItem> Services { get; set; }

        public IList<EmployeeListResponseModel> Employees { get; set; }
    }
}
