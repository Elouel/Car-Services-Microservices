namespace CarServices.Garage.Data.Models
{
    using System.Collections.Generic;

    public class Garage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Employee> Employees { get;  } = new List<Employee>();

        public IList<Department> Departments { get; } = new List<Department>();

        public IList<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
