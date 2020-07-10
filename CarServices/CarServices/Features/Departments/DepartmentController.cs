namespace CarServices.Garage.Features.Departments
{
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using CarServices.Garage.Features.Departments.Models;
    using CarServices.Garage.Infrastructure.Extensions;
    using CarServices.Garage.Data.Models;
    using CarServices.Garage.Features.Garages.Models;

    using static CarServices.Infrastructure.WebConstants;
    using static CarService.Infrastructure.InfrastructureConstants;
    using System.Collections.Generic;
    using CarServices.Garage.Features.Services.Models;
    using CarServices.Garage.Data;
    using Microsoft.EntityFrameworkCore;
    using CarServices.Services.Employee;

    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService departmentServcie;
        private readonly ICurrentEmployeeService currentEmployeeService;
        private readonly GarageDbContext dbContext;

        public DepartmentController(
            IDepartmentService departmentService,
            ICurrentEmployeeService currentEmployeeService,
            GarageDbContext dbContext)
        {
            this.departmentServcie = departmentService;
            this.currentEmployeeService = currentEmployeeService;
            this.dbContext = dbContext;

        }


        [HttpGet]
        [Route(Id)]
        public async Task<DepartmentDetailsResponseModel> Details(int id)
        {
            Department department = await this.departmentServcie.GetDepartmentDetails(id);

            var departmentResponse = new DepartmentDetailsResponseModel()
            {
                Id = department.Id,
                Name = department.Name,
                Services = department.Services.Select(s =>
                {
                    return new ServiceListItem()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Price = s.Price,
                    };
                }).ToList(),
            };

            if (this.currentEmployeeService.IsAdmin)
            {
                departmentResponse.Employees = department.Employees.Select(e =>
                {
                    return new EmployeeListResponseModel()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Role = e.Role.ToString(),
                    };
                }).ToList();
            }

            return departmentResponse;
        }

        [HttpPost]
        [Authorize(Policy = GarageAdminPolicyName)]
        public async Task<ActionResult<int>> Create(CreateDepartmentRequestModel model)
        {
            var userId = this.User.GetId();

            var result = await this.departmentServcie.CreateDepartment(model, userId);

            return Created(nameof(this.Create), result);
        }



        [HttpGet]
        [Route("{id}/services")]

        public async Task<ActionResult<IEnumerable<ServiceOutputModel>>> Services(int id)
        {

            IEnumerable<ServiceOutputModel> services =
                await this.dbContext
                .Departments
                .Where(d => d.Id == id)
                .Include(d => d.Services)
                .SelectMany(d => d.Services)
                .Select(x =>
                       new ServiceOutputModel()
                       {
                           Id = x.Id,
                           Name = x.Name,
                           Price = x.Price
                       }
                   ).ToListAsync();

            return Ok(services);
        }
    }
}
