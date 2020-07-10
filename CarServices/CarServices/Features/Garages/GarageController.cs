namespace CarServices.Garage.Features.Garages
{
    using CarService.Services.Identity;
    using CarServices.Garage.Features.Employees.Models;
    using CarServices.Garage.Features.Garages.Models;
    using CarServices.Garage.Features.Vehicles.Models;
    using CarServices.Services.Employee;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static CarService.Infrastructure.InfrastructureConstants;
    using static CarServices.Infrastructure.WebConstants;

    public class GarageController : ApiController
    {
        private readonly IGarageService garageService;
        private readonly ICurrentEmployeeService currentEmployeeService;
        private readonly ICurrentUserService currentUserService;

        public GarageController(IGarageService garageService,
             ICurrentEmployeeService currentEmployeeService,
            ICurrentUserService currentUserService)
        {
            this.garageService = garageService;
            this.currentEmployeeService = currentEmployeeService;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<GarageResponseModel>> Get(int id)
        {
            var garage = await this.garageService.GetGarage(id);

            var garageResponse = new GarageResponseModel()
            {
                Id = garage.Id,
                GarageName = garage.Name,
                Departments = garage.Departments.Select(d =>
                {
                    return new DepartmentListResponseModel()
                    {
                        Id = d.Id,
                        Name = d.Name,
                    };
                }).ToList(),
            };

            if (this.currentEmployeeService.IsAdmin)
            {
                garageResponse.Employees =
                    garage.Employees.Select(e =>
                    {
                        return new EmployeeResponseModel()
                        {
                            EmployeeId = e.Id,
                            Name = e.Name,
                            Role = (int)e.Role,
                            RoleDisplayName = e.Role.ToString(),
                        };
                    }).ToList();

                garageResponse.Vehicles = garage.Vehicles.Select(v =>
                {
                    return new VehicleListModel()
                    {
                        Id = v.Id,
                        Make = v.Make,
                        Color = v.Color,
                        Model = v.Model,
                        RegistryNumber = v.RegistryNumber
                    };
                }).ToList();
            }

            return garageResponse;
        }

        [HttpPost]
        public async Task<ActionResult<GarageResponseModel>> Create(GarageCreateRequestModel model)
        {
            return await this.garageService.CreateGarage(model, this.currentUserService.UserId);
        }

        //[HttpPost]
        //public async Task<ActionResult<GarageResponseModel>> CreateSpeciealGarage(GarageCreateRequestModel model)
        //{
        //    return await this.garageService.CreateGarage(model, this.currentUserService.UserId);
        //}

        [HttpGet]
        [Route("Departments/{id}")]
        [Authorize(Policy = GarageAdminPolicyName)]
        public async Task<IEnumerable<DepartmentListResponseModel>> Departments(int id)
        {
            var result = await this.garageService.GetGarageDepartments(id);

            return result.Select(d =>
            {
                return new DepartmentListResponseModel()
                {
                    Id = d.Id,
                    Name = d.Name,
                };
            });
        }
    }
}
