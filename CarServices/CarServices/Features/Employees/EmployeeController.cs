namespace CarServices.Garage.Features.Employees
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using CarServices.Garage.Features.Employees.Models;
    using CarServices.Garage.Infrastructure.Extensions;

    using static CarServices.Infrastructure.WebConstants;
    using static CarService.Infrastructure.InfrastructureConstants;


    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<EmployeeResponseModel>> Details(string id)
            => await this.employeeService.GetEmployeeByUser(id);

        [HttpPost]
        [Authorize(Policy = GarageAdminPolicyName)]
        public async Task<ActionResult<int>> Create(EmployeeCreateModel model)
        {
            var userId = this.User.GetId();

            var result = await this.employeeService.CreateEmployee(model);

            return Created(nameof(this.Create), result);
        }

        [HttpPut]
        [Route(Id)]
        public async Task<ActionResult<int>> ChangeName(int id, [FromBody] EmployeeUpdateModel model)
        {

            var result = await this.employeeService.UpdateEmployee(id, model);

            return Created(nameof(this.Create), result);
        }
    }
}
