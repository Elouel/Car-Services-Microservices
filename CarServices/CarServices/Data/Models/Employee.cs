namespace CarServices.Garage.Data.Models
{
    using CarServices.Services.Employee;

    public class Employee
    {
        public int Id { get; set; }

        public GarageRoleType Role { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public Garage Garage { get; set; }
    }
}
