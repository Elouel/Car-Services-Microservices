namespace CarServices.Garage.Features.Employees.Models
{
    public class EmployeeResponseModel
    {
        public int EmployeeId { get; set; }

        public string UserId { get; set; }

        public int GarageId { get; set; }

        public string Name { get; set; }

        public int Role { get; set; }

        public string RoleDisplayName { get; set; }
    }
}
