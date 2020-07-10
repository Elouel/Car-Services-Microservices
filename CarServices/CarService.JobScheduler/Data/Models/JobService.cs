
namespace CarServices.JobScheduler.Data.Models
{
    public class JobService
    {
        public int Id { get; set; }

        public int JobId { get; set; }

        public int ServiceId { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }
    }
}
