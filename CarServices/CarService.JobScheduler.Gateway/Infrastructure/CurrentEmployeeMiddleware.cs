

namespace CarServices.Gateway.Infrastructure
{
    using CarService.Gateway.Services.Garages;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using static CarService.Infrastructure.InfrastructureConstants;

    public class CurrentEmployeeMiddleware : IMiddleware
    {
        private readonly IGarageService garageService;

        public CurrentEmployeeMiddleware(IGarageService employeeService)
        {
            this.garageService = employeeService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentEmployee = this.garageService.Details(userId).GetAwaiter().GetResult();

            context.Request.Headers.Add(GarageRoleHeaderName, currentEmployee.RoleDisplayName);
            context.Request.Headers.Add(CurrentGarageIdHeaderName, currentEmployee.GarageId.ToString());
            context.Request.Headers.Add(CurrentEmployeeIdHeaderName, currentEmployee.EmployeeId.ToString());
            context.Request.Headers.Add(CurrentEmployeeNameHeaderName, currentEmployee.Name);

            await next.Invoke(context);
        }
    }
}
