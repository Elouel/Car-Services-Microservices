
namespace CarService.Features.Jobs.Models
{
    using System;
    using System.Collections.Generic;

    public class JobResponseModel
    {
        public int Id { get; set; }

        public int CreatedByEmployeeId { get; set; }
        public string CreateByEmployeeName { get; set; }

        public int? AssignedEmployeeId { get; set; }

        public string AssignedEmployeeName { get; set; }

        public JobStatus JobStatus { get; set; }

        public DateTime DeadLine { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? StratedDate { get; set; }

        public DateTime? FinishedDate { get; set; }

        public int DepartmentId { get; set; }

        public int VehicleId { get; set; }

        public IEnumerable<PurchasedServiceModel> PurchasedServices { get; set; }
    }
}
