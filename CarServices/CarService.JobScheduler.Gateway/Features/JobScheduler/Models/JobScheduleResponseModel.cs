
namespace CarService.Gateway.Features.JobScheduler.Models
{
    using CarService.Features.Jobs;
    using CarService.Features.Jobs.Models;
    using CarServices.Features.Vehicles;
    using System;
    using System.Collections.Generic;

    public class JobScheduleResponseModel
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

        public VehicleResponseModel Vehicle { get; set; }

        public IEnumerable<PurchasedServiceModel> PurchasedServices { get; set; }
    }
}
