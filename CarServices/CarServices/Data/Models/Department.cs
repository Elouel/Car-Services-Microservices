namespace CarServices.Garage.Data.Models
{
    using System.Collections.Generic;

    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Service> Services { get; } = new List<Service>();

        public IList<Employee> Employees { get; } = new List<Employee>();
    }
}
