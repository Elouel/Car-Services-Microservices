namespace CarServices.Garage.Features.Services
{
    using CarServices.Garage.Data;
    using CarServices.Garage.Data.Models;
    using CarServices.Garage.Features.Services.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    using static CarService.Infrastructure.InfrastructureConstants;

    public class ServiceController : ApiController
    {
        private readonly GarageDbContext dbContext;

        public ServiceController(GarageDbContext carServiceDbContext)
        {
            this.dbContext = carServiceDbContext;
        }

        [HttpPost]
        [Authorize(Policy = GarageAdminPolicyName)]
        public async Task<ActionResult<int>> Create(ServiceCreateRequestModel model)
        {

            Service service = new Service()
            {
                Name = model.ServiceName,
                Price = model.Price,
            };

            Department department = await this.dbContext.Departments.FirstOrDefaultAsync(d => d.Id == model.DepartmentId);
            department.Services.Add(service);

            await this.dbContext.SaveChangesAsync();

            return Created(nameof(this.Create), service.Id);
        }

    }
}
