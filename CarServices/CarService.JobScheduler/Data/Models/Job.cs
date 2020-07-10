namespace CarServices.JobScheduler.Data.Models
{
    using CarService.Features.Jobs;
    using System;
    using System.Collections.Generic;

    public class Job
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

        public IList<JobService> PurchasedServices { get; set; }

        public void StartJob(int employeeId, string employeeName)
        {
            if(this.AssignedEmployeeId.HasValue)
            {
                throw new InvalidOperationException("Job is already started");
            }

            this.AssignedEmployeeId = employeeId;
            this.AssignedEmployeeName = employeeName;
            this.JobStatus = JobStatus.Started;
        }
    }
}
