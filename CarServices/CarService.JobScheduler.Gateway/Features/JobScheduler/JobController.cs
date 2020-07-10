

namespace CarService.Gateway.Feautres.Job
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using CarServices;
    using Microsoft.AspNetCore.Authorization;
    using CarService.Gateway.Services.Garages;
    using CarService.Gateway.Features.JobScheduler.Models;
    using CarService.Gateway.Services.JobSchedulers;
    using static CarService.Infrastructure.InfrastructureConstants;
    using CarServices.Services.Employee;
    using System;
    using CarService.Features.Jobs.Models;
    using CarService.Services.Identity;
    using System.Collections.Generic;
    using CarServices.Features.Vehicles;
    using System.Security.Cryptography.X509Certificates;

    public class JobController : ApiController
    {
        private readonly IGarageService garageService;
        private readonly IJobScheduler jobSchedulerService;
        private readonly ICurrentEmployeeService currentEmployeeService;
        private readonly ICurrentUserService currentUserService;

        public JobController(
            IGarageService garageService,
            IJobScheduler jobSchedulerService,
            ICurrentEmployeeService currentEmployeeService,
            ICurrentUserService currentUserService)
        {
            this.garageService = garageService;
            this.jobSchedulerService = jobSchedulerService;
            this.currentEmployeeService = currentEmployeeService;
            this.currentUserService = currentUserService;
        }

        [HttpPost]
        [Authorize(Policy = GarageSalesmanPolicyName)]
        public async Task<ActionResult<JobResponseModel>> Schedule(JobCreateRequestModel model)
        {

            var services = await this.garageService.Services(model.DepartmentId);

            JobScheduleData scheduleJobModel = new JobScheduleData()
            {
                CreatedByEmployeeId = this.currentEmployeeService.CurrentEmployeeId,
                CreateByEmployeeName = this.currentEmployeeService.EmployeeName,
                DeadLine = DateTime.UtcNow,
                DepartmentId = model.DepartmentId,
                PurchasedServices = services.Where(x => model.PurchasedServices.Contains(x.Id)).Select(s => new PurchasedServiceModel()
                {
                    Name = s.Name,
                    Id = s.Id,
                    Price = s.Price,
                }),
                VehicleId = model.VehicleId,
            };

            JobResponseModel scheludeJob = await jobSchedulerService.Schedule(
                scheduleJobModel,
                this.currentEmployeeService.GarageRole.ToString(),
                this.currentEmployeeService.CurrentGarageId.ToString(),
                this.currentEmployeeService.CurrentEmployeeId.ToString(),
                this.currentEmployeeService.EmployeeName);

            return Created(nameof(Schedule), scheludeJob);
        }

        [HttpGet]
        [Route("department/{id}")]
        public async Task<ActionResult<JobScheduleResponseModel>> JobsInDepartment(int id)
        {
            IEnumerable<JobResponseModel> jobs = await this.jobSchedulerService.JobsInDepartment(
                    id,
                    this.currentEmployeeService.GarageRole.ToString(),
                    this.currentEmployeeService.CurrentGarageId.ToString(),
                    this.currentEmployeeService.CurrentEmployeeId.ToString(),
                    this.currentEmployeeService.EmployeeName);

            IEnumerable<VehicleResponseModel> vehicles = new List<VehicleResponseModel>();

            if (jobs.Any())
            {
                var vehicleIds = jobs.Select(x => x.VehicleId).Distinct();

                vehicles = await this.garageService.Vehicles(vehicleIds);
            }


            IEnumerable<JobScheduleResponseModel> result = jobs.Select(j => new JobScheduleResponseModel()
            {
                AssignedEmployeeId = j.AssignedEmployeeId,
                AssignedEmployeeName = j.AssignedEmployeeName,
                CreateByEmployeeName = j.CreateByEmployeeName,
                CreatedByEmployeeId = j.CreatedByEmployeeId,
                CreatedDate = j.CreatedDate,
                DeadLine = j.DeadLine,
                DepartmentId = j.DepartmentId,
                FinishedDate = j.FinishedDate,
                Id = j.Id,
                JobStatus = j.JobStatus,
                PurchasedServices = j.PurchasedServices,
                StratedDate = j.StratedDate,
                Vehicle = vehicles.FirstOrDefault(x => x.Id == j.Id)
            });


            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobScheduleResponseModel>>> CurrentEmployeeJob()
        {
            IEnumerable<JobResponseModel> jobs = new List<JobResponseModel>();
            if (this.currentEmployeeService.IsSalesman)
            {
                jobs = await this.jobSchedulerService.SalesmanJobs(
                    this.currentEmployeeService.GarageRole.ToString(),
                    this.currentEmployeeService.CurrentGarageId.ToString(),
                    this.currentEmployeeService.CurrentEmployeeId.ToString(),
                    this.currentEmployeeService.EmployeeName);
            }
            else if (this.currentEmployeeService.IsOperator)
            {
                jobs = await this.jobSchedulerService.OperatorJobs(
                    this.currentEmployeeService.GarageRole.ToString(),
                    this.currentEmployeeService.CurrentGarageId.ToString(),
                    this.currentEmployeeService.CurrentEmployeeId.ToString(),
                    this.currentEmployeeService.EmployeeName);
            }

            IEnumerable<VehicleResponseModel> vehicles = new List<VehicleResponseModel>();

            if (jobs.Any())
            {
                var vehicleIds = jobs.Select(x => x.VehicleId).Distinct();

                vehicles = await this.garageService.Vehicles(vehicleIds);
            }

            IEnumerable<JobScheduleResponseModel> result = jobs.Select(j => new JobScheduleResponseModel()
            {
                AssignedEmployeeId = j.AssignedEmployeeId,
                AssignedEmployeeName = j.AssignedEmployeeName,
                CreateByEmployeeName = j.CreateByEmployeeName,
                CreatedByEmployeeId = j.CreatedByEmployeeId,
                CreatedDate = j.CreatedDate,
                DeadLine = j.DeadLine,
                DepartmentId = j.DepartmentId,
                FinishedDate = j.FinishedDate,
                Id = j.Id,
                JobStatus = j.JobStatus,
                PurchasedServices = j.PurchasedServices,
                StratedDate = j.StratedDate,
                Vehicle = vehicles.FirstOrDefault(x => x.Id == j.Id)
            });


            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> ChangeJobStatus(int id)
        {
            await this.jobSchedulerService.ChangeJobStatus(
                id,
                this.currentEmployeeService.GarageRole.ToString(),
                this.currentEmployeeService.CurrentGarageId.ToString(),
                this.currentEmployeeService.CurrentEmployeeId.ToString(),
                this.currentEmployeeService.EmployeeName);

            return Ok();
        }
    }
}
