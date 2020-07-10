using System.Data.Common;

namespace CarServices.Garage.Features.Services.Models
{
    public class ServiceOutputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
