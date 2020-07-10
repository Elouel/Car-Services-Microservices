
namespace CarServices.Garage.Data.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string RegistryNumber { get; set; }
        public Garage Garage { get; set; }
    }
}
