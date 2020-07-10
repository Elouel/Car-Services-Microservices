

namespace CarServices.Garage.Features.Employees.Models
{
    using CarServices.Services.Employee;
    using System.ComponentModel.DataAnnotations;

    public class EmployeeCreateModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public GarageRoleType Role { get; set; }

        [Required]
        public int GarageId { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
