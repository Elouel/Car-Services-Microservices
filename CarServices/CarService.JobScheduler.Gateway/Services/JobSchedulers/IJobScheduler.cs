

namespace CarService.Gateway.Services.JobSchedulers
{
    using CarService.Features.Jobs.Models;
    using Refit;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static CarService.Infrastructure.InfrastructureConstants;

    public interface IJobScheduler
    {
        [Post("/jobs")]
        Task<JobResponseModel> Schedule([Body] JobScheduleData model,
            [Header(GarageRoleHeaderName)] string garageRole,
            [Header(CurrentGarageIdHeaderName)] string garageId,
            [Header(CurrentEmployeeIdHeaderName)] string employeeId,
            [Header(CurrentEmployeeNameHeaderName)] string employeeName);

        [Get("/jobs/salesman")]
        Task<IEnumerable<JobResponseModel>> SalesmanJobs(
            [Header(GarageRoleHeaderName)] string garageRole,
            [Header(CurrentGarageIdHeaderName)] string garageId,
            [Header(CurrentEmployeeIdHeaderName)] string employeeId,
            [Header(CurrentEmployeeNameHeaderName)] string employeeName);


        [Get("/jobs/operator")]
        Task<IEnumerable<JobResponseModel>> OperatorJobs(
            [Header(GarageRoleHeaderName)] string garageRole,
            [Header(CurrentGarageIdHeaderName)] string garageId,
            [Header(CurrentEmployeeIdHeaderName)] string employeeId,
            [Header(CurrentEmployeeNameHeaderName)] string employeeName);


        [Get("/jobs/department/{id}")]
        Task<IEnumerable<JobResponseModel>> JobsInDepartment(
        [Query] int id,
        [Header(GarageRoleHeaderName)] string garageRole,
        [Header(CurrentGarageIdHeaderName)] string garageId,
        [Header(CurrentEmployeeIdHeaderName)] string employeeId,
        [Header(CurrentEmployeeNameHeaderName)] string employeeName);

        [Put("/jobs/{id}")]
        Task<IEnumerable<JobResponseModel>> ChangeJobStatus(
        [Query] int id,
        [Header(GarageRoleHeaderName)] string garageRole,
        [Header(CurrentGarageIdHeaderName)] string garageId,
        [Header(CurrentEmployeeIdHeaderName)] string employeeId,
        [Header(CurrentEmployeeNameHeaderName)] string employeeName);
    }
}
