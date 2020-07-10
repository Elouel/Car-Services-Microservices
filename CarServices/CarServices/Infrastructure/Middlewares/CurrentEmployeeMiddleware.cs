

namespace CarServices.Garage.Infrastructure.Middlewares
{
    using CarServices.Garage.Features.Employees;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using static CarService.Infrastructure.InfrastructureConstants;

    public class CurrentEmployeeMiddleware : IMiddleware
    {
        private readonly IEmployeeService employeeService;

        public CurrentEmployeeMiddleware(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentEmployeeGarage = await this.employeeService.GetEmployeeByUser(userId);

            if(currentEmployeeGarage != null)
            {
                context.Request.Headers.Add(GarageRoleHeaderName, currentEmployeeGarage.RoleDisplayName);
                context.Request.Headers.Add(CurrentGarageIdHeaderName, currentEmployeeGarage.GarageId.ToString());
                context.Request.Headers.Add(CurrentEmployeeIdHeaderName, currentEmployeeGarage.EmployeeId.ToString());
            }

            await next.Invoke(context);
        }
    }
}
