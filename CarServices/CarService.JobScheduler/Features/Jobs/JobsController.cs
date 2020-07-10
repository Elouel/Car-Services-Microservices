namespace CarService.JobScheduler.Features.Jobs
{
    using CarService.Features.Jobs;
    using CarService.Features.Jobs.Models;
    using CarService.JobScheduler.Data;
    using CarServices;
    using CarServices.JobScheduler.Data.Models;
    using CarServices.Services.Employee;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class JobsController : ApiController
    {
        private readonly ICurrentEmployeeService currentEmployeeService;
        private readonly JobSchedulerDbContext dbContext;

        public JobsController(
            ICurrentEmployeeService currentEmployeeService
            , JobSchedulerDbContext dbContext)
        {
            this.currentEmployeeService = currentEmployeeService;
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<JobResponseModel> Schedule(JobScheduleData model)
        {
            Job job = new Job()
            {
                CreatedDate = DateTime.UtcNow,
                CreatedByEmployeeId = model.CreatedByEmployeeId,
                CreateByEmployeeName = model.CreateByEmployeeName,
                DeadLine = DateTime.UtcNow,
                DepartmentId = model.DepartmentId,
                JobStatus = JobStatus.Pending,
                VehicleId = model.VehicleId,
            };

            job.PurchasedServices = model.PurchasedServices.Select(ps => new JobService()
            {
                Name = ps.Name,
                Price = ps.Price,
                ServiceId = ps.Id
            }).ToList();

            this.dbContext.Add(job);

            await this.dbContext.SaveChangesAsync();


            var scheduledJob = new JobResponseModel()
            {
                AssignedEmployeeId = job.AssignedEmployeeId,
                CreatedByEmployeeId = job.CreatedByEmployeeId,
                CreateByEmployeeName = job.CreateByEmployeeName,
                AssignedEmployeeName = job.AssignedEmployeeName,
                CreatedDate = job.CreatedDate,
                DeadLine = job.DeadLine,
                DepartmentId = job.DepartmentId,
                FinishedDate = job.FinishedDate,
                Id = job.Id,
                JobStatus = job.JobStatus,
                PurchasedServices = job.PurchasedServices.Select(s => new PurchasedServiceModel()
                {
                    Name = s.Name,
                    Price = s.Price,
                    Id = s.ServiceId,
                }),
                StratedDate = job.StratedDate,
                VehicleId = job.VehicleId,
            };

            return scheduledJob;
        }

        [HttpGet]
        [Route("salesman")]
        public async Task<IEnumerable<JobResponseModel>> SalesmanJobs()
        {

            IEnumerable<Job> salesmanJobs = await this.dbContext.Jobs
                 .Where(j => j.CreatedByEmployeeId == this.currentEmployeeService.CurrentEmployeeId)
                 .Include(j => j.PurchasedServices)
                 .ToListAsync();

            var response = salesmanJobs.Select(job => new JobResponseModel
            {
                AssignedEmployeeId = job.AssignedEmployeeId,
                CreatedByEmployeeId = job.CreatedByEmployeeId,
                CreateByEmployeeName = job.CreateByEmployeeName,
                AssignedEmployeeName = job.AssignedEmployeeName,
                CreatedDate = job.CreatedDate,
                DeadLine = job.DeadLine,
                DepartmentId = job.DepartmentId,
                FinishedDate = job.FinishedDate,
                Id = job.Id,
                JobStatus = job.JobStatus,
                PurchasedServices = job.PurchasedServices.Select(s => new PurchasedServiceModel()
                {
                    Name = s.Name,
                    Price = s.Price,
                    Id = s.ServiceId,
                }),
                StratedDate = job.StratedDate,
                VehicleId = job.VehicleId,
            });

            return response;
        }

        [HttpGet]
        [Route("operator")]
        public async Task<IEnumerable<JobResponseModel>> OperatorJobs()
        {

            IEnumerable<Job> salesmanJobs = await this.dbContext.Jobs
                 .Where(j => j.AssignedEmployeeId == this.currentEmployeeService.CurrentEmployeeId)
                 .Include(j => j.PurchasedServices)
                 .ToListAsync();

            var response = salesmanJobs.Select(job => new JobResponseModel
            {
                AssignedEmployeeId = job.AssignedEmployeeId,
                CreatedByEmployeeId = job.CreatedByEmployeeId,
                CreateByEmployeeName = job.CreateByEmployeeName,
                AssignedEmployeeName = job.AssignedEmployeeName,
                CreatedDate = job.CreatedDate,
                DeadLine = job.DeadLine,
                DepartmentId = job.DepartmentId,
                FinishedDate = job.FinishedDate,
                Id = job.Id,
                JobStatus = job.JobStatus,
                PurchasedServices = job.PurchasedServices.Select(s => new PurchasedServiceModel()
                {
                    Name = s.Name,
                    Price = s.Price,
                    Id = s.ServiceId,
                }),
                StratedDate = job.StratedDate,
                VehicleId = job.VehicleId,
            });

            return response;
        }

        [HttpGet]
        [Route("department/{id}")]
        public async Task<IEnumerable<JobResponseModel>> JobsInDepartment(int id)
        {

            IEnumerable<Job> salesmanJobs = await this.dbContext.Jobs
                 .Where(j => j.DepartmentId == id)
                 .Include(j => j.PurchasedServices)
                 .ToListAsync();

            var response = salesmanJobs.Select(job => new JobResponseModel
            {
                AssignedEmployeeId = job.AssignedEmployeeId,
                CreatedByEmployeeId = job.CreatedByEmployeeId,
                CreateByEmployeeName = job.CreateByEmployeeName,
                AssignedEmployeeName = job.AssignedEmployeeName,
                CreatedDate = job.CreatedDate,
                DeadLine = job.DeadLine,
                DepartmentId = job.DepartmentId,
                FinishedDate = job.FinishedDate,
                Id = job.Id,
                JobStatus = job.JobStatus,
                PurchasedServices = job.PurchasedServices.Select(s => new PurchasedServiceModel()
                {
                    Name = s.Name,
                    Price = s.Price,
                    Id = s.ServiceId,
                }),
                StratedDate = job.StratedDate,
                VehicleId = job.VehicleId,
            });

            return response;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> ChangeJobStatus(int id)
        {
            Job job = await this.dbContext.Jobs.FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return BadRequest();
            }

            switch (job.JobStatus)
            {
                case JobStatus.Pending:
                    job.StartJob(this.currentEmployeeService.CurrentEmployeeId, this.currentEmployeeService.EmployeeName);
                    break;
                case JobStatus.Started:
                    job.JobStatus = JobStatus.Finished;
                    break;
            }

            await this.dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
