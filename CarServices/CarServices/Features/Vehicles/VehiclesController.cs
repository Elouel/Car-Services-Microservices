namespace CarServices.Garage.Features.Vehicles
{
    using CarServices.Features.Vehicles;
    using CarServices.Garage.Data;
    using CarServices.Garage.Data.Models;
    using CarServices.Garage.Features.Vehicles.Models;
    using CarServices.Services.Employee;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static CarService.Infrastructure.InfrastructureConstants;

    public class VehiclesController : ApiController
    {
        private readonly GarageDbContext dbContext;
        private readonly ICurrentEmployeeService currentEmployeeService;

        public VehiclesController(GarageDbContext carServiceDbContext,
            ICurrentEmployeeService currentEmployeeService)
        {
            this.dbContext = carServiceDbContext;
            this.currentEmployeeService = currentEmployeeService;
        }

        [HttpPost]
        [Authorize(Policy = GarageAdminPolicyName)]
        public async Task<ActionResult<int>> Create(VehicleCreateRequestModel model)
        {

            Vehicle vehicle = new Vehicle()
            {
                Color = model.Color,
                Make = model.Make,
                RegistryNumber = model.RegistryNumber,
                Model = model.Model,
            };

            Garage garage = await this.dbContext.Garages.FirstOrDefaultAsync(d => d.Id == this.currentEmployeeService.CurrentGarageId);
            garage.Vehicles.Add(vehicle);

            await this.dbContext.SaveChangesAsync();

            return Created(nameof(this.Create), vehicle.Id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleListModel>>> CurrentGarageVehicles()
        {

            IEnumerable<Vehicle> vehicles = await this.dbContext.Garages
                .Where(g => g.Id == currentEmployeeService.CurrentGarageId)
                .Include(g => g.Vehicles)
                .SelectMany(g => g.Vehicles)
                .ToListAsync();


            IEnumerable<VehicleListModel> currentGarageVehicles = vehicles.Select(v => new VehicleListModel()
            {
                Id = v.Id,
                Color = v.Color,
                Make = v.Make,
                Model = v.Model,
                RegistryNumber = v.RegistryNumber,
            });


            return Ok(currentGarageVehicles);
        }

        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<VehicleResponseModel>>> All([FromQuery] IEnumerable<int> ids)
        {

            var vehicles = await this.dbContext
                 .Vehicles
                 .Where(v => ids.Contains(v.Id))
                 .Select(v => new VehicleResponseModel()
                 {
                     Id = v.Id,
                     Make = v.Make,
                     Model = v.Model,
                     Color = v.Color,
                     RegistryNumber = v.RegistryNumber
                 })
                 .ToListAsync();

            return Ok(vehicles);
        }
    }
}
